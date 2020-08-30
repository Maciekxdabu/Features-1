using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum GamePad
    {
        Xbox,
        PS4
    }

    public static Controller controler;

    [Tooltip("GamePad to controll with")]
    public GamePad gamePad = GamePad.PS4;

    public float []GoalsList;
    [Tooltip("Money to get for the best score")]
    public int maxMoney;
    [Tooltip("Money to get for worst score")]
    public int minMoney;
    [Tooltip("Maximum difference of goal ratio and player ratio (after it there is only minimal wage)")]
    public float maxDiff;

    public TextMesh goaltext;
    public TextMesh ScoreText;
    public FluidLevel Cup;

    [Header("Bottle to use")]
    public BottleMovement bottle;
    public Pouring bottleP;

    [Header("Leave at zero")]
    public float goal;
    public int Money = 0;

    private bool gameOn = false;

    void Start()
    {
        controler = this;

        newQuest();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (gameOn == false)
            {
                bottle.grabBottle();
                Cup.reset();
                gameOn = true;
            }
            else
            {
                bottle.releaseBottle();
                gameOn = false;
                Serve();
            }
        }

        if (gameOn == true)
        {
            if (Input.GetKeyDown( (gamePad == GamePad.Xbox) ? KeyCode.JoystickButton8 : KeyCode.JoystickButton10 ))//Xbox:8 PS4:10
            {
                bottle.pouringScript.changeLiquid();
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
            Money += minMoney;
        }
        else
        {
            Money += maxMoney - Mathf.FloorToInt( (score / maxDiff)*(maxMoney - minMoney) );
        }

        Cup.reset();
        newQuest();
    }

    public void newQuest()
    {
        goal = GoalsList[Random.Range(0, GoalsList.Length)];
        textReset();
    }

    public void buyDrink(int i)
    {
        Money = bottleP.buyBottle(Money, i);

        textReset();
    }

    private void textReset()
    {
        goaltext.text = "Do: " + goal.ToString() + " wódka to juice ratio";
        ScoreText.text = "Money: " + Money.ToString();
    }

    public bool isGameOn()
    {
        return gameOn;
    }
}
