using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;
using static UnityEditor.Progress;



public class StoreManger : MonoBehaviour
{

    public GameObject itemPrefab;
    public Transform itemPanel;
    public Button leftButton, rightButton;
    public Button firstButton, secondButton, thirdButton;
    public List<StoreItem> allItems;
    private List<StoreItem> filteredItems;
    private int currentPage = 0;
    // 한 페이지에 표시될 아이템 개수
    private int itemsPerPage = 4;


    private void Start()
    {
        PopulateShop();

        leftButton.onClick.AddListener(PreviousPage);
        rightButton.onClick.AddListener(NextPage);
        firstButton.onClick.AddListener(() => FilterItems("drink"));
        secondButton.onClick.AddListener(() => FilterItems("food"));
        thirdButton.onClick.AddListener(() => FilterItems("etc"));


        FilterItems("drink");

    }
    void PopulateShop()
    {
        Debug.Log("PopulateShop() 실행");

        foreach (var item in allItems)
        {
            Debug.Log($"아이템 추가: {item.itemName} | 가격: {item.price}");

            GameObject newItem = Instantiate(itemPrefab, itemPanel);
            

            TextMeshProUGUI itemNameText = newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemPriceText = newItem.transform.Find("BuyButton/ItemPrice").GetComponent<TextMeshProUGUI>();
            Image itemIconImage = newItem.transform.Find("ItemIcon").GetComponent<Image>();

            if (itemNameText != null) itemNameText.text = item.itemName;
            if (itemPriceText != null) itemPriceText.text = item.price.ToString();
            if (itemIconImage != null) itemIconImage.sprite = item.itemIcon;
        }
    }

    void FilterItems(string category)
    {

        filteredItems = allItems.FindAll(item => item.category == category);

        currentPage = 0;

        UpdateShopUI();
    }

    void UpdateShopUI()
    {

        foreach(Transform child in itemPanel)
        {
            Destroy(child.gameObject);
        }

        int startIdx = currentPage * itemsPerPage; 
        // 남아있는 아이템의 개수가 표시되는 아이템보다 적을 경우를 위해서 Mathf.Min 사용
        int endIdx = Mathf.Min(startIdx + itemsPerPage, filteredItems.Count);

        for(int i = startIdx; i < endIdx; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, itemPanel);
            TextMeshProUGUI itemNameText = newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemPriceText = newItem.transform.Find("BuyButton/ItemPrice").GetComponent<TextMeshProUGUI>();
            Image itemIconImage = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();

            itemNameText.text = filteredItems[i].itemName;
            itemPriceText.text = filteredItems[i].price.ToString("X");
            itemIconImage.sprite = filteredItems[i].itemIcon;
            
            buyButton.onClick.AddListener(() => PurchaseItem(filteredItems[i]));


        }

        leftButton.interactable = (currentPage > 0);
        rightButton.interactable = (endIdx < filteredItems.Count);
    }


    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateShopUI();
        }
    }
    void NextPage()
    {
        if ((currentPage + 1) * itemsPerPage < filteredItems.Count)
        {
            currentPage++;
            UpdateShopUI();
            
        }
    }

    void PurchaseItem(StoreItem item)
    {

        Debug.Log($"구매: {item.itemName} | 가격: {item.price}원");
    }


}
