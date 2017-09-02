using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public bool isRepeatable { get; set; }

    private float timerValue,
                  initTimerValue;

    //public UnityAction timerAction0;

    private float TimerValue
    {
        get { return timerValue; }
        set
        {
            if (timerValue <= 0 && isRepeatable)
            {

            }
        }
    }

    public void Init(float val)
    {
        initTimerValue = timerValue = val;
    }

	void Start () {
		
	}
	
	void Update () {
		
	}
}
