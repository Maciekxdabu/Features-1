using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleMovement : MonoBehaviour
{
    public bool toggle = false;
    public float rotationSpeed;
    public float moveSpeed;

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
            //Mouse
            //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            //transform.Rotate(Vector3.back * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);

            transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
            transform.Rotate(Vector3.back * Input.GetAxis("Joystick_Triggers") * rotationSpeed * Time.deltaTime);
        }

        /*if (Input.GetKeyDown(KeyCode.JoystickButton4))//RB
        {
            toggle = true;
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton4))//RB
        {
            toggle = false;

            transform.position = homeP;
            transform.rotation = homeR;
        }*/
    }
    
    /*private void OnMouseDown()
    {
        grabBottle();
    }

    private void OnMouseUpAsButton()
    {
        releaseBottle();
    }*/

    public void grabBottle()
    {
        toggle = true;
    }

    public void releaseBottle()
    {
        toggle = false;

        transform.position = homeP;
        transform.rotation = homeR;
    }
}
