using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : MonoBehaviour
{
    public Transform []pourPoints;

    public float dropCooldown;
    private float cooldown;

    public GameObject Drop;

    public string[] liquidName;
    public Sprite[] liquidSprite;
    public Color[] liquidColor;
    public float[] liquidSpeed;

    [Header("Do not change")]
    public int currentLiquid = 0;

    void Start()
    {
        cooldown = dropCooldown;
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
                GameObject drop = Instantiate(Drop, pourPosition, Quaternion.identity);
                drop.GetComponent<DropScript>().dropColor = liquidColor[currentLiquid];
                drop.GetComponent<DropScript>().speed = liquidSpeed[currentLiquid];


                cooldown = dropCooldown;
            }
        }
    }

    public void nextLiquid()
    {
        currentLiquid += 1;
        if (currentLiquid == liquidColor.Length)
        {
            currentLiquid = 0;
        }

        GetComponent<SpriteRenderer>().sprite = liquidSprite[currentLiquid];

        GetComponent<BottleMovement>().resetRotation();
    }
}
