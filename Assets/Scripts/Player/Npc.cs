using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GameObject interactionIcon;
    private FloatingText floatingText;
    private MessageUi messageUi;
    private Animator animator;
    private Rigidbody2D rb;
    private readonly string animationLabel = "floatingText";
    private bool inProximity;
    private GameObject player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        messageUi = GameObject.Find("Canvas").GetComponent<MessageUi>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("Idle");

        Vector3 pos = gameObject.transform.position;
        pos.y -= 0.05f;
        interactionIcon = Instantiate(floatingTextPrefab, pos, Quaternion.identity) as GameObject;
        floatingText = interactionIcon.GetComponent<FloatingText>();
        interactionIcon.SetActive(false);

        interactionIcon.transform.SetParent(gameObject.transform);
    }

    private void Update()
    {
        if (inProximity && Input.GetKeyDown(KeyCode.E) && !MenuHandler.isPaused)
        {
            animator.enabled = false;
            SetNpcDirection();
            messageUi.OpenMessageBox(GetNpcDialog(gameObject.tag), false);
            inProximity = false;
        }

        if (!messageUi.isOpen)
        {
            animator.enabled = true;
        }
    }

    private string GetNpcDialog(string npcName)
    {
        switch (npcName)
        {
            case "leo":
                return "What's your most college moment?";
            case "johnny":
                return "Huh???";
            case "will":
                return "I like Five Guys.";
            case "nick":
                return "Give me that bacon egg n cheese!";
            case "arceus":
                return "How'd you get in here?";
            default:
                return "Who am I??";
        }
    }

    private void SetNpcDirection()
    {
        float angle = (player == null) ? 0f : Calculate.AngleBetween2Points(gameObject.transform.position, player.transform.position, 45);
        Direction dir = Direction.Down;

        if (angle < 90) { dir = Direction.Right; }
        else if (angle < 180) { dir = Direction.Up; }
        else if (angle < 270) { dir = Direction.Left; }
        else { dir = Direction.Down; }

        spriteRenderer.sprite = sprites[(int)dir];

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        player = other.collider.gameObject;
        interactionIcon.SetActive(true);
        floatingText.Activate(animationLabel);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        inProximity = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        floatingText.Deactivate();
        interactionIcon.SetActive(false);
        inProximity = false;
        player = null;
    }
}