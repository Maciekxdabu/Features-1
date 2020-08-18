using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum State
    {
        choosingBottle,
        movingBottle,
        gameOut
    }

    public float []GoalsList;

    public int maxMoney;
    public int minMoney;
    [Tooltip("Maximum difference of goal ratio and player ratio (after it there is only minimal wage)")]
    public float maxDiff;
    public float arrowDisplacement;

    public TextMesh goaltext;
    public TextMesh ScoreText;
    public FluidLevel Cup;
    public Transform Arrow;

    [Header("List of bottles to use")]
    public BottleMovement[] bottles;

    [Header("Leave at zero")]
    public float goal;
    public int Score = 0;
    public int chosenBottle = 0;

    //private State current = State.gameOut;
    private bool gameOn = false;
    private bool bottleHeld = false;
    private bool JoystickArrowPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Arrow.gameObject.SetActive(false);

        newQuest();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn == true)
        {
            if (bottleHeld == false)
            {
                if (Input.GetAxis("Joystick_Arrow_Horizontal") == 1 && JoystickArrowPressed == false)//Joystick Right Arrow
                {
                    JoystickArrowPressed = true;
                    chosenBottle += 1;
                    if (chosenBottle == bottles.Length)
                        chosenBottle = 0;

                    Arrow.position = bottles[chosenBottle].transform.position + new Vector3(0, arrowDisplacement, 0);
                }
                if (Input.GetAxis("Joystick_Arrow_Horizontal") == -1 && JoystickArrowPressed == false)//Joystick Left Arrow
                {
                    JoystickArrowPressed = true;
                    chosenBottle -= 1;
                    if (chosenBottle == -1)
                        chosenBottle = bottles.Length - 1;

                    Arrow.position = bottles[chosenBottle].transform.position + new Vector3(0, arrowDisplacement, 0);
                }
            }

            if (Input.GetAxis("Joystick_Arrow_Horizontal") == 0)
            {
                JoystickArrowPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                bottleHeld = !bottleHeld;

                if (bottleHeld == true)
                {
                    bottles[chosenBottle].grabBottle();
                    Arrow.gameObject.SetActive(false);
                }
                else
                {
                    bottles[chosenBottle].releaseBottle();
                    Arrow.gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (gameOn == false)
            {
                chosenBottle = 0;
                gameOn = true;
                //current = State.choosingBottle;
                Arrow.gameObject.SetActive(true);
            }
            else
            {
                if (bottleHeld == true)
                {
                    bottles[chosenBottle].releaseBottle();
                    bottleHeld = false;
                }

                chosenBottle = 0;
                gameOn = false;
                //current = State.gameOut;
                Arrow.gameObject.SetActive(false);
                Serve();
            }
        }
    }

    public void Serve()
    {
        float score = Mathf.Abs(goal - Cup.getWodSokRatio());

        if (Cup.fluidLevel < 50)
        {
            ;
        }
        else if (score >= maxDiff)
        {
            Score += minMoney;
        }
        else
        {
            Score += maxMoney - Mathf.FloorToInt( (score / maxDiff)*(maxMoney - minMoney) );
        }

        Cup.reset();
        newQuest();
    }

    public void newQuest()
    {
        goal = GoalsList[Random.Range(0, GoalsList.Length)];
        goaltext.text = "Do: " + goal.ToString() + " wódka to juice ratio";
        ScoreText.text = "Score: " + Score.ToString();
    }
}
