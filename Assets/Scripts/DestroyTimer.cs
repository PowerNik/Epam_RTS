using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

    float timeWaited = 0;
    float TimeSet = 1;
	
	void Update () {
        if (timeWaited < TimeSet)
            timeWaited += Time.deltaTime;
        else
            Destroy(gameObject);
	}
}
