using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button exitButton;

    public static bool isPaused { get; private set; }

    private void Start()
    {
        exitButton.onClick.AddListener(OnAccept);
    }

    private void OnAccept()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        isPaused = true;

    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
    }

}