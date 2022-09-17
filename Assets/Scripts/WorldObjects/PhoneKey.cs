using UnityEngine;
using UnityEngine.UI;

public class PhoneKey : MonoBehaviour
{

    [SerializeField] private GameObject floatingTextPrefab;
    private MessageUi messageUi;

    private FloatingText floatingText;
    private GameObject interactionIcon;
    private readonly string animationLabel = "floatingText";
    private bool inProximity;

    private void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("Closet", 0) == 1);

        Vector3 pos = gameObject.transform.position;
        pos.y -= 0.05f;

        interactionIcon = Instantiate(floatingTextPrefab, pos, Quaternion.identity) as GameObject;
        floatingText = interactionIcon.GetComponent<FloatingText>();
        interactionIcon.SetActive(false);

        interactionIcon.transform.SetParent(gameObject.transform);
        messageUi = GameObject.Find("Canvas").GetComponent<MessageUi>();

    }

    private void Update()
    {

        if (inProximity && Input.GetKeyDown(KeyCode.E))
        {
            int key = PlayerPrefs.GetInt("Key", 0);
            if (key == 1)
            {
                messageUi.OpenMessageBox("You already have the key...", false);
            }
            else
            {
                messageUi.OpenMessageBox("You obtained the key...", false);
                PlayerPrefs.SetInt("Key", 1);
                PlayerPrefs.Save();
            }

            inProximity = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        interactionIcon.SetActive(true);
        floatingText.Activate(animationLabel);

        inProximity = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        floatingText.Deactivate();
        interactionIcon.SetActive(false);
        inProximity = false;

    }
}