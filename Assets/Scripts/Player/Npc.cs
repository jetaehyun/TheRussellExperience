using UnityEngine;

public class Npc : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Idle" + gameObject.tag);
        animator.Play("Idle" + gameObject.tag);
    }

    private void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider");
    }
}