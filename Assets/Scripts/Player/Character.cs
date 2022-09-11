using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float runSpeed = .8f;
    public float speedLimiter = .5f;
    private Animator animator;
    private Direction prevDirection;
    private MapManager mapManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mapManager = GameObject.Find(ManagerNames.MAP_MANAGER).GetComponent<MapManager>();
        prevDirection = mapManager.GetSpawnDirection();
        SetIdleDirection(prevDirection);

    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (PlayerManager.blockPlayerAction || CanvasManager.blockPlayerAction)
        {
            horizontal = 0f;
            vertical = 0f;
        }

    }

    void FixedUpdate()
    {

        if (horizontal != 0 || vertical != 0)
        {
            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= speedLimiter;
            }

            rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

            if (vertical == 0 && horizontal != 0)
            {
                if (horizontal > 0) { setMovementAnimation(Direction.Right); }
                else { setMovementAnimation(Direction.Left); }
            }
            else if (horizontal == 0 && vertical != 0)
            {
                if (vertical > 0) { setMovementAnimation(Direction.Up); }
                else { setMovementAnimation(Direction.Down); }
            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            setIdleAnimation();
        }

    }
    private void SetIdleDirection(Direction dir)
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        else if (dir == Direction.None)
            return;

        animator.Play("Idle" + dir.ToString());
    }

    private void setIdleAnimation()
    {
        if (prevDirection == Direction.None) { return; }
        SetIdleDirection(prevDirection);

        prevDirection = Direction.None;
    }

    private void setMovementAnimation(Direction d)
    {

        if (prevDirection == d) { return; }

        animator.Play("Run" + d.ToString());
        prevDirection = d;
    }
}
