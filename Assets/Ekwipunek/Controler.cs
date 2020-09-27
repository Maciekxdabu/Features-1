using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controler : MonoBehaviour
{
    public enum GamePad
    {
        Xbox,
        PS4
    }

    public GamePad gamePad;

    public MessageLife message;

    [Tooltip("List of selectables that are constantly visable in the game, in order to disable them when needed")]
    public Selectable[] selectables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( gamePad == GamePad.Xbox ? KeyCode.JoystickButton6 : KeyCode.JoystickButton9))
        {
            if (Backpack.backpack.isDisplayed() == false)
            {
                DisableVisableSelectables();
                Backpack.backpack.Display();
            }
            else
            {
                Backpack.backpack.Hide();
                EnableVisableSelectables();
            }
        }

        if (Backpack.backpack.isDisplayed() == true)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                Backpack.backpack.changeCategory(1);
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                Backpack.backpack.changeCategory(-1);
            }
        }
    }

    private void DisableVisableSelectables()
    {
        foreach (Selectable ob in selectables)
        {
            ob.interactable = false;
        }
    }

    private void EnableVisableSelectables()
    {
        foreach (Selectable ob in selectables)
        {
            ob.interactable = true;
        }
    }

    //meant to be used on button
    public void CompleteQuest(Item item)
    {
        if (Backpack.backpack.useItem(item, 1) == true)
        {
            message.ShowMessage("Quest successfully vompleted - item: \"" + item.name + "\" consumed");
        }
        else
        {
            message.ShowMessage("Mission failed - there was not enough: \"" + item.name + "\" item");
        }
    }

    //meant to be used on button
    public void AddItem(Item item)
    {
        if (Backpack.backpack.addItem(item, 1) == true)
        {
            message.ShowMessage("Item \"" + item.name + "\" successfully added");
        }
        else
        {
            message.ShowMessage("Error: Item \"" + item.name + "\" was not found in backpack");
        }
    }
}
