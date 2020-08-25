using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleMovement : MonoBehaviour
{
    public bool toggle = false;
    [Tooltip("Speed with which bottle is rotated")]
    public float rotationSpeed;
    [Tooltip("Speed with which bottle is moved on the screen")]
    public float moveSpeed;
    [Tooltip("Speed with which the bottle rotates to vertical position (the bigger the distance the stronger the pull)")]
    public float wankaSpeed;
    [Tooltip("Speed with which the bottle rotates to vertical position (constant speed)")]
    public float wankaSpeed2;
    [Tooltip("0 - First option (firts speed), other numbers - second option (second speed)")]
    public int WankaMode = 0;

    public Pouring pouringScript;
    public Controller controller;
    public SpriteRenderer liquid;

    private Vector3 homeP;
    private Quaternion homeR;

    void Start()
    {
        homeP = transform.position;
        homeR = transform.rotation;

        GetComponent<SpriteRenderer>().enabled = false;
        liquid.enabled = false;
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

            if (WankaMode == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * wankaSpeed);
            }
            else
            {
                float ratio = wankaSpeed2 * Time.deltaTime / Quaternion.Angle(transform.rotation, Quaternion.identity);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, ratio);
            }
            
            pouringScript.adjustLiquid();
        }
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

        pouringScript.grabFromStack();

        GetComponent<SpriteRenderer>().enabled = true;
        liquid.enabled = true;
    }

    public void releaseBottle()
    {
        toggle = false;

        pouringScript.releaseFromStack();

        GetComponent<SpriteRenderer>().enabled = false;
        liquid.enabled = false;

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
