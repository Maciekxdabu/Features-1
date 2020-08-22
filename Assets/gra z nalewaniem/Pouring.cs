using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Do not change")]
    public int currentLiquid = 0;//current Stack

    void Start()
    {
        cooldown = dropCooldown;

        bottleSprite = bottleStacks[currentLiquid].bottleSprite;
        liquidColor = bottleStacks[currentLiquid].fluidColor;
        liquidSpeed = bottleStacks[currentLiquid].fluidSpeed;
        liquidName = bottleStacks[currentLiquid].fluidName;

        remainingLiquid = bottleStacks[currentLiquid].frontLiquidValue;

        GetComponent<SpriteRenderer>().sprite = bottleSprite;
    }

    void Update()
    {
        Vector3 pourPosition = Vector3.zero;
        bool is_Pouring = false;

        foreach (Transform position in pourPoints)
        {
            if (position.position.y < transform.position.y)
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
}
