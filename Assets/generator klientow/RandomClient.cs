using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomClient : MonoBehaviour
{
    public enum Gender // enum because it can be used for future to add cosmic/fantasy type characters
    {
        HMale = 0,
        HFemale = 1
    }

    public ClientParts clientParts;

    public SpriteRenderer Hair;
    public SpriteRenderer Head;
    public SpriteRenderer Body;

    //public Gender gender = Gender.HMale;

    void Start()
    {
        randomize( (Gender)Random.Range(0, 2) );
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            randomize((Gender)Random.Range(0, 2));
        }
    }

    void randomize(Gender g)
    {
        switch (g)
        {
            case Gender.HMale:
                Hair.sprite = clientParts.MHairs[Random.Range(0, clientParts.MHairs.Length)];
                Head.sprite = clientParts.MHeads[Random.Range(0, clientParts.MHeads.Length)];
                Body.sprite = clientParts.MBodys[Random.Range(0, clientParts.MBodys.Length)];
                break;
            case Gender.HFemale:
                Hair.sprite = clientParts.FHairs[Random.Range(0, clientParts.FHairs.Length)];
                Head.sprite = clientParts.FHeads[Random.Range(0, clientParts.FHeads.Length)];
                Body.sprite = clientParts.FBodys[Random.Range(0, clientParts.FBodys.Length)];
                break;
            default:
                Debug.Log("ERR: Incorrect/Non existing gender");
                break;
        }
    }
}
