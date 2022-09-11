using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHandler : MonoBehaviour {
    
    [SerializeField] private Button exitButton;
    private void Start() {
        exitButton.onClick.AddListener(OnAccept);
    }

    private void OnAccept()
    {
        Application.Quit();
    }

}