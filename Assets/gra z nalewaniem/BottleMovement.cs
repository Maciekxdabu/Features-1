using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleMovement : MonoBehaviour
{
    public bool toggle = false;
    public float rotationSpeed;
    public float moveSpeed;

    public Pouring pouringScript;
    public Controller controller;

    private Vector3 homeP;
    private Quaternion homeR;

    void Start()
    {
        homeP = transform.position;
        homeR = transform.rotation;
    }

    void Update()
    {
        if (toggle == true)
        {
            //Mouse
            //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            //transform.Rotate(Vector3.back * Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);

            transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);//Xbox and PS4

            if (controller.gamePad == Controller.GamePad.Xbox)
            {
                transform.Rotate(Vector3.back * Input.GetAxis("Joystick_Triggers") * rotationSpeed * Time.deltaTime);//Xbox
            }
            else
            {
                //PS4
                float direction = 0;
                if (Input.GetKey(KeyCode.JoystickButton6))
                {
                    direction -= 1;
                }
                if (Input.GetKey(KeyCode.JoystickButton7))
                {
                    direction += 1;
                }

                transform.Rotate(Vector3.back * direction * rotationSpeed * Time.deltaTime);
            }
        }

        /*if (Input.GetKeyDown(KeyCode.JoystickButton4))//RB
        {
            toggle = true;
        }
        if (Input.GetKeyUp(KeyCode.JoystickButton4))//RB
        {
            toggle = false;

            resetTransform();
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

        resetTransform();
    }

    public void resetTransform()
    {
        transform.position = homeP;
        transform.rotation = homeR;
    }

    public void resetRotation()
    {
        transform.rotation = homeR;
    }
}
