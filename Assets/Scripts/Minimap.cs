using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minimap : MonoBehaviour, IPointerClickHandler
{
    private Camera minimapCamera;
    private Camera mainCamera;
    private Vector3 pos;
    private RawImage minimapImage;

    void Start()
    {
        minimapCamera = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<Camera>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        minimapImage = GetComponent<RawImage>();
        //Debug.Log(minimapImage.mainTexture.height);
        //Debug.Log(minimapImage.mainTexture.width);
        //Debug.Log("Camera height: " + minimapCamera.orthographicSize * 2);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
