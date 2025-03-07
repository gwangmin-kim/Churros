using JetBrains.Annotations;
using Mono.Cecil.Cil;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{


    [SerializeField] private GameObject _recipePanel;
    [SerializeField] private GameObject _storePanel;
    [SerializeField] private GameObject _inventroyPanel;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _workButton;
    public InventoryManger _inventoryManger;
    [SerializeField] private int _salary = 5000;

    private TextMeshProUGUI _totalSales;
    private TextMeshProUGUI _fundage; 
    void Start()
    {
        
        _storeButton.onClick.AddListener(OpenStore);
        _inventoryButton.onClick.AddListener(OpenInventory);
        _workButton.onClick.AddListener(Working);
        
        _totalSales = _recipePanel.transform.Find("TotalSalesValue").GetComponent<TextMeshProUGUI>();
        _fundage = _recipePanel.transform.Find("FundageValue").GetComponent<TextMeshProUGUI>();



    }

	void Update()
	{
        UpdateTotalSales();
        UpdateMoney();
	}
	void UpdateTotalSales()
    {
        
        if (_totalSales != null)
        {
            _totalSales.text = _salary.ToString();
        }
    } 
	void UpdateMoney()
    {
        _fundage.text = PlayerMoneyManager.Instance.CheckMoney().ToString();
    }
	void OpenStore()
    {
        Debug.Log("상점 창 실행");
        _storePanel.SetActive(true);
    }

    void OpenInventory()
    {
        Debug.Log("인벤토리 창 실행");
        _inventroyPanel.SetActive(true);
        _inventoryManger.OpenInventory();
    }

    void Working()
    {
        PlayerMoneyManager.Instance.EarnMoney(_salary);
        Debug.Log($"일을 하고 돈을 벌었습니다. +{_salary}\n현재 금액 : {PlayerMoneyManager.Instance.CheckMoney()}");
    }
  
}
