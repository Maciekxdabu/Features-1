using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum Type
    {
        ingredient,
        quest,
        other
    }

    public string Name;
    public Sprite sprite;
    public Type type;
    //public int quantity;
    //public int ID;
    //public static int curID = 0;

    public void Awake()
    {
        //ID = curID;
        //curID++;
    }

    private void OnDestroy()
    {
        
    }
}
