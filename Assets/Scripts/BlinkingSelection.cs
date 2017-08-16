using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingSelection : MonoBehaviour {
    float timeCounter1 = 0;
    float timeCounter2 = 0;
    bool Blinking = false;
    public bool blinking { get { return Blinking; } set { Blinking = value; } }


    private void Update()
    {
        if (Blinking)
        {
            if (timeCounter2 < 2f)
            {
                Blink();
                timeCounter2 += Time.deltaTime;
            }
            else
            {
                timeCounter2 = 0;
                Blinking = false;
                GetComponent<SpriteRenderer>().color = Color.green;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    public void Blink()
    {
        Blinking = true;
        if (timeCounter1 < 0.5f)
        {
            timeCounter1 += Time.deltaTime;
        }
        else
        {
            Swap();
            timeCounter1 = 0;
        }
    }
    public void Swap()
    {
        if (GetComponent<SpriteRenderer>().enabled == true)
            GetComponent<SpriteRenderer>().enabled = false;
        else
            GetComponent<SpriteRenderer>().enabled = true;
    }
}
