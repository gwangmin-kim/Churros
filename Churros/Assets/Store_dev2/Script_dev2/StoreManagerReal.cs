using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StoreManagerReal : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefeb;
    [SerializeField] private Transform itemPanel;
    // 좌우 페이지 넘김 버튼
    [SerializeField] private Button rightPageButton, leftPageButton;
    // 카테고리 분류 버튼
    [SerializeField] private Button categoryFirstButton, categorySecondButton, categoryThirdButton;
    // 등록된 모든 아이템
    [SerializeField] private List<StoreItem> allItems;
    // 카테고리에 따라 분류한 아이템
    [SerializeField] private List<StoreItem> filteredItems;

    // 상점 한 페이지에 표시될 최대 아이템 개수
    private int MaxItemPerPage = 4;
    // 현재 페이지
    private int currentPage = 0;

    void Start()
    {
        
    }

    // (카테고리 변경 및 처음 시작 시) 상점 UI 업데이트
    void UpdateStoreUI()
    {
        // 시작 인덱스
        int startIndex = currentPage * MaxItemPerPage;
        // 끝 인덱스 (아이템 개수가 4개보다 적게 남은경우 적은걸 선택)
        int endIndex = Mathf.Min(startIndex + MaxItemPerPage, filteredItems.Count);

        // 판넬 초기화 (이전 카테고리 상품 제거)
        foreach (Transform before in itemPanel)
        {
            Destroy(before.gameObject);
        }

        for (int i = startIndex; i < endIndex; i++)
        {

        }
    }


}
