using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoad : MonoBehaviour
{
    //public Item item;
    //public int quantity;

    public Text text;
    public Image image;
    public Text Qtext;

    void Start()
    {
        //LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(Backpack.SItem item)
    {
        text.text = item.item.name;
        image.sprite = item.item.sprite;
        Qtext.text = item.quantity.ToString();
    }
}
