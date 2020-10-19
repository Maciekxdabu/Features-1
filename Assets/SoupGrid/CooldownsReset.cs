using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownsReset : MonoBehaviour
{
    public IngredientPack[] Packs;

    public static bool Cooldownable=true;

    public void Start()
    {
        GetComponent<Image>().color = Color.green;
    }

    public void resetCooldowns()
    {
        if (Cooldownable == true)
        {
            Cooldownable = false;
            GetComponent<Image>().color = Color.red;

            foreach (IngredientPack pack in Packs)
            {
                pack.resetCooldown();
            }
        }
        else
        {
            Cooldownable = true;
            GetComponent<Image>().color = Color.green;
        }
    }
}
