using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {

    [SerializeField]
    ParticleSystem flame;
    MeshCollider flameTrigger;
	// Use this for initialization
	void Start () {
        flameTrigger = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Enable()
    {
        flame.Play();
        flameTrigger.enabled = true;
    }
    public void Disable()
    {
        flame.Stop();
        flameTrigger.enabled = false;
    }
    private void OnTriggerStay(Collider obj)
    {
        if (obj.GetComponent<Unit>()!=null)
            if(obj.GetComponent<Unit>().IsEnemy)
            {
                obj.GetComponent<Unit>().Health -= GetComponentInParent<Attack>().currentDPS * Time.deltaTime;
            }
    }
}
