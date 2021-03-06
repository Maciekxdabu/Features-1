﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*[System.Serializable]
public class SItem
{
    public Item item;
    public int quantity;
}*/

public class Backpack : MonoBehaviour
{
    [System.Serializable]
    public class SItem
    {
        public Item item;
        public int quantity;
    }

    public static Backpack backpack = null;

    public Transform itemList;
    public GameObject ItemPref;
    public Text PageText;
    public GameObject Background;
    public Transform Selection;
    public ItemLoaderDescription DescriptionItem;

    public bool displayed = false;
    public int chosen_item = 0;//chosen item (in currentList, 0-8)
    public int curPage = 0;//current page number
    private int maxPages;
    public List<int> currentList;//list of currently displayed items

    public List<SItem> Items;

    //public int ItemsPerRow;
    //public int ItemsPerColumn;
    public int ItemsPerPage;

    //public float DistanceHor;
    public float DistanceVer;

    private List<GameObject> Tiles = new List<GameObject>();
    private GameObject lastSelected;
    private GameObject nextSelection;
    
    void Start()
    {
        backpack = this;

        PageText.text = "0/0";

        //ItemsPerPage = ItemsPerRow * ItemsPerColumn;
        //ItemsPerPage = ItemsPerColumn;

        generateTiles();
        nextSelection = Tiles[0];
        Hide();
        refreshView(true);
    }

    public bool addItem(Item item, int quan)
    {
        for (int i=0; i < Items.Count; i++)
        {
            if (Items[i].item == item)
            {
                Items[i].quantity += quan;

                refreshView(true);

                return true;
            }
        }

        return false;
    }

    //returns the number of the given item in backpack (0 means there is no such item in bakpack)
    public int checkForItem(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].item == item)
            {
                return Items[i].quantity;
            }
        }

        return 0;
    }

    //if item exists, and there is enough of it to satisfy number value, it uses it
    public bool useItem(Item item, int number = 1)
    {
        if (number == 0)
        {
            Debug.Log("WAR: Something just requested to use 0 of some item");
            return false;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].item == item)
            {
                if (Items[i].quantity >= number)
                {
                    Items[i].quantity -= number;

                    refreshView(true);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    public void Display()
    {
        displayed = true;

        PageText.enabled = true;
        Background.SetActive(true);
        DescriptionItem.gameObject.SetActive(true);

        refreshView(false);
        EventSystem.current.SetSelectedGameObject(Tiles[0]);
        lastSelected = Tiles[0];
    }

    public void Hide()
    {
        displayed = false;

        PageText.enabled = false;
        Background.SetActive(false);
        DescriptionItem.gameObject.SetActive(false);

        refreshView(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public bool isDisplayed()
    {
        return displayed;
    }

    //generates tiles to displaye items
    private void generateTiles()
    {
        Vector3 curPos = itemList.transform.position;
        //float angle = itemList.transform.rotation.eulerAngles.z;
        Quaternion tempRot = itemList.transform.rotation;
        itemList.transform.rotation = Quaternion.identity;

        for (int i=0; i<ItemsPerPage; i++)
        {
            //curPos.x = itemList.transform.position.x + DistanceHor * (i % ItemsPerRow);
            curPos.y = itemList.transform.position.y - DistanceVer * i;

            GameObject temp = Instantiate(ItemPref, curPos, Quaternion.identity, itemList.transform);
            temp.GetComponent<ItemSelectionEvent>().numberSelected = i;
            Tiles.Add(temp);
            temp.SetActive(false);
        }

        itemList.transform.rotation = tempRot;
    }

    //Destroys tiles responsible for displaying items
    private void DestroyTiles()
    {
        for (int i=0; i<Tiles.Count; i++)
        {
            if (Tiles[i] != null)
            {
                Destroy(Tiles[i]);
            }
        }

        Tiles.Clear();
    }

    //generates a list of objects based on current Category and availibity (if it is more than 0)
    //use when changing category or adding/removing items
    public void makeCurrentList()
    {
        currentList.Clear();

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].quantity > 0)
            {
                currentList.Add(i);
            }
        }
    }

    /// <summary>
    /// Refreshes displayed items data
    /// </summary>
    /// <param name="changed">true if there is a change in inventory (either if category is changed, or items are used/added)</param>
    public void refreshView(bool changed/*, bool itemUsage=false(when an item is used there is no need to change page and chosen_item)*/)
    {
        if (changed == true)
        {
            makeCurrentList();
            curPage = 0;
            maxPages = (int)Mathf.Ceil(currentList.Count * 1.0f / ItemsPerPage);
            chosen_item = 0;

            PageText.text = "1/" + maxPages.ToString();

            if (currentList.Count > 0)
            {
                DescriptionItem.LoadData(Items[currentList[0]].item);
            }
        }

        foreach (GameObject ob in Tiles)
        {
            ob.SetActive(false);
        }

        if (displayed == true)
        {

            int count = 0;//tracks position on current page (from 0 to ItemsPerPage)
            for (int i = ItemsPerPage * curPage; i < Mathf.Min(currentList.Count, ItemsPerPage * (curPage + 1)); i++)
            {
                Tiles[count].SetActive(true);
                Tiles[count].GetComponent<ItemLoad>().LoadData(Items[currentList[i]]);

                count++;
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lastSelected);
            lastSelected = Tiles[0];
        }
    }

    //function meant for item buttons
    public void SelectChosen(int n)
    {
        chosen_item = n;

        refreshDescription();
    }

    public void refreshDescription()
    {
        DescriptionItem.LoadData(Items[currentList[chosen_item + (curPage * ItemsPerPage)]].item);
    }

    //changes page (values only -1, 0, 1)
    //function meant for arrow buttons
    public void changePage(int change)
    {
        change = Mathf.Clamp(change, -1, 1);
        switch (change)
        {
            case -1:
                if (curPage > 0)
                {
                    curPage--;
                }
                break;
            case 1:
                if (currentList.Count > (curPage + 1) * ItemsPerPage)
                {
                    curPage++;
                }
                break;
            case 0:
                break;
            default:
                break;

        }

        PageText.text = (curPage + 1).ToString() + "/" + maxPages.ToString();
        refreshView(false);
    }
}