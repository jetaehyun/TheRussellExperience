using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageUi : MonoBehaviour
{

    public enum Click
    {
        ACCEPTED,
        DECLINED,
        NONE
    }

    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject messageBox;
    public bool isOpen { get; private set; } = false;

    public Click decision { get; private set; } = Click.NONE;

    private void Start()
    {
        acceptButton.onClick.AddListener(OnAcceptButton);
        declineButton.onClick.AddListener(OnDeclineButton);
    }

    private void OnAcceptButton()
    {
        Debug.Log("Pressed accept");
        decision = Click.ACCEPTED;
        CloseMessageBox();
    }

    private void OnDeclineButton()
    {
        Debug.Log("Pressed decline");
        decision = Click.DECLINED;
        CloseMessageBox();
    }

    public void OpenMessageBox(string text)
    {
        decision = Click.NONE;
        messageBox.SetActive(true);
        displayText.text = string.Empty;
        displayText.text = text;
        isOpen = true;
    }

    private void CloseMessageBox()
    {
        messageBox.SetActive(false);
        isOpen = false;
    }

}