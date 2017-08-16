using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponentInParent<Attack>().isAttacking)
        {
            Vector3 dirVector = GetComponentInParent<Attack>().enemy.transform.position - transform.position;
            transform.rotation.SetFromToRotation(transform.position, dirVector);
        }
	}
}
