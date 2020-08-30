using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoad : MonoBehaviour
{
    public Item item;
    public int quantity;

    public Text text;
    public Image image;
    public Text Qtext;

    void Start()
    {
        text.text = item.name;
        image.sprite = item.sprite;
        Qtext.text = quantity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void LoadData()
    {

    }*/
}
