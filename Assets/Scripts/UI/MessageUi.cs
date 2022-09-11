using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    [SerializeField] private GameObject dialogIcon;
    [SerializeField] private GameObject buttonTray;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject messageBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private FloatingText floatingText;
    public bool isOpen { get; private set; } = false;
    private bool isSystemMessage;
    public Click decision { get; private set; } = Click.NONE;
    private TypeWriterEffect t;

    private void Start()
    {
        t = GetComponent<TypeWriterEffect>();
        floatingText = gameObject.transform.Find("MessageBox/DialogIcon").GetComponent<FloatingText>();
        acceptButton.onClick.AddListener(OnAcceptButton);
        declineButton.onClick.AddListener(OnDeclineButton);

        // EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerDown;
        // entry.callback.AddListener((eventData) => {  });
        // trigger.triggers.Add(entry);
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

    public void OpenMessageBox(string text, bool isSystemMessage = true)
    {
        decision = Click.NONE;
        messageBox.SetActive(true);
        displayText.text = string.Empty;
        isOpen = true;
        this.isSystemMessage = isSystemMessage;

        buttonTray.SetActive(isSystemMessage);
        dialogIcon.SetActive(!isSystemMessage);

        if (!isSystemMessage)
        {
            floatingText.Activate("floatingDialog");
        }
        else
        {
            floatingText.Deactivate();
        }


        StartCoroutine(StepThroughDialog(text));

    }

    private void CloseMessageBox()
    {
        messageBox.SetActive(false);
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen && !isSystemMessage && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PlaySound());
            CloseMessageBox();
        }

    }

}