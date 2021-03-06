﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    [System.Serializable]
    public class IngredientData
    {
        public Ingredient ingredient=null;
        public int quantity=0;
    }

    public int maxIngredients=0;
    
    [SerializeField]
    //private IngredientData[] ingredients = new IngredientData[4];
    private Ingredient[] ingredients = new Ingredient[4];//A list of possible ingredients (dictionary)

    public IngredientData[] startingIngredients;//A list with quantities of starting ingredients

    private List<int> ingredientIndexes = new List<int>();//A list of indexes of ingredeients currently stored in the pot (ingredient list)
    private List<int> quantities = new List<int>();

    private List<Ingredient> inPot;// stores a list of ingredients currently in pot (during display in main game)

    public List<Text> ingredientsTexts;
    public Text mainCount;
    public Image ShopOverlay;
    public Gauge gauge;

    [Header("Do not modify")]
    public bool VisiblePot = false;

    void Start()
    {
        foreach (IngredientData data in startingIngredients)
        {
            int index = getIndex(data.ingredient);

            if (index != -1)
            {
                for (int i=0; i<data.quantity; i++)
                {
                    ingredientIndexes.Add(index);
                }
            }
            else
            {
                Debug.LogWarning("ERR: Ingredient not found in the list of possible ingredients -- TileGenerator:Start()");
            }
        }

        for (int i=0; i<ingredients.Length; i++)
        {
            ingredientsTexts[i].text = CountIngredients(ingredients[i]).ToString();
        }

        mainCount.text = ingredientIndexes.Count + "/" + maxIngredients;
        gauge.adjustFluid(1.0f * ingredientIndexes.Count / maxIngredients);

        Hide();
        GenerateGrid();
    }

    public void HideReveal()
    {
        if (VisiblePot == true)//Hide
        {
            foreach (TileData tile in TileData.tilesList)
            {
                if (tile.used == false)
                {
                    ingredientIndexes.Add(tile.ingredientIndex);
                }
            }

            Hide();
        }
        else//Reveal
        {
            if (ingredientIndexes.Count < TileData.tilesList.Count)
            {
                Debug.LogWarning("There are no enough ingredients to fill the pot, you need at least 61");
                return;
            }

            VisiblePot = true;
            ShopOverlay.enabled = true;

            foreach (TileData tile in TileData.tilesList)
            {
                tile.gameObject.SetActive(true);
            }

            GenerateGrid();
        }
    }

    private void Hide()
    {
        VisiblePot = false;
        ShopOverlay.enabled = false;

        foreach (TileData tile in TileData.tilesList)
        {
            tile.Use();
            tile.gameObject.SetActive(false);
        }
    }

    private int getIndex(Ingredient item)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] == item)
            {
                return i;
            }
        }

        return -1;
    }

    public void GenerateGrid()
    {
        if (VisiblePot == true)
        {
            foreach (TileData tile in TileData.tilesList)
            {
                if (tile.used == false)
                {
                    ingredientIndexes.Add(tile.ingredientIndex);
                    tile.Use();
                }
            }

            List<TileData> TilesPool = new List<TileData>(TileData.tilesList);

            if (ingredientIndexes.Count >= TileData.tilesList.Count)
            {
                while (TilesPool.Count > 0)
                {
                    int randomTile = Random.Range(0, TilesPool.Count);
                    int randomIngredientIndex = Random.Range(0, ingredientIndexes.Count);
                    int randomIngredient = ingredientIndexes[randomIngredientIndex];

                    TilesPool[randomTile].changeIngredient(ingredients[randomIngredient], randomIngredient);

                    TilesPool.RemoveAt(randomTile);
                    ingredientIndexes.RemoveAt(randomIngredientIndex);
                }
            }
        }
    }

    public int CountIngredients(Ingredient ing)
    {
        int index = getIndex(ing);

        if (index != -1)
        {
            int sum = 0;

            for (int i=0; i<ingredientIndexes.Count; i++)
            {
                if (ingredientIndexes[i] == index)
                {
                    sum++;
                }
            }

            return sum;
        }
        else
        {
            return 0;
        }
    }

    public void AddIngredient(Ingredient ing, int quantity)
    {
        if (VisiblePot == false)
        {
            int index = getIndex(ing);

            if (index != -1)
            {
                while (ingredientIndexes.Count < maxIngredients && quantity > 0)
                {
                    ingredientIndexes.Add(index);
                    quantity--;

                    mainCount.text = ingredientIndexes.Count + "/" + maxIngredients;
                    ingredientsTexts[index].text = CountIngredients(ingredients[index]).ToString();
                    gauge.adjustFluid(1.0f * ingredientIndexes.Count / maxIngredients);
                }
            }
        }
    }

    //used in a button
    public void AddIngredient(Ingredient ing)
    {
        if (VisiblePot == false)
        {
            if (ingredientIndexes.Count < maxIngredients)
            {
                int index = getIndex(ing);

                if (index != -1)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        for (int i=0; i<10; i++)
                        {
                            if (ingredientIndexes.Count < maxIngredients)
                                ingredientIndexes.Add(index);
                            else
                                break;
                        }
                    }
                    else
                    {
                        ingredientIndexes.Add(index);
                    }
                    

                    mainCount.text = ingredientIndexes.Count + "/" + maxIngredients;
                    ingredientsTexts[index].text = CountIngredients(ingredients[index]).ToString();
                    gauge.adjustFluid(1.0f * ingredientIndexes.Count / maxIngredients);
                }
            }
        }
    }

    //used in a button
    public void SubtractIngredient(Ingredient ing)
    {
        if (VisiblePot == false)
        {
            int index = getIndex(ing);

            if (index != -1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ingredientIndexes.Remove(index);
                    }
                }
                else
                {
                    ingredientIndexes.Remove(index);
                }

                mainCount.text = ingredientIndexes.Count + "/" + maxIngredients;
                ingredientsTexts[index].text = CountIngredients(ingredients[index]).ToString();
                gauge.adjustFluid(1.0f * ingredientIndexes.Count / maxIngredients);
            }
        }
    }
}
