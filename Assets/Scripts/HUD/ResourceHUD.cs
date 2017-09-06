using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
	Food = 1,
	Eqipment = 2,
	Special = 3
}
public class ResourceHUD : MonoBehaviour 
{

    [SerializeField]
    GameResource resource;

    private Text resourceValuePresentation;

    void Awake()
    {
        resourceValuePresentation = gameObject.GetComponent<Text>();
    }

    public void Init(ref GameResource resource)
    {
        this.resource = resource;
        this.transform.GetComponentInParent<Image>().sprite = this.resource.backgroundUI;
        resource.ChangeValue = this.ChangeValue;
        ////First invoke of ChangeValue
        resource.Value = resource.Value;
    }

    private void ChangeValue(int newVal)
    {
        resourceValuePresentation.text = newVal.ToString();
    }
}
