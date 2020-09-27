using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelectionEvent : EventTrigger
{
    public int numberSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnSelect(BaseEventData eventData)
    {
        Backpack.backpack.SelectChosen(numberSelected);
        //base.OnSelect(eventData);
    }
}
