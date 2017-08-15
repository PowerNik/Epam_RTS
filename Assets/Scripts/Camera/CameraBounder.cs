using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounder : MonoBehaviour
{
	private Camera camera;

	private Vector3 worldLeftDown;
	private Vector3 worldLeftUp;
	private Vector3 worldRightUp;
	private Vector3 worldRightDown;

	void Start()
	{
		camera = GetComponent<Camera>();
	}

	private void OnDrawGizmos()
	{
		if (camera == null)
		{
			return;
		}

		RaycastHit hit;
		Gizmos.color = Color.red;

		Vector3 leftDown = new Vector3(0, 0, 0);
		Vector3 leftUp = new Vector3(camera.pixelWidth, 0, 0);
		Vector3 RightUp = new Vector3(camera.pixelWidth, camera.pixelHeight, 0);
		Vector3 rightDown = new Vector3(0, camera.pixelHeight, 0);

		Ray ray = camera.ScreenPointToRay(leftDown);
		if (Physics.Raycast(ray, out hit))
		{
			worldLeftDown = hit.point;
		}

		ray = camera.ScreenPointToRay(leftUp);
		if (Physics.Raycast(ray, out hit))
		{
			worldLeftUp = hit.point;
		}

		ray = camera.ScreenPointToRay(RightUp);
		if (Physics.Raycast(ray, out hit))
		{
			worldRightUp = hit.point;
		}

		ray = camera.ScreenPointToRay(rightDown);
		if (Physics.Raycast(ray, out hit))
		{
			worldRightDown = hit.point;
		}

		Gizmos.DrawLine(worldLeftDown, worldLeftUp);
		Gizmos.DrawLine(worldLeftUp, worldRightUp);
		Gizmos.DrawLine(worldRightUp, worldRightDown);
		Gizmos.DrawLine(worldRightDown, worldLeftDown);
	}
}
