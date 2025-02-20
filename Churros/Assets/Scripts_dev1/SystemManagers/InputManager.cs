using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private PlayerInput _playerInput;

	private void Start()
	{
		_playerInput = GetComponent<PlayerInput>();
	}

	// UI 모드로 전환
	public void SwitchToUIMode()
	{
		_playerInput.SwitchCurrentActionMap("UI");
	}
	// 게임 조작 모드로 전환
	public void SwitchToPlayerMode()
	{
		_playerInput.SwitchCurrentActionMap("Player");
	}
}
