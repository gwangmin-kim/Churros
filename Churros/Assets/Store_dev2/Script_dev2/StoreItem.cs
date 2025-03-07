using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "New Store Item", menuName = "Store/Item")]




public class StoreItem : ScriptableObject
{
    public string itemName;  // 아이템 이름
    public Sprite itemIcon;  // 아이템 이미지
    public int price;        // 아이템 가격
    public string category;  // 아이템 카테고리 ("음료수", "식품", "기타")
    public int originalstock ; // 재고 초기값
    [System.NonSerialized] public int stock; // 상점에 남은 재고
    [System.NonSerialized] public int mystock = 0; // 내가 가진 아이템 재고

    // OnEnable은 처음 실행될 때 실행되는 함수 (재고값 초기화에 이용) <= 안해주면 한번 사라진 재고가 게임 재시작해도 돌아오지 않는다.
    private void OnEnable()
    {
        stock = originalstock;
        Debug.Log($"기존 재고값 : {originalstock}, 현재 재고량 : {stock}");
    }
    
}
