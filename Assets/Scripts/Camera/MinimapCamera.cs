using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Camera mainCamera;
    Vector3[] frustumCorners = new Vector3[4];
    private Vector3 cornerPoint;
    RaycastHit[] mainCameraRectCorners = new RaycastHit[4];
    private LineRenderer mainCameraRect;

    [SerializeField]
    private LayerMask layerToRay;

    void Start () {
        cornerPoint.y = 10;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    mainCameraRect = GetComponent<LineRenderer>();
        mainCamera.CalculateFrustumCorners(mainCamera.rect,mainCamera.farClipPlane,Camera.MonoOrStereoscopicEye.Mono,frustumCorners);
	}
	void Update () {
	    for (int i = 0; i < 4; i++)
	    {
            RaycastHit hitPoint;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformVector(frustumCorners[i]).normalized, out hitPoint, 1000, layerToRay))
            {
                mainCameraRectCorners[i] = hitPoint;
                cornerPoint.x = hitPoint.point.x;
                cornerPoint.z = hitPoint.point.z;
                mainCameraRect.SetPosition(i, cornerPoint);
            }
            Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.TransformVector(frustumCorners[i]),Color.blue);
	    }
	    mainCameraRect.SetPosition(4, mainCameraRect.GetPosition(0));
        //TODO.Delete this test block
        #region TestBlock_TODELETE

	    if (Input.GetKeyDown("k"))
	    {
	        Debug.Log(mainCamera.transform.position);
	    }
	    #endregion
    }

	public LayerMask GetLayerToRay()
	{
		return layerToRay;
	}
    
}
