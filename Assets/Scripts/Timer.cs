using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private bool isRepeatable { get; set; }

    private float timerValue,
                  initTimerValue;

    UnityAction timerAction;
    private float TimerValue
    {
        get { return timerValue; }
        set
        {
            timerValue = value;
            if (timerValue <= 0 && isRepeatable)
            {
                timerAction.Invoke();
                timerValue = initTimerValue;
            }else if(timerValue <= 0)
            {
                if(timerAction != null)
                {
                    timerAction.Invoke();
                }
            }
        }
    }

    public void Init(float val, bool isRepeatable, UnityAction timerAction)
    {
        initTimerValue = timerValue = val;
        this.timerAction = timerAction;
    }
	
	void FixedUpdate ()
    {
        TimerValue -= Time.fixedDeltaTime;	
	}
}
