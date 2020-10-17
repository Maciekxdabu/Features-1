using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public static List<TileData> tilesList = new List<TileData>();

    public bool used=false;//FOR GAMEPLAY ---- will be used for checking if tile has been used while playing minigame (to not count the ingredient stored in it)
    public int ingredientIndex;

    private void Awake()
    {
        tilesList.Add(this);
        ingredientIndex = -1;
        used = true;
    }

    public void changeIngredient(Ingredient ingred, int ingIndex)
    {
        used = false;

        ingredientIndex = ingIndex;
        GetComponent<SpriteRenderer>().sprite = ingred.sprite;

        int randomRot = Random.Range(0, 6);

        transform.localRotation = Quaternion.Euler(0, 0, 90 + (randomRot * 60));
    }

    public void Use()
    {
        used = true;
        ingredientIndex = -1;
    }
}
