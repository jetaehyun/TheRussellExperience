using System.Collections;
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    public bool isOpen { get; private set; } = false;

    public Click decision { get; private set; } = Click.NONE;

    private TypeWriterEffect t;

    private void Start()
    {
        t = GetComponent<TypeWriterEffect>();
        acceptButton.onClick.AddListener(OnAcceptButton);
        declineButton.onClick.AddListener(OnDeclineButton);
    }

    private IEnumerator PlaySound()
    {
        audioSource.Play();
        yield return new WaitWhile(() => audioSource.isPlaying);
    }
    private void OnAcceptButton()
    {
        StartCoroutine(PlaySound());
        decision = Click.ACCEPTED;
        CloseMessageBox();
    }

    private void OnDeclineButton()
    {
        StartCoroutine(PlaySound());
        decision = Click.DECLINED;
        CloseMessageBox();
    }

    private IEnumerator StepThroughDialog(string text)
    {
        yield return t.Run(text, displayText);
    }

    public void OpenMessageBox(string text)
    {
        decision = Click.NONE;
        messageBox.SetActive(true);
        displayText.text = string.Empty;
        isOpen = true;

        StartCoroutine(StepThroughDialog(text));

    }

    private void CloseMessageBox()
    {
        messageBox.SetActive(false);
        isOpen = false;
    }

}