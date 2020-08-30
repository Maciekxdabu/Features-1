using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int CatNumber = 4;
    public enum Category
    {
        All = 0,
        Ingredient = 1,
        Quest = 2,
        Other = 3
    }

    public static Backpack backpack = null;

    public Transform itemList;
    public GameObject ItemPref;
    public Text CatText;

    public bool displayed = false;
    public Category category = Category.All;

    public List<SItem> Items;
    //public int money;

    public int ItemsPerRow;

    public float DistanceHor;
    public float DistanceVer;

    private GameObject Temp;
    
    void Start()
    {
        backpack = this;

        CatText.text = category.ToString();
        CatText.enabled = false;

        /*foreach (SItem it in Items)
        {
            Quantities.Add(0);
        }*/

        //Display();
    }

    void Update()
    {
        
    }

    public bool addItem(Item item, int quan)
    {
        for (int i=0; i < Items.Count; i++)
        {
            if (Items[i].item == item)
            {
                Items[i].quantity += quan;

                refreshView();

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

                    refreshView();

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
        refreshView();
    }

    public void Hide()
    {
        displayed = false;
        refreshView();
    }

    public bool isDisplayed()
    {
        return displayed;
    }

    public void changeCategory(Category newCat)
    {
        category = newCat;

        CatText.text = category.ToString();

        refreshView();
    }

    //changes category for: -1 - to previous one, 0 - does not change, 1 - to next one
    public void changeCategory(int move)
    {
        move = Mathf.Clamp(move, -1, 1);
        if ((int)category + move == CatNumber)
        {
            category = 0;
        }
        else if ((int)category + move == -1)
        {
            category = (Category)(CatNumber - 1);
        }
        else
        {
            category = (Category)((int)category + move);
        }

        CatText.text = category.ToString();

        refreshView();
    }

    public void refreshView()
    {
        DestroyList();
        CatText.enabled = false;

        Temp = new GameObject("TempList");
        Temp.transform.parent = itemList;

        if (displayed == true)
        {
            CatText.enabled = true;

            Vector3 curPos = itemList.transform.position;

            Item.Type curType = Item.Type.other;
            switch (category)
            {
                case Category.All:
                    break;
                case Category.Ingredient:
                    curType = Item.Type.ingredient;
                    break;
                case Category.Other:
                    curType = Item.Type.other;
                    break;
                case Category.Quest:
                    curType = Item.Type.quest;
                    break;
                default:
                    break;
            }

            if (category == Category.All)
            {
                int count = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].quantity > 0)
                    {
                        curPos.x = itemList.transform.position.x + DistanceHor * (count % ItemsPerRow);
                        curPos.y = itemList.transform.position.y - DistanceVer * Mathf.Floor(count / ItemsPerRow);

                        GameObject Tem = Instantiate(ItemPref, curPos, Quaternion.identity, Temp.transform);
                        Tem.GetComponent<ItemLoad>().item = Items[i].item;
                        Tem.GetComponent<ItemLoad>().quantity = Items[i].quantity;

                        count++;
                    }
                }
            }
            else
            {
                int count = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (curType == Items[i].item.type && Items[i].quantity > 0)
                    {
                        curPos.x = itemList.transform.position.x + DistanceHor * (count % ItemsPerRow);
                        curPos.y = itemList.transform.position.y - DistanceVer * Mathf.Floor(count / ItemsPerRow);

                        GameObject Tem = Instantiate(ItemPref, curPos, Quaternion.identity, Temp.transform);
                        Tem.GetComponent<ItemLoad>().item = Items[i].item;
                        Tem.GetComponent<ItemLoad>().quantity = Items[i].quantity;

                        count++;
                    }
                }
            }
        }
    }

    public void DestroyList()
    {
        if (Temp != null)
            Destroy(Temp);
    }
}