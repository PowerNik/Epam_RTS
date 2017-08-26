using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float moveSpeed = 50;
	public float scrollSpeed = 2;

	public float maxHeight = 50;
	public float minHeight = 5;

	private float height;
	private float tempHeight;

	private float horiz, vert;
	private bool left, right, up, down;

	private CameraLimiter camLimiter;

	void Start()
	{
		height = (maxHeight + minHeight) / 2;
		tempHeight = height;

		Vector3 pos = GameManagerBeforeMerge.GetGameManager().MapManagerInstance.GetCitizenBasePoint();
		float deltaZ = height / Mathf.Tan(transform.eulerAngles.x * Mathf.Deg2Rad);
		transform.position = pos - Vector3.forward * deltaZ;

		camLimiter = GetComponent<CameraLimiter>();
	}

	void Update()
	{
		MoveCamera();
		ScrollCamera();

		Apply();
	}

	private void MoveCamera()
	{
		horiz = (right) ? 1 : ((left) ? -1 : 0);
		vert = (up) ? 1 : ((down) ? -1 : 0);
	}

	private void ScrollCamera()
	{
		float scrollValue = Input.GetAxis("Mouse ScrollWheel");
		if (scrollValue != 0)
		{
			tempHeight -= scrollSpeed * Mathf.Sign(scrollValue);
		}

		tempHeight = Mathf.Clamp(tempHeight, minHeight, maxHeight);
		height = Mathf.Lerp(height, tempHeight, 10 * Time.deltaTime);
	}

	private void Apply()
	{
		Vector3 direction = new Vector3(horiz, 0, vert) * moveSpeed * Time.deltaTime;
		direction = camLimiter.CalculateLimitPosition(direction);

		float xPos = Mathf.Lerp(transform.position.x, direction.x, 100 * Time.deltaTime);
		float zPos = Mathf.Lerp(transform.position.z, direction.z, 100 * Time.deltaTime);
		transform.position = new Vector3(xPos, height, zPos);
	}

	public void CursorTriggerEnter(string triggerName)
	{
		SetTrigger(triggerName, true);
	}

	public void CursorTriggerExit(string triggerName)
	{
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