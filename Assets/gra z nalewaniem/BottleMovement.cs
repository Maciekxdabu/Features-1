using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleMovement : MonoBehaviour
{
    public bool toggle = false;
    public float rotSpeed;

    private Vector3 homeP;
    private Quaternion homeR;

    // Start is called before the first frame update
    void Start()
    {
        homeP = transform.position;
        homeR = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle == true)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            transform.Rotate(Vector3.back * Input.GetAxisRaw("Horizontal") * rotSpeed);
        }
    }
    
    private void OnMouseDown()
    {
        toggle = true;
    }

    private void OnMouseUpAsButton()
    {
        toggle = false;

        transform.position = homeP;
        transform.rotation = homeR;
    }
}
