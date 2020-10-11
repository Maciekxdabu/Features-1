using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public static List<TileData> tilesList = new List<TileData>();

    public bool used=false;//FOR GAMEPLAY ---- will be used for checking if tile has been used while playing minigame (to not count the ingredient stored in it)
    //public Ingredient ingredient;
    public int ingredientIndex;

    private void Awake()
    {
        tilesList.Add(this);
    }

    public void changeIngredient(Ingredient ingred, int ingIndex)
    {
        //ingredient = ingred;
        ingredientIndex = ingIndex;
        GetComponent<SpriteRenderer>().sprite = ingred.sprite;

        used = false;
    }

    public void Use()
    {
        used = true;
        //ingredient = null;
        ingredientIndex = -1;
    }
}
