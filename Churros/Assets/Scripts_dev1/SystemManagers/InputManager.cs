using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private const string PlayerModeActionMapName = "Player";
	private const string PlaceModeActionMapName = "Place";

	private static PlayerInput _playerInput;
	[SerializeField] private static InputActionMap _playerModeActionMap;
	[SerializeField] private static InputActionMap _placeModeActionMap;

	private void Start()
	{
		_playerInput = GetComponent<PlayerInput>();

		_playerModeActionMap = _playerInput.actions.FindActionMap(PlayerModeActionMapName);
		_placeModeActionMap = _playerInput.actions.FindActionMap(PlaceModeActionMapName);

		SwitchToPlayerMode();
	}

	// 배치 모드로 전환
	public static void SwitchToPlaceMode()
	{
		_playerInput.SwitchCurrentActionMap(PlaceModeActionMapName);
		_playerModeActionMap.Disable();
		_placeModeActionMap.Enable();
	}
	// 게임 모드로 전환
	public static void SwitchToPlayerMode()
	{
		_playerInput.SwitchCurrentActionMap(PlayerModeActionMapName);
		_playerModeActionMap.Enable();
		_placeModeActionMap.Disable();
	}

	// 마우스 좌표 읽기
	public static Ray GetCursorRay()
	{
		return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
	}

	// for input test
	public void ActionTest(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Started:
				Debug.Log("TestAction started");
				break;

			case InputActionPhase.Performed:
				Debug.Log("TestAction performed");
				break;

			case InputActionPhase.Canceled:
				Debug.Log("TestAction canceled");
				break;
		}
	}
}
