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
    public Text PageText;
    public Image Background;
    public Transform Selection;
    public ItemLoaderDescription DescriptionItem;

    public bool displayed = false;
    public Category category = Category.All;
    public int chosen_item = 0;//chosen item (in currentList, 0-8)
    public int curPage = 0;//current page number
    private int maxPages;
    public List<int> currentList;//list of currently displayed items

    public List<SItem> Items;
    //public int money;

    public int ItemsPerRow;
    public int ItemsPerColumn;
    private int ItemsPerPage;

    public float DistanceHor;
    public float DistanceVer;

    private List<GameObject> Tiles = new List<GameObject>();
    
    void Start()
    {
        backpack = this;

        CatText.text = category.ToString();
        CatText.enabled = false;
        PageText.text = "0/0";
        PageText.enabled = false;
        Background.enabled = false;

        ItemsPerPage = ItemsPerRow * ItemsPerColumn;

        Selection.position = itemList.position;

        generateTiles();
        //makeCurrentList();
        refreshView(true);

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
        refreshView(false);
    }

    public void Hide()
    {
        displayed = false;
        refreshView(false);
    }

    public bool isDisplayed()
    {
        return displayed;
    }

    public void changeCategory(Category newCat)
    {
        category = newCat;

        CatText.text = category.ToString();

        refreshView(true);
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

        refreshView(true);
    }

    //generates tiles to displaye items
    private void generateTiles()
    {
        //DestroyTiles();

        Vector3 curPos = itemList.transform.position;

        for (int i=0; i<ItemsPerPage; i++)
        {
            curPos.x = itemList.transform.position.x + DistanceHor * (i % ItemsPerRow);
            curPos.y = itemList.transform.position.y - DistanceVer * Mathf.Floor(i / ItemsPerRow);

            GameObject temp = Instantiate(ItemPref, curPos, Quaternion.identity, itemList.transform);
            //temp.SetActive(false);
            Tiles.Add(temp);
            temp.SetActive(false);
        }
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
        //if (displayed == true)
        //{
            currentList.Clear();

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
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].quantity > 0)
                    {
                        currentList.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (curType == Items[i].item.type && Items[i].quantity > 0)
                    {
                        currentList.Add(i);
                    }
                }
            }
        //}
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
            Selection.position = itemList.position + new Vector3((chosen_item % ItemsPerRow) * DistanceHor, Mathf.Floor(chosen_item / ItemsPerColumn) * DistanceVer * -1, 0);

            PageText.text = "1/" + maxPages.ToString();

            if (currentList.Count > 0)
            {
                DescriptionItem.LoadData(Items[currentList[0]].item);
            }
        }

        CatText.enabled = false;
        PageText.enabled = false;
        Background.enabled = false;
        Selection.gameObject.SetActive(false);
        DescriptionItem.gameObject.SetActive(false);

        foreach (GameObject ob in Tiles)
        {
            ob.SetActive(false);
        }

        if (displayed == true)
        {
            CatText.enabled = true;
            PageText.enabled = true;
            Background.enabled = true;
            Selection.gameObject.SetActive(true);
            DescriptionItem.gameObject.SetActive(true);

            int count = 0;//tracks position on current page (from 0 to ItemsPerPage)
            for (int i = ItemsPerPage * curPage; i < Mathf.Min(currentList.Count, ItemsPerPage * (curPage + 1)); i++)
            {
                Tiles[count].SetActive(true);
                Tiles[count].GetComponent<ItemLoad>().LoadData(Items[currentList[i]]);

                count++;
            }
        }
    }

    //0 - up, 1 - right, 2 - down, 3 - left
    public void changeItem(int direction)
    {
        direction = Mathf.Clamp(direction, 0, 3);

        switch (direction)
        {
            case 0:
                chosen_item -= ItemsPerRow;
                if (chosen_item < 0)
                {
                    /*chosen_item = chosen_item + (int)Mathf.Ceil(currentList.Count / ItemsPerRow) * ItemsPerRow;
                    if (chosen_item >= currentList.Count)
                    {
                        chosen_item -= ItemsPerRow;
                    }*/

                    int temp = currentList.Count - (ItemsPerPage * curPage) - 1;
                    chosen_item = Mathf.Min(temp, ItemsPerPage - 1);//TO DO (straight)
                }
                break;
            case 1:
                chosen_item++;
                if (chosen_item > Mathf.Min(currentList.Count - (ItemsPerPage * curPage) - 1, ItemsPerPage - 1))
                {
                    chosen_item = 0;
                }
                break;
            case 2:
                chosen_item += ItemsPerRow;
                if (chosen_item > Mathf.Min(currentList.Count - (ItemsPerPage * curPage) - 1, ItemsPerPage - 1))
                {
                    chosen_item = 0;//TO DO (straight)
                }
                break;
            case 3:
                chosen_item--;
                if (chosen_item < 0)
                {
                    int temp = currentList.Count - (ItemsPerPage * curPage) - 1;
                    chosen_item = Mathf.Min(temp, ItemsPerPage - 1);
                }
                break;
            default:
                Debug.Log("Error with direction value in backpack");
                break;
        }

        DescriptionItem.LoadData(Items[currentList[chosen_item + (curPage * ItemsPerPage)]].item);
        Selection.position = itemList.position + new Vector3((chosen_item % ItemsPerRow) * DistanceHor, Mathf.Floor(chosen_item / ItemsPerColumn) * DistanceVer * -1, 0);
    }

    //changes page (values only -1, 0, 1)
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