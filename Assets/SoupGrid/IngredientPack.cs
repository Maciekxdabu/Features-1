using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPack : MonoBehaviour
{
    [System.Serializable]
    public class IngredientData
    {
        public Ingredient ingredient;
        //public AnimationCurve chances;
        public int min=0;
        public int max;
    }

    public TileGenerator tilegen;

    [Tooltip("Cooldown of buying the pack in seconds")]
    public float cooldownValue;
    public int price;//do not implement now (when integrating with project)
    public IngredientData[] ingredients;

    [Header("References")]
    public Text CooldownText;
    public Text PackName;
    public TileGenerator generator;

    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        CooldownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
            CooldownText.text = cooldown.ToString("F2");

            if (cooldown <= 0)
            {
                GetComponent<Button>().interactable = true;
                PackName.enabled = true;
                CooldownText.enabled = false;
            }
        }
    }

    public void BuyPack()
    {
        if (cooldown <= 0 && generator.VisiblePot == false)
        {
            if (CooldownsReset.Cooldownable == true)
            {
                cooldown = cooldownValue;
                CooldownText.enabled = true;
                GetComponent<Button>().interactable = false;
                PackName.enabled = false;
            }

            foreach (IngredientData data in ingredients)
            {
                int Rquantity = Random.Range(data.min, data.max + 1);

                generator.AddIngredient(data.ingredient, Rquantity);
            }
        }
    }

    public void resetCooldown()
    {
        cooldown = 0;
    }
}
