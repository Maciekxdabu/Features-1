﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStack : MonoBehaviour
{
    public int bottlesNumber;
    public int MaxBottlesNumber;

    public float frontLiquidValue;// min 0, max 100
    public bool isHeld = false;

    [Header("Fluid and bottle values")]
    public string fluidName;
    public Color fluidColor;
    public float fluidSpeed;
    public Sprite bottleSprite;
    public Sprite bottleMask;
    [Tooltip("Dimensions of a rectangle than contain the liquid (shorter)")]
    public float a;
    [Tooltip("Dimensions of a rectangle than contain the liquid (longer)")]
    public float b;
    public Vector3 localRectangePosition;
    public Vector3[] pourPointsPosition;

    [Header("Price of a new bottle")]
    public int Price = 5;

    [Header("Stack visibility")]
    public int whenShowSecond = 2;
    public int whenShowThird = 3;

    public GameObject bottle1;
    public GameObject bottle2;
    public GameObject bottle3;

    public SpriteRenderer fluid1;
    public SpriteRenderer fluid2;
    public SpriteRenderer fluid3;

    public TextMesh QuantityText;

    [Header("Fluid position values")]
    [Tooltip("Minimum value of y of fluid (when it is at its lowest")]
    public float minHeight;
    [Tooltip("Maximum value of y of fluid (when it is at its highest")]
    public float maxHeight;


    void Start()
    {
        bottle1.GetComponent<SpriteRenderer>().sprite = bottleSprite;
        bottle2.GetComponent<SpriteRenderer>().sprite = bottleSprite;
        bottle3.GetComponent<SpriteRenderer>().sprite = bottleSprite;
        bottle1.GetComponent<SpriteMask>().sprite = bottleMask;
        bottle2.GetComponent<SpriteMask>().sprite = bottleMask;
        bottle3.GetComponent<SpriteMask>().sprite = bottleMask;

        fluid1.color = fluidColor;
        fluid2.color = fluidColor;
        fluid3.color = fluidColor;

        bottle1.SetActive(false);
        bottle2.SetActive(false);
        bottle3.SetActive(false);

        if (bottlesNumber >= 1)
        {
            bottle1.SetActive(true);

            if (bottlesNumber >= whenShowSecond)
            {
                bottle2.SetActive(true);

                if (bottlesNumber >= whenShowThird)
                {
                    bottle3.SetActive(true);
                }
            }

            frontLiquidValue = 100;
        }
        else
        {
            frontLiquidValue = 0;
        }

        resetFrontFluid();

        QuantityText.text = bottlesNumber + "/" + MaxBottlesNumber;

        fluid2.transform.localPosition = Vector3.up * maxHeight;
        fluid3.transform.localPosition = Vector3.up * maxHeight;
    }

    void Update()
    {
        //resetFrontFluid();
    }

    public int howMuchBottles()
    {
        return bottlesNumber;
    }

    //This function is meant to only add bottles (allows only non-negative values)
    public bool addBottles(uint n)
    {
        if (bottlesNumber == MaxBottlesNumber)
        {
            return false;
        }

        bottle1.SetActive(false);
        bottle2.SetActive(false);
        bottle3.SetActive(false);

        bottlesNumber += (int)n;

        if (bottlesNumber >= 1)
        {
            bottle1.SetActive(true);

            if (bottlesNumber >= whenShowSecond)
            {
                bottle2.SetActive(true);

                if (bottlesNumber >= whenShowThird)
                {
                    bottle3.SetActive(true);
                }
            }
        }

        QuantityText.text = bottlesNumber + "/" + MaxBottlesNumber;

        return true;
    }

    //deletes current bottle, and takes another one if available
    public void nextBottle()
    {
        bottlesNumber -= 1;

        if (bottlesNumber > 0)
        {
            frontLiquidValue = 100;

            if (bottlesNumber < whenShowThird)
            {
                bottle3.SetActive(false);

                if (bottlesNumber < whenShowSecond)
                {
                    bottle2.SetActive(false);
                }
            }
        }
        else if (bottlesNumber == 0)
        {
            frontLiquidValue = 0;

            bottle1.SetActive(false);
            bottle2.SetActive(false);
            bottle3.SetActive(false);
        }
        else
        {
            bottlesNumber = 0;
            frontLiquidValue = 0;
        }

        resetFrontFluid();

        QuantityText.text = bottlesNumber + "/" + MaxBottlesNumber;
    }

    public void grab()
    {
        isHeld = true;

        bottle1.SetActive(false);
    }

    public void release()
    {
        isHeld = false;

        bottle1.SetActive(true);
    }

    //Kiedy odkładamy butelkę spowrotem (po wyłączeniu minigry)
    public void putBottleAway(float remainingLiq)
    {
        if (remainingLiq == 0)
        {
            nextBottle();
        }
        else
        {
            frontLiquidValue = remainingLiq;
            resetFrontFluid();
        }

        release();
    }

    public void resetFrontFluid()
    {
        fluid1.transform.localPosition = Vector3.up * (minHeight + frontLiquidValue / 100 * (maxHeight - minHeight));
    }
}
