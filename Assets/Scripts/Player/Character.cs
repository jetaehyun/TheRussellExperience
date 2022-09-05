using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private enum Direction
    {
        RunUp,
        RunDown,
        RunLeft,
        RunRight,
        Idle
    }

    private enum IdleDirection
    {
        IdleUp,
        IdleDown,
        IdleLeft,
        IdleRight
    }

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float runSpeed = .8f;
    public float speedLimiter = .5f;

    private Animator animator;

    private Direction prevDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        prevDirection = Direction.RunDown;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (PlayerManager.blockPlayerAction)
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
                if (horizontal > 0)
                {
                    setMovementAnimation(Direction.RunRight, true);
                }
                else
                {
                    setMovementAnimation(Direction.RunLeft, true);
                }
            }
            else if (horizontal == 0 && vertical != 0)
            {
                if (vertical > 0)
                {
                    setMovementAnimation(Direction.RunUp, true);
                }
                else
                {
                    setMovementAnimation(Direction.RunDown, true);
                }
            }

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            setIdleAnimation();
        }

    }
    private void setIdleAnimation()
    {
        switch (prevDirection)
        {
            case Direction.RunUp:
                animator.Play(IdleDirection.IdleUp.ToString());
                break;
            case Direction.RunDown:
                animator.Play(IdleDirection.IdleDown.ToString());
                break;
            case Direction.RunLeft:
                animator.Play(IdleDirection.IdleLeft.ToString());
                break;
            case Direction.RunRight:
                animator.Play(IdleDirection.IdleRight.ToString());
                break;
            case Direction.Idle:
                break;
            default:
                animator.Play(IdleDirection.IdleDown.ToString());
                break;
        }

        prevDirection = Direction.Idle;
    }
    private void setMovementAnimation(Direction d, bool state)
    {

        if (prevDirection == d) { return; }

        animator.Play(d.ToString());
        prevDirection = d;
    }
}
