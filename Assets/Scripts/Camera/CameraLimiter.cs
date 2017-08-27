using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
	private Camera myCamera;

	private Vector3 worldLeftDown;
	private Vector3 worldLeftUp;
	private Vector3 worldRightUp;
	private Vector3 worldRightDown;

	// Координаты углов ViewPort'а камеры
	private Vector3 leftDown = new Vector3(0, 0, 0);
	private Vector3 leftUp = new Vector3(0, 1, 0);
	private Vector3 rightUp = new Vector3(1, 1, 0);
	private Vector3 rightDown = new Vector3(1, 0, 0);

	private float limitLeft;
	private float limitRight;
	private float limitTop;
	private float limitBot;

	void Start()
	{
		myCamera = GetComponent<Camera>();
        
		limitRight = GameManagerBeforeMerge.GetGameManager().MapManagerInstance.MapWidth;
		limitLeft = 0;
		limitTop = GameManagerBeforeMerge.GetGameManager().MapManagerInstance.MapLength;
		limitBot = 0;
	}

	private void GetViewRect()
	{
		RaycastHit hit;

		Ray ray = myCamera.ViewportPointToRay(leftDown);
		if (Physics.Raycast(ray, out hit))
		{
			worldLeftDown = hit.point;
		}

		ray = myCamera.ViewportPointToRay(leftUp);
		if (Physics.Raycast(ray, out hit))
		{
			worldLeftUp = hit.point;
		}

		ray = myCamera.ViewportPointToRay(rightUp);
		if (Physics.Raycast(ray, out hit))
		{
			worldRightUp = hit.point;
		}

		ray = myCamera.ViewportPointToRay(rightDown);
		if (Physics.Raycast(ray, out hit))
		{
			worldRightDown = hit.point;
		}
	}

	private void OnDrawGizmos()
	{
		if (myCamera == null)
		{
			return;
		}

		GetViewRect();
		Gizmos.color = Color.red;

		Gizmos.DrawLine(worldLeftDown, worldLeftUp);
		Gizmos.DrawLine(worldLeftUp, worldRightUp);
		Gizmos.DrawLine(worldRightUp, worldRightDown);
		Gizmos.DrawLine(worldRightDown, worldLeftDown);
	}


	public Vector3 CalculateLimitPosition(Vector3 direction)
	{
		Vector3 movePos = myCamera.transform.position + direction;
		float height = movePos.y;

		float camRotate = myCamera.transform.rotation.eulerAngles.x;
		float topSideAngle = camRotate - 0.5f * myCamera.fieldOfView;
		float botSideAngle = camRotate + 0.5f * myCamera.fieldOfView;

		float zTop = limitTop - 0.9f * height / Mathf.Tan(topSideAngle * Mathf.Deg2Rad);
		float zBot = limitBot - 0.9f * height / Mathf.Tan(botSideAngle * Mathf.Deg2Rad);
		float zPos = Mathf.Clamp(movePos.z, zBot, zTop);

		float rectWidth = 0.7f * height * myCamera.aspect * Mathf.Cos(topSideAngle * Mathf.Deg2Rad);

		float xRight = limitRight - rectWidth;
		float xLeft = limitLeft + rectWidth;
		float xPos = Mathf.Clamp(movePos.x, xLeft, xRight);

		return new Vector3(xPos, height, zPos);
	}
}
