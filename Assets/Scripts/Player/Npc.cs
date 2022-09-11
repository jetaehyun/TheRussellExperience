using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    private GameObject interactionIcon;
    private FloatingText floatingText;

    private Animator animator;
    private Rigidbody2D rb;
    private readonly string animationLabel = "floatingText";


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.Play("Idle" + gameObject.tag);

        Vector3 pos = gameObject.transform.position;
        pos.y -= 0.05f;
        interactionIcon = Instantiate(floatingTextPrefab, pos, Quaternion.identity) as GameObject;
        floatingText = interactionIcon.GetComponent<FloatingText>();
        interactionIcon.SetActive(false);

        interactionIcon.transform.SetParent(gameObject.transform);
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        interactionIcon.SetActive(true);
        floatingText.Activate(animationLabel);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        floatingText.Deactivate();
        interactionIcon.SetActive(false);
    }
}