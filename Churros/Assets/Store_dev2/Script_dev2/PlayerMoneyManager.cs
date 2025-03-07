using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    // 싱글톤으로 생성 (단 하나만 존재하고 있음)
    public static PlayerMoneyManager Instance { get; private set;}

    [SerializeField] private int _money = 0;

    // Start보다 먼저 시작되는 함수
	private void Awake()
	{
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    // 금액 비교해서 구매 가능한지 아닌지 판단하는 함수 (구매 가능할 경우 여기서 바로 금액 차감)
    public bool SpendMoney(int price)
    {
        if(_money >= price)
        {
            Debug.Log("정상 차감");
            _money = _money - price;
            Debug.Log($"남은 금액 : {_money}");
            return true;
        }
        else
        {
            Debug.Log("구매 실패 : 잔액 부족");
            return false;
        }

    }

    // 돈을 추가해주는 함수
    public void EarnMoney(int amount)
    {
        Debug.Log("금액 추가");
        _money = _money + amount;

    }

    // 남은 돈을 리턴
    public int CheckMoney()
    {
        return _money;
    }
}
