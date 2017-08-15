using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{	public Transform startPoint;

	public float moveSpeed = 20;
	public float fastMoveSpeed = 50;

	public KeyCode rotateCamLeft = KeyCode.Q;
	public KeyCode rotateCamRight = KeyCode.E;

	public int rotationX = 50;
	public int rotationLimit = 180;

	public float maxHeight = 50;
	public float minHeight = 5;
	public float scrollSpeed = 2;

	private float rotationSpeed = 3;
	private float camRotation;
	private float height;
	private float tempHeight;

	private float horiz, vert;
	private bool left, right, up, down;
	private bool isFastMove = false;

	void Start()
	{
		height = (maxHeight + minHeight) / 2;
		tempHeight = height;
		transform.position = new Vector3(startPoint.position.x, height, startPoint.position.z);
	}

	void Update()
	{
		MoveCamera();
		ScrollCamera();
		RotateCamera();

		Apply();
	}


	private void MoveCamera()
	{
		horiz = (left) ? -1 : ((right) ? 1 : 0);
		vert = (down) ? -1 : ((up) ? 1 : 0);
	}

	private void RotateCamera()
	{
		if (Input.GetKey(rotateCamLeft))
		{
			camRotation -= rotationSpeed;
		}

		if (Input.GetKey(rotateCamRight))
		{
			camRotation += rotationSpeed;
		}

		camRotation = Mathf.Clamp(camRotation, -rotationLimit, rotationLimit);
	}

	private void ScrollCamera()
	{
		float scrollValue = Input.GetAxis("Mouse ScrollWheel");
		if (scrollValue != 0)
		{
			tempHeight -= scrollSpeed * Mathf.Sign(scrollValue);
		}

		tempHeight = Mathf.Clamp(tempHeight, minHeight, maxHeight);
		height = Mathf.Lerp(height, tempHeight, 3 * Time.deltaTime);
	}

	private void Apply()
	{
		Vector3 direction = new Vector3(horiz, vert, 0);

		if (isFastMove == false)
		{
			direction *= moveSpeed;
		}
		else
		{
			direction *= fastMoveSpeed;
		}

		transform.Translate(direction * Time.deltaTime);

		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		transform.rotation = Quaternion.Euler(rotationX, camRotation, 0);
	}


	public void CursorTriggerEnter(string triggerName)
	{
		isFastMove = false;
		SetTrigger(triggerName, true);
	}

	public void FastCursorTriggerEnter(string triggerName)
	{
		isFastMove = true;
		SetTrigger(triggerName, true);
	}

	public void CursorTriggerExit(string triggerName)
	{
		isFastMove = false;
		SetTrigger(triggerName, false);
	}

	private void SetTrigger(string triggerName, bool value)
	{
		switch (triggerName)
		{
			case "Left":
				left = value;
				break;
			case "Right":
				right = value;
				break;
			case "Up":
				up = value;
				break;
			case "Down":
				down = value;
				break;
		}
	}
}