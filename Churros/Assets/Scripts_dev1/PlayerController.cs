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

	// ���� Input System���� ��� 2d ���ʹ� ��ֶ����� �Ǿ�����
	// �̵��� �ʹ� �����ϰ� ������ �ε巴�� �����ϵ��� �ӷ��� �����ϴ� �Լ�
	private void AdjustSpeed()
	{
		if (playerStatus.isMoving && _moveSpeed < _maxSpeed)
		{
			_moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, Time.deltaTime * _moveDamping);
		}
		else if (!playerStatus.isMoving)
		{
			// 1��: ���ߴ� �͵� �ε巴��(�̲����ٴ� ������ �� �� ����)
			// �̰ɷ� �Ϸ��� Move ��Ŀ���� ���� �ʿ�
			// _moveSpeed = Mathf.Lerp(_moveSpeed, 0, _moveDamping * Time.deltaTime);
			// 2��: ��� ����
			_moveSpeed = 0f;
		}
	}

	// �̵� ������ �ٶ󺸵��� �÷��̾ ȸ����Ŵ
	// �ε巯�� ȸ�� ����
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

	// �Էµ� �������� AdjustSpeed���� ������ �ӷ¿� ���� �̵�
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