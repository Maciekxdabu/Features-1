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
            if (Input.GetKeyDown(KeyCode.D))
            {
                Backpack.backpack.changeCategory(1);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Backpack.backpack.changeCategory(-1);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Backpack.backpack.changeItem(0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Backpack.backpack.changeItem(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Backpack.backpack.changeItem(2);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Backpack.backpack.changeItem(3);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Backpack.backpack.changePage(-1);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Backpack.backpack.changePage(1);
            }
        }
    }
}
