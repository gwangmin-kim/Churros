using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{


    [SerializeField] private GameObject _storePanel;
    [SerializeField] private Button _storeButton;

    void Start()
    {
        _storeButton.onClick.AddListener(OpenStore);
    }

    void OpenStore()
    {
        Debug.Log("상점 창 실행");
        _storePanel.SetActive(true);
    }
  
}
