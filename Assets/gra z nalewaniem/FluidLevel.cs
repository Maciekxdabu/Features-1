using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidLevel : MonoBehaviour
{
    [Range(0, 100)]
    public float fluidLevel = 0;
    public float maxY;
    public float liquidPerDrop;
    public Transform fluid;

    private Color currentColor = new Color();
    public List<Color> ColorList = new List<Color>();
    public List<float> ColorValues = new List<float>();
    private int wodkaIndex = -1;
    public Color wodkaColor = Color.white;
    

    // Start is called before the first frame update
    void Start()
    {
        currentColor.a = 255;
    }

    // Update is called once per frame
    void Update()
    {
        fluid.transform.position = new Vector3(0, maxY + fluidLevel*maxY/(-100), 0);
        fluid.GetComponent<SpriteRenderer>().color = currentColor;
    }

    public void addLiquid(Color color)
    {
        if (fluidLevel > 100)
        {
            fluidLevel = 100;
            return;
        }
        else if (fluidLevel == 100)
        {
            return;
        }

        bool newColor = true;

        fluidLevel += liquidPerDrop;

        for (int i=0; i<ColorList.Count; i++)
        {
            if (ColorList[i] == color)
            {
                ColorValues[i] += liquidPerDrop;
                newColor = false;
            }
        }

        if (newColor == true)
        {
            ColorList.Add(color);
            ColorValues.Add(liquidPerDrop);
            if (color == wodkaColor)
            {
                wodkaIndex = ColorList.Count - 1;
            }
        }

        MixColors();
    }

    private void MixColors()
    {
        if (ColorList.Count == 0)
        {
            ;
        }
        else if (ColorList.Count >= 1)
        {
            float weight = ColorValues[0];
            currentColor = ColorList[0];

            for (int i=1; i<ColorList.Count; i++)
            {
                currentColor = Color.Lerp(currentColor, ColorList[i], ColorValues[i] / (weight + ColorValues[i]));
                weight += ColorValues[i];
            }
        }
    }

    public void reset()
    {
        fluidLevel = 0;
        ColorList.Clear();
        ColorValues.Clear();
        wodkaIndex = -1;
    }

    public float getWodSokRatio()
    {
        if (wodkaIndex == -1)
        {
            return 0;
        }
        else
        {
            return ColorValues[wodkaIndex] / fluidLevel;
        }
    }
}
