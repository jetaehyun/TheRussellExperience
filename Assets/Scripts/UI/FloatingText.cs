using UnityEngine;

public class FloatingText : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Activate(string animationLabel)
    {
        animator.Play(animationLabel, 0, 0);
    }

    public void Deactivate()
    {
        animator.StopPlayback();
    }
}