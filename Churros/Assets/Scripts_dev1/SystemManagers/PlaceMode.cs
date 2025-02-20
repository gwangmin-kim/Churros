using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceMode : MonoBehaviour
{
	private Camera _mainCamera;
	private GameObject _selectedObject;

	private bool _isSelected = false;

	private void Start()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		if (!_isSelected)
		{
			_selectedObject = GetObjectUnderMouse();
		}
		else
		{
			MoveSelectedObject();
		}
	}

	private GameObject GetObjectUnderMouse()
	{
		if (_isSelected)
		{
			return null;
		}
		Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hitInformation;

		if (Physics.Raycast(ray, out hitInformation))
		{
			//Debug.Log(hitInformation.collider.gameObject.name);
			return hitInformation.collider.gameObject;
		}

		return null;
	}

	private void MoveSelectedObject()
	{
		if (!_isSelected)
		{
			return;
		}
		Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hitInformation;

		if (Physics.Raycast(ray, out hitInformation))
		{
			Vector3 targetPosition = hitInformation.point;
			targetPosition.y = _selectedObject.transform.position.y;
			_selectedObject.transform.position = targetPosition;
		}
	}

	private void TrySelectObject()
	{
		if (_isSelected)
		{
			return;
		}
		_isSelected = true;
		//Debug.Log(hitInformation.collider.gameObject.name);
	}

	private void PlaceObject()
	{
		if (!_isSelected)
		{
			return;
		}
		_isSelected = false;
		_selectedObject = null;
	}

	public void ActionClick(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			//Debug.Log("Click performed");
			if (!_isSelected)
			{
				TrySelectObject();
			}
			else
			{
				PlaceObject();
			}
		}
	}
}
