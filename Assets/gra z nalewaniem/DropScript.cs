using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public Color dropColor;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = dropColor;
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cup"))
        {
            collision.gameObject.GetComponent<FluidLevel>().addLiquid(dropColor);
        }

        Destroy(gameObject);
    }
}
