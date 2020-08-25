using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pouring : MonoBehaviour
{
    public Transform []pourPoints;

    public float dropCooldown;
    private float cooldown;

    public GameObject Drop;

    public string liquidName;
    public Sprite bottleSprite;
    public Color liquidColor;
    public float liquidSpeed;

    public BottleStack[] bottleStacks;

    public float remainingLiquid = 100;
    public float liquidPerDrop = 0.075f;

    //public float minY;
    //public float maxY;
    [Tooltip("Shorter edge")]
    public float a;
    [Tooltip("Longer edge")]
    public float b;
    public Transform RectPosition;

    [Header("Do not change")]
    public int currentLiquid = 0;//current Stack
    public SpriteRenderer liquid;
    public Text TInfo;
    private string Tpart;
    private string TGref;

    void Start()
    {
        cooldown = dropCooldown;

        bottleSprite = bottleStacks[currentLiquid].bottleSprite;
        liquidColor = bottleStacks[currentLiquid].fluidColor;
        liquidSpeed = bottleStacks[currentLiquid].fluidSpeed;
        liquidName = bottleStacks[currentLiquid].fluidName;
        liquid.color = liquidColor;

        remainingLiquid = bottleStacks[currentLiquid].frontLiquidValue;

        GetComponent<SpriteRenderer>().sprite = bottleSprite;

        a *= transform.localScale.x;
        b *= transform.localScale.y;
    }

    void Update()
    {
        Vector3 pourPosition = Vector3.zero;
        bool is_Pouring = false;

        float fluidLevel = liquid.transform.position.y + liquid.size.y / 2 * liquid.transform.lossyScale.y;

        foreach (Transform position in pourPoints)
        {
            if (position.position.y < fluidLevel)
            {
                if (is_Pouring == false)
                {
                    pourPosition = position.position;
                    is_Pouring = true;
                }
                else if (pourPosition.y > position.position.y)
                {
                    pourPosition = position.position;
                }
            }
        }

        if (is_Pouring)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0)
            {
                if (remainingLiquid > 0)
                {
                    GameObject drop = Instantiate(Drop, pourPosition, Quaternion.identity);
                    drop.GetComponent<DropScript>().dropColor = liquidColor;
                    drop.GetComponent<DropScript>().speed = liquidSpeed;

                    remainingLiquid -= liquidPerDrop;
                }
                else if (bottleStacks[currentLiquid].bottlesNumber > 0)
                {
                    bottleStacks[currentLiquid].nextBottle();
                    remainingLiquid = bottleStacks[currentLiquid].frontLiquidValue;
                }

                cooldown = dropCooldown;
            }
        }
    }

    public void changeLiquid()
    {
        bottleStacks[currentLiquid].putBottleAway(remainingLiquid);
        bottleStacks[currentLiquid].release();

        currentLiquid += 1;
        if (currentLiquid == bottleStacks.Length)
        {
            currentLiquid = 0;
        }

        bottleSprite = bottleStacks[currentLiquid].bottleSprite;
        liquidColor = bottleStacks[currentLiquid].fluidColor;
        liquidSpeed = bottleStacks[currentLiquid].fluidSpeed;
        liquidName = bottleStacks[currentLiquid].fluidName;
        liquid.color = liquidColor;

        remainingLiquid = bottleStacks[currentLiquid].frontLiquidValue;
        bottleStacks[currentLiquid].grab();

        GetComponent<SpriteRenderer>().sprite = bottleSprite;

        GetComponent<BottleMovement>().resetRotation();
    }

    public void grabFromStack()
    {
        bottleStacks[currentLiquid].grab();
    }

    public void releaseFromStack()
    {
        bottleStacks[currentLiquid].putBottleAway(remainingLiquid);
        bottleStacks[currentLiquid].release();
    }

    public int buyBottle(int m, int i)
    {
        if (bottleStacks[i].Price > m)
        {
            return m;
        }
        else
        {
            if (bottleStacks[i].addBottles(1) == true)
            {
                return m - bottleStacks[i].Price;
            }
            else
            {
                return m;
            }
        }
    }

    public void adjustLiquid()
    {
        if (remainingLiquid <= 0)
        {
            return;
        }
        Transform Ltrans = liquid.transform;
        Ltrans.rotation = Quaternion.identity;
        //Ltrans.position = (Vector3.up * (-12) * transform.localScale.y) + transform.position;
        float liquidHeight = liquid.size.y / 2 * Ltrans.lossyScale.y;

        float angle = transform.eulerAngles.z;
        angle = (angle > 180 ? (360 - angle) : angle);
        angle = (angle > 90 ? 90 - (angle - 90) : angle);// angle ranges from 0 to 90 (inclusive)
        angle *= Mathf.Deg2Rad;
        float angleG = Mathf.Atan(b / a);
        
        if (angle == 0)
        {
            Ltrans.position = RectPosition.position + Vector3.up * ((b * remainingLiquid / 100) - (b / 2) - liquidHeight);
        }
        else if (angle < 90 * Mathf.Deg2Rad)
        {
            if (angle < angleG)// from 0 to G
            {
                float TArea = a * a * Mathf.Tan(angle) / 2;
                float curArea = remainingLiquid / 100 * a * b;
                float height = 0;
                TGref = "under G angle";

                if (curArea <= TArea)//lower triangle
                {
                    height = Mathf.Sqrt(2 * curArea / Mathf.Tan(angle)) * Mathf.Sin(angle);
                    height = (((a * Mathf.Sin(angle)) + (a * b - 2 * TArea) / (a / Mathf.Cos(angle)) / 2) - height) * -1;
                    Tpart = "lower triangle";
                }
                else if (curArea >= (a * b) - TArea)//higher triangle
                {
                    curArea = a * b - curArea;
                    height = Mathf.Sqrt(2 * curArea / Mathf.Tan(angle)) * Mathf.Sin(angle);
                    height = ((a * Mathf.Sin(angle)) + (a * b - 2 * TArea) / (a / Mathf.Cos(angle)) / 2) - height;
                    Tpart = "higher triangle";
                }
                else//middle part
                {
                    curArea -= TArea;
                    height = curArea * Mathf.Cos(angle) / a;
                    height -= (a * b - 2 * TArea) / (a / Mathf.Cos(angle)) / 2;
                    Tpart = "middle";
                }

                Ltrans.position = RectPosition.position + Vector3.up * (height - liquidHeight);
            }
            else if (angle > angleG)//from G to 90
            {
                float TArea = b * b / Mathf.Tan(angle) / 2;
                float curArea = remainingLiquid / 100 * a * b;
                float height = 0;
                TGref = "over G angle";

                if (curArea <= TArea)//lower triangle
                {
                    height = Mathf.Sqrt(2 * curArea * Mathf.Tan(angle)) * Mathf.Cos(angle);
                    height = (((b * Mathf.Cos(angle)) + (a * b - TArea * 2) / (b / Mathf.Sin(angle)) / 2) - height) * -1;
                    Tpart = "lower triangle";
                }
                else if (curArea >= (a * b) - TArea)//higher triangle
                {
                    curArea = a * b - curArea;
                    height = Mathf.Sqrt(2 * curArea * Mathf.Tan(angle)) * Mathf.Cos(angle);
                    height = ((b * Mathf.Cos(angle)) + (a * b - TArea * 2) / (b / Mathf.Sin(angle)) / 2) - height;
                    Tpart = "higher triangle";
                }
                else//middle part
                {
                    curArea -= TArea;
                    height = curArea * Mathf.Sin(angle) / b;
                    height -= (a * b - 2 * TArea) / (b / Mathf.Sin(angle)) / 2;
                    Tpart = "middle";
                }

                Ltrans.position = RectPosition.position + Vector3.up * (height - liquidHeight);
            }
            else if (angle == angleG)
            {
                float curArea = remainingLiquid / 100 * a * b;
                float height = 0;
                TGref = "G angle";

                if (curArea < a * b / 2)//lower triangle
                {
                    height = Mathf.Sqrt(2 * curArea / Mathf.Tan(angle)) * Mathf.Sin(angle);
                    height = (a * b / 2 - height) * -1;
                }
                else if (curArea > a * b / 2)//higher triangle
                {
                    curArea -= a * b / 2;
                    height = Mathf.Sqrt(2 * curArea / Mathf.Tan(angle)) * Mathf.Sin(angle);
                    height = a * b / 2 - height;
                }
                else
                {
                    ;
                }

                Ltrans.position = RectPosition.position + Vector3.up * (height - liquidHeight);
            }
        }
        else if (angle == 90 * Mathf.Deg2Rad)
        {
            Ltrans.position = RectPosition.position + Vector3.up * ((a * remainingLiquid / 100) - (a / 2) - liquidHeight);
        }

        TInfo.text = "G angle: " + (angleG * Mathf.Rad2Deg).ToString() + "\nAngle: " + (angle * Mathf.Rad2Deg).ToString() + "\nPart: " + Tpart + "\nG reference: " + TGref;
    }
}
