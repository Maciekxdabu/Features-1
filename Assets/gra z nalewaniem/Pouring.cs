using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : MonoBehaviour
{
    public Transform []pourPoints;

    public float dropCooldown;
    private float cooldown;

    public GameObject Drop;

    public Color liquidColor;
    public float liquidSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = dropCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pourPosition = Vector3.zero;
        bool is_Pouring = false;

        foreach (Transform position in pourPoints)
        {
            if (position.position.y < transform.position.y)
            {
                if (is_Pouring == false)
                {
                    pourPosition = position.position;
                    is_Pouring = true;
                }
                else if (pourPosition.y > position.position.y)
                {
                    pourPosition = position.position;
                }
            }
        }

        if (is_Pouring)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0)
            {
                GameObject drop = Instantiate(Drop, pourPosition, Quaternion.identity);
                drop.GetComponent<DropScript>().dropColor = liquidColor;
                drop.GetComponent<DropScript>().speed = liquidSpeed;


                cooldown = dropCooldown;
            }
        }
    }
}
