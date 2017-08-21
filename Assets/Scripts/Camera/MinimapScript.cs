using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    private Camera mainCamera;
    Vector3[] frustumCorners = new Vector3[4];
    private Vector3 cornerPoint;
    RaycastHit[] mainCameraRectCorners = new RaycastHit[4];
    private LineRenderer mainCameraRect;

    List<GameObject> spehereList = new List<GameObject>();

    void Start () {
        cornerPoint.y = 10;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    mainCameraRect = GetComponent<LineRenderer>();
        mainCamera.CalculateFrustumCorners(mainCamera.rect,mainCamera.farClipPlane,Camera.MonoOrStereoscopicEye.Mono,frustumCorners);
 
//	    Ray corner1 = mainCamera.ScreenPointToRay(mainCamera.transform.TransformVector(frustumCorners[i]));
	    GameObject c1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	    GameObject c2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	    GameObject c3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	    GameObject c4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	    spehereList.Add(c1);
	    spehereList.Add(c2);
	    spehereList.Add(c3);
	    spehereList.Add(c4);
	    //   for (int i = 0; i < 4; i++)
	    //   {
	    //       spehereList[i].transform.position = mainCamera.transform.TransformVector(frustumCorners[i]);
	    //       Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.TransformVector(frustumCorners[i]),Color.green);
	    //   }
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < 4; i++)
	    {
            RaycastHit hitPoint;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformVector(frustumCorners[i]).normalized, out hitPoint, 1000, LayerMask.GetMask("GroundMesh")))
            {
                mainCameraRectCorners[i] = hitPoint;
                cornerPoint.x = hitPoint.point.x;
                cornerPoint.z = hitPoint.point.z;
                mainCameraRect.SetPosition(i, cornerPoint);
//                spehereList[i].transform.position = hitPoint.point;
            }
            Debug.DrawLine(mainCamera.transform.position, mainCamera.transform.TransformVector(frustumCorners[i]),Color.blue);
	    }
	    mainCameraRect.SetPosition(4, mainCameraRect.GetPosition(0));

    }
}
