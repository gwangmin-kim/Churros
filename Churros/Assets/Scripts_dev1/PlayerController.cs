using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
	public PlayerStatus playerStatus;

	private Vector2 _moveInput2d;
	private float _moveSpeed;

	[Header("Movement")]
	[SerializeField] private float _maxSpeed;
	[SerializeField] private float _moveDamping;
	[SerializeField] private float _rotationDamping;

	private void Start()
	{
		playerStatus = new PlayerStatus();
	}

	private void Update()
	{
		// movement
		AdjustSpeed();
		Rotate();
		Move();
	}

	// 현재 Input System에서 얻는 2d 벡터는 노멀라이즈 되어있음
	// 이동이 너무 딱딱하게 느껴져 부드럽게 가속하도록 속력을 조절하는 함수
	private void AdjustSpeed()
	{
		if (playerStatus.isMoving && _moveSpeed < _maxSpeed)
		{
			_moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, Time.deltaTime * _moveDamping);
		}
		else if (!playerStatus.isMoving)
		{
			// 1안: 멈추는 것도 부드럽게(미끄럽다는 느낌이 날 수 있음)
			// 이걸로 하려면 Move 매커니즘 수정 필요
			// _moveSpeed = Mathf.Lerp(_moveSpeed, 0, _moveDamping * Time.deltaTime);
			// 2안: 즉시 정지
			_moveSpeed = 0f;
		}
	}

	// 이동 방향을 바라보도록 플레이어를 회전시킴
	// 부드러운 회전 적용
	private void Rotate()
	{
		if (!playerStatus.isMoving)
		{
			return;
		}
		Vector3 direction = new Vector3(_moveInput2d.x, 0, _moveInput2d.y);
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationDamping);
	}

	// 입력된 방향으로 AdjustSpeed에서 결정된 속력에 따라 이동
	private void Move()
	{
		Vector3 direction = new Vector3(_moveInput2d.x, 0, _moveInput2d.y);
		transform.Translate(Time.deltaTime * _moveSpeed * direction, Space.World);
	}

	private void Interact()
	{

	}

	private void Attack()
	{

	}

	// Process Input
	public void ActionMove(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				playerStatus.isMoving = true;
				_moveInput2d = context.ReadValue<Vector2>();
				break;

			case InputActionPhase.Canceled:
				playerStatus.isMoving = false;
				_moveInput2d = Vector2.zero;
				break;
		}
	}

	public void ActionInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Debug.Log("performed");
		}
	}

	public void ActionAttack(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Debug.Log("performed");
		}
	}
}

public class PlayerStatus
{
	public bool isPlaying;
	public bool isMoving;
	public bool isHolding;

	public PlayerStatus()
	{
		isPlaying = false;
		isMoving = false;
		isHolding = false;
	}
}

public class PlayerMovement
{

}