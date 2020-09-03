using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLoaderDescription : MonoBehaviour
{
    public Text Name;
    public Image image;
    public Text description;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(Item item)
    {
        Name.text = item.name;
        image.sprite = item.sprite;
        description.text = item.Description;
    }
}
