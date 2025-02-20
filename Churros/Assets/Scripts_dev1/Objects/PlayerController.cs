using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerStatus _playerStatus;
	
	private Vector2 _moveInput2d;
	private float _moveSpeed;

	[Header("Movement")]
	[SerializeField] private float _maxSpeed;
	[SerializeField] private float _moveDamping;
	[SerializeField] private float _rotationDamping;

	[Header("Interaction")]
	[SerializeField] private float _interactionRange;

	private void Start()
	{
		_playerStatus = new PlayerStatus();
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
		if (_playerStatus.isMoving && _moveSpeed < _maxSpeed)
		{
			_moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, Time.deltaTime * _moveDamping);
		}
		else if (!_playerStatus.isMoving)
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
		if (!_playerStatus.isMoving)
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
		// check collision
		// 플레이어의 다음 위치에 SphereCast를 해서 충돌하는 물체가 있다면 움직이지 않음
		Vector3 direction = new Vector3(_moveInput2d.x, 0, _moveInput2d.y);
		if (Physics.SphereCast(transform.position, 0.5f, direction, out RaycastHit _, Time.deltaTime * _moveSpeed))
		{
			return;
		}
		transform.Translate(Time.deltaTime * _moveSpeed * direction, Space.World);
	}

	private void Interact()
	{
		Debug.Log("Interact Called");

		// 상호작용 가능한 물체가 앞에 있는지 확인
		RaycastHit hitInformation;
		if (Physics.Raycast(transform.position, transform.forward, out hitInformation, _interactionRange))
		{
			IInteractable interactable = hitInformation.collider.GetComponent<IInteractable>();
			if (interactable != null)
			{
				interactable.Interact(_playerStatus);
			}
		}
	}

	private void Attack()
	{
		Debug.Log("Attack Called");

		// 무언가를 들고 있으면 공격할 수 없음
		if (_playerStatus.IsHolding())
		{
			return;
		}
	}

	// Process Input
	public void ActionMove(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				_playerStatus.isMoving = true;
				_moveInput2d = context.ReadValue<Vector2>();
				break;

			case InputActionPhase.Canceled:
				_playerStatus.isMoving = false;
				_moveInput2d = Vector2.zero;
				break;
		}
	}

	public void ActionInteract(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Debug.Log("performed");
			Interact();
		}
	}

	public void ActionAttack(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			// Debug.Log("performed");
			Attack();
		}
	}
}

public class PlayerStatus
{
	public bool isPlaying;
	public bool isMoving;
	private PlayerItemQueue _playerItemQueue;

	public PlayerStatus()
	{
		isPlaying = false;
		isMoving = false;
		_playerItemQueue = new PlayerItemQueue(1);
	}

	// 무언가를 들고 있으면 특정 행동이 불가능한 로직 구현이 필요할 수 있음
	// ex: 공격, 문 열기, 주문 받기 등
	public bool IsHolding()
	{
		return !_playerItemQueue.IsEmpty();
	}

	// 아이템 획득(상호작용으로써 IInteractable에 의해 호출)
	// 성공 실패 여부를 bool 값으로 반환
	// FiniteContainer에서 보관된 아이템의 개수를 줄여야할 지 판단하기 위함 (실패 시 남은 개수가 줄어들지 않아야 하므로)
	public bool Get(Item item)
	{
		return _playerItemQueue.Enqueue(item);
	}

	// 아이템 내려놓기(상호작용으로써 IInteractable에 의해 호출)
	// 실패 시 null 반환
	public Item Put()
	{
		return _playerItemQueue.Dequeue();
	}
}

// 플레이어가 들고 있는 아이템을 큐에 저장
public class PlayerItemQueue
{
	private int _maxSize;
	private Queue<Item> _itemQueue;

	public PlayerItemQueue(int size)
	{
		_maxSize = size;
		_itemQueue = new Queue<Item>(size);
	}

	public bool IsEmpty()
	{
		return _itemQueue.Count == 0;
	}

	public bool Enqueue(Item item)
	{
		if (_itemQueue.Count >= _maxSize)
		{
			Debug.Log("cannot Get item: Player holds too many items");
			return false;
		}
		_itemQueue.Enqueue(item);
		return true;
	}

	public Item Dequeue()
	{
		if (IsEmpty())
		{
			Debug.Log("cannot Put item: Player holds no item");
			return null;
		}
		return _itemQueue.Dequeue();
	}
}

public class PlayerMovement
{

}