using UnityEngine;

[CreateAssetMenu(fileName = "New Store Item", menuName = "Store/Item")]




public class StoreItem : ScriptableObject
{
    public string itemName;  // 아이템 이름
    public Sprite itemIcon;  // 아이템 이미지
    public int price;        // 아이템 가격
    public string category;  // 아이템 카테고리 ("음료수", "식품", "기타")
}
