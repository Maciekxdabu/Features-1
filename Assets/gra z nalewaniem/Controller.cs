using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float []GoalsList;

    public int maxMoney;
    public int minMoney;
    [Tooltip("Maximum difference of goal ratio and player ratio (after it there is only minimal wage)")]
    public float maxDiff;

    public TextMesh goaltext;
    public TextMesh ScoreText;
    public FluidLevel Cup;

    [Header("Leave at zero")]
    public float goal;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        newQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
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
