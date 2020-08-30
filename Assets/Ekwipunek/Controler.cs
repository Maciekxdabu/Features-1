using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Backpack.backpack.isDisplayed() == false)
            {
                Backpack.backpack.Display();
            }
            else
            {
                Backpack.backpack.Hide();
            }
        }

        if (Backpack.backpack.isDisplayed() == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Backpack.backpack.changeCategory(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Backpack.backpack.changeCategory(-1);
            }
        }
    }
}
