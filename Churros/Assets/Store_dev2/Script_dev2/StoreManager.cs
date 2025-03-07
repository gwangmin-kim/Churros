using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StoreManagerReal : MonoBehaviour
{

    [SerializeField] private GameObject _itemPrefeb;
    [SerializeField] private GameObject _storePanel;
    [SerializeField] private Transform _itemPanel;
    // 좌우 페이지 넘김 버튼
    [SerializeField] private Button _rightPageButton, _leftPageButton;
    // 카테고리 분류 버튼
    [SerializeField] private Button _categoryFirstButton, _categorySecondButton, _categoryThirdButton;
    // 창 닫기 버튼
    [SerializeField] private Button _exitButton;
    // 등록된 모든 아이템
    [SerializeField] private List<StoreItem> _allItems;
    // 카테고리에 따라 분류한 아이템
    [SerializeField] private List<StoreItem> _filteredItems;

    // 상점 한 페이지에 표시될 최대 아이템 개수
    private int _maxItemPerPage = 4;
    // 현재 페이지
    private int _currentPage = 0;

    private void Start()
    {
        _leftPageButton.onClick.AddListener(PreviousPage);
        _rightPageButton.onClick.AddListener(NextPage);
        _exitButton.onClick.AddListener(ExitStore);
        _categoryFirstButton.onClick.AddListener(() => FilterItems("drink"));
        _categorySecondButton.onClick.AddListener(() => FilterItems("food"));
        _categoryThirdButton.onClick.AddListener(() => FilterItems("etc"));


        FilterItems("drink");
        
    }

    // 이전 페이지로 넘어가는 함수
    void PreviousPage()
    {
        // 페이지가 가장 앞일 경우 실행 X
        if(_currentPage > 0)
        {
            _currentPage--;
            UpdateStoreUI();
        }
    }

    // 다음 페이지로 넘어가는 함수
    void NextPage()
    {
        // 다음 페이지가 존재하지 않을 경우 실행 X
        if((_currentPage + 1) * _maxItemPerPage < _filteredItems.Count)
        {
            _currentPage++;
            UpdateStoreUI();
        }
    }

    // 카테고리를 선택할 때 분류해주는 함수
    void FilterItems(string category)
    {
        // 람다식을 이용하여 입력받은 카테고리와 일치하는 아이템 찾기 => filteredItems에 넣기
        _filteredItems = _allItems.FindAll(item => item.category == category);

        _currentPage = 0;
        UpdateStoreUI();
    }
    
    // (카테고리 변경 및 처음 시작 시) 상점 UI 업데이트
    void UpdateStoreUI()
    {
        Debug.Log("UpdateStoreUI() 실행");
        // 시작 인덱스
        int startIndex = _currentPage * _maxItemPerPage;
        // 끝 인덱스 (아이템 개수가 4개보다 적게 남은경우 적은걸 선택)
        int endIndex = Mathf.Min(startIndex + _maxItemPerPage, _filteredItems.Count);

        // 판넬 초기화 (이전 카테고리 상품 제거)
        foreach (Transform before in _itemPanel)
        {
            Destroy(before.gameObject);
        }

        for (int i = startIndex; i < endIndex; i++)
        {
            //Instantiate(A, B) => A를 B에 복제한다. 아이템 프리팹을 판넬에 복제
            GameObject newItem = Instantiate(_itemPrefeb, _itemPanel);
            //프리팹의 요소들을 변수로 생성 (이름, 가격, 아이콘, 버튼 순)
            TextMeshProUGUI itemNameText = newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemPriceText = newItem.transform.Find("BuyButton/ItemPrice").GetComponent<TextMeshProUGUI>();
            Image itemIconImage = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
            

            //프리팹의 요소들을 변수에 저장 (이름, 가격, 아이콘, 버튼 순)
            itemNameText.text = _filteredItems[i].itemName;
            itemPriceText.text = _filteredItems[i].price.ToString();
            itemIconImage.sprite = _filteredItems[i].itemIcon;
            // i를 람다식에 직접 참조할 경우, 최종 값인 4가 들어가게 된다
            int index = i;
            buyButton.onClick.AddListener(() => PurchaseItem(_filteredItems[index]));


            
        }
    }

    void PurchaseItem(StoreItem item)
    {
        Debug.Log($"아이템 구매 : {item.itemName}");

    }

    void ExitStore()
    {
        _storePanel.SetActive(false);
    }

}
