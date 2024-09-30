
// CameraRotate
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public Transform targetObject;
	public Camera CameraObject;
	public Vector3 targetOffset;

	public float averageDistance = 5f;

	public float maxDistance = 20f;

	public float minDistance = 0.6f;

	public float xSpeed = 200f;

	public float ySpeed = 200f;

	public int yMinLimit = -80;

	public int yMaxLimit = 80;
	public CanvasGroup _Canvas;

	public float EndLook = 60, startLook = 50;
	//   public int zoomSpeed = 40;

	//  public float panSpeed = 0.3f;

	public float zoomDampening = 5f;

	//   public float rotateOnOff = 1f;

	public float xDeg;

	public float yDeg;

	private float currentDistance;

	public float desiredDistance;

	private Quaternion currentRotation;

	Quaternion desiredRotation;

	Quaternion rotation;

	Vector3 position;

	//private float idleTimer;

	private float idleSmooth;

	// public static CameraRotate instance;

	private void Start()
	{
		Init();
	}

	private async void OnEnable()
	{
		Init();
		await Task.Delay(2000);
		CameraObject.fieldOfView = 55;
		IsMainMenu = true;
		isDragging = false;
	}

	public void Init()
	{
		if (!targetObject)
		{
			GameObject gameObject = new GameObject("Cam Target");
			gameObject.transform.position = base.transform.position + base.transform.forward * averageDistance;
			targetObject = gameObject.transform;
		}

		currentDistance = averageDistance;
		desiredDistance = averageDistance;
		position = base.transform.position;
		rotation = base.transform.rotation;
		currentRotation = base.transform.rotation;
		desiredRotation = base.transform.rotation;
		position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
		//SetChracterPos();
	}

	public void SetCarMianPos()
	{
		isDragging = true;
		StartCoroutine(SetPos(-139f, 0.3f, 6f));
		isrimSelect = false;
		maxDistance = 6f;
		minDistance = 6f;
		targetOffset.y = -0.22f;
	}

	public void SetChracterPos()
	{
		isDragging = true;
		StartCoroutine(SetPos(-90f, 9f, 4f));
		isrimSelect = false;
		maxDistance = 4f;
		minDistance = 4f;
		targetOffset.y = 0;
	}

	public void SetPlatePaintPos()
	{
		//    print(DatabaseManager.instance.currentVehicle);
		//  if (DatabaseManager.instance.currentVehicle == 11)
		//    StartCoroutine(SetPos(90, 12, 2.7f));
		//else
		isDragging = true;
		StartCoroutine(SetPos(40, 10, 2.4f));
		maxDistance = 6;
		minDistance = 6;
		isrimSelect = false;
	}

	public void SetBodyPaintPos()
	{
		isDragging = true;
		StartCoroutine(SetPos(200, 20, 3.4f));
		maxDistance = 20;
		minDistance = 20;
		isrimSelect = false;
	}

	public void SetRimPos()
	{
		isDragging = true;
		StartCoroutine(SetPos(-103.18f, 0, 3.4f));
		maxDistance = 15;
		minDistance = 15;
		isrimSelect = true;
	}

	IEnumerator SetPos(float x, float y, float dd)
	{
		int loopCount = 20;
		float xStep = Mathf.Abs(((x - xDeg) / loopCount));
		float yStep = Mathf.Abs(((y - yDeg) / loopCount));
		float desiredDistanceStep = Mathf.Abs(((dd - desiredDistance) / loopCount));

		for (int i = 0; i < loopCount; i++)
		{
			yield return null;
			xDeg = Mathf.MoveTowards(xDeg, x, xStep);
			yDeg = Mathf.MoveTowards(yDeg, y, yStep);
			desiredDistance = Mathf.MoveTowards(desiredDistance, dd, desiredDistanceStep);
		}
	}

	public bool isDragging;
	public bool isrimSelect,IsMainMenu;
	public float AutoSpeed = 0.05f;

	private void LateUpdate()
	{
		if (isDragging && !IsMainMenu)
		{
			xDeg += CnControls.CnInputManager.GetAxis("Mouse X") * xSpeed * 0.02f;
			xDeg = ClampAngle(xDeg, -360, 360);
			yDeg -= CnControls.CnInputManager.GetAxis("Mouse Y") * ySpeed * 0.02f;
			yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
			desiredRotation = Quaternion.Euler(yDeg, xDeg, 0f);
			currentRotation = base.transform.rotation;
			rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening);
			base.transform.rotation = rotation;
			//idleTimer = 0f;
			idleSmooth = 0f;
		}
		else if (!isrimSelect && !IsMainMenu)
		{
			xDeg += xSpeed * AutoSpeed * Time.deltaTime;
			yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
			desiredRotation = Quaternion.Euler(yDeg, xDeg, 0f);
			currentRotation = base.transform.rotation;
			rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening * 2f);
			base.transform.rotation = rotation;
		}

		//desiredDistance -= Input.GetAxis("Mouse ScrollWheel")  0.02f  (float)zoomSpeed * Mathf.Abs(desiredDistance);
		desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
		currentDistance = Mathf.Lerp(currentDistance, desiredDistance, 0.02f * zoomDampening);
		position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
		base.transform.position = position;
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}

		if (angle > 360f)
		{
			angle -= 360f;
		}

		return Mathf.Clamp(angle, min, max);
	}

	/*public void OnBeginDrag()
	{
	    isDragging = true;
	}*/
	/*public void OnEndrag()
	{
	    isDragging = false;
	}*/



	// void Update()
	// {
	// 	if (DragCheck)
	// 	{
	// 		if (CameraObject)
	// 		{
	// 			if (CameraObject.fieldOfView > startLook)
	// 			{
	// 				CameraObject.fieldOfView -= 10f * Time.deltaTime;
	// 			}
	//
	// 			if (_Canvas && _Canvas.alpha > 0)
	// 			{
	// 				_Canvas.alpha -= 0.05f;
	// 				isDragging = true;
	// 			}
	// 		}
	// 	}
	// 	else
	// 	{
	// 		if (CameraObject)
	// 		{
	// 			if (CameraObject.fieldOfView < EndLook)
	// 			{
	// 				CameraObject.fieldOfView += 10f * Time.deltaTime;
	// 			}
	//
	// 			if (_Canvas && _Canvas.alpha < 1)
	// 			{
	// 				_Canvas.alpha += 0.05f;
	// 				isDragging = false;
	// 			}
	// 		}
	// 	}
	// }

	private bool DragCheck = false;

	public void Drage(bool DragValue)
	{
		isDragging = DragValue;
		DragCheck = DragValue;
	}
}
