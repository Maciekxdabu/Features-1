using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageLife : MonoBehaviour
{
    public float cooldownTime=2f;
    private float cooldown=0f;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            Color color = text.color;
            color.a = Mathf.Clamp(255 * cooldown / cooldownTime, 0, 255);
            text.color = color;

            if (cooldown <= 0)
            {
                text.enabled = false;
            }
        }
    }

    public void ShowMessage(string message)
    {
        cooldown = cooldownTime;
        text.enabled = true;
        text.text = message;
    }
}
