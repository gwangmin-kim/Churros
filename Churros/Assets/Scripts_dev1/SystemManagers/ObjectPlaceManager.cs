using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlaceManager : MonoBehaviour
{
	// 위치를 찍기 위한 raycast에 사용할 레이어마스크
	public static LayerMask layer;
	private static ObjectController _selectedObject;

	private void Start()
	{
		layer = LayerMask.GetMask("Object");
	}

	public static void SelectObject(ObjectController gameObject)
	{
		_selectedObject = gameObject;
	}

	public static void DeselectObject()
	{
		SelectObject(null);
	}

	private GameObject GetObjectUnderMouse()
	{
		Ray ray = InputManager.GetCursorRay();
		RaycastHit hitInformation;

		if (Physics.Raycast(ray, out hitInformation, Mathf.Infinity, layer))
		{
			Debug.Log(hitInformation.collider.gameObject.name);
			return hitInformation.collider.gameObject;
		}
		return null;
	}

	// Process Input
	public void ActionClick(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Click performed");
			if (_selectedObject == null)
			{
				if (GetObjectUnderMouse().TryGetComponent<ObjectController>(out ObjectController objectController))
				{
					SelectObject(objectController);
				}
			}
			else
			{
				DeselectObject();
			}
		}
	}

	public void ActionPoint(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Point performed");
			ObjectPlacement.MoveToCursor(_selectedObject);
		}
	}

	public void ActionRotate(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			Debug.Log("Rotate performed");
			ObjectPlacement.Rotate(_selectedObject);
		}
	}
}

public static class ObjectPlacement
{
	public static void MoveToCursor(ObjectController gameObject)
	{
		if (gameObject == null)
		{
			return;
		}

		Ray ray = InputManager.GetCursorRay();
		RaycastHit hitInformation;
		
		if (Physics.Raycast(ray, out hitInformation, Mathf.Infinity, TileMap.layer))
		{
			Vector3 targetPosition = TileMap.GetAlignedPosition(hitInformation.point, gameObject.size);
			gameObject.transform.position = targetPosition;
		}
	}
	
	public static void Rotate(ObjectController gameObject)
	{
		if (gameObject == null)
		{
			return;
		}
		Vector3 angle = new Vector3(0, 90, 0);
		gameObject.transform.Rotate(angle);
		gameObject.SwapSize();
		
		// 돌린 상태에 맞춰 위치 재조정
		MoveToCursor(gameObject);
	}
}