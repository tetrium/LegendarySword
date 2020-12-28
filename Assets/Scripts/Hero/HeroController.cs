using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] AnimatorController animatorController;

    [SerializeField] float speed;


    [Header("Hero States")]
    public bool isFacingRight = false;

    private Rigidbody2D _rigidbody2D;
    private Vector2 movementDirection;
    private Vector2 nonZeroMovementDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        nonZeroMovementDirection = Vector2.right;
        animatorController.Play(AnimationId.Idle);

    }
    void Update()
    {
        HandleControls();
        HandleFacingDirection();
        HandleHeroMovement();

    }
    void HandleFacingDirection() {
        if (nonZeroMovementDirection.x > 0)
        {
            isFacingRight = true;
            this.transform.rotation = Quaternion.identity;
        }
        else {
            isFacingRight = false;
            this.transform.rotation = Quaternion.Euler(0,180,0);

        }
    }
    void HandleControls() {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        if (Mathf.Abs(movementDirection.x) > 0)
        {
            animatorController.Play(AnimationId.Run);

            nonZeroMovementDirection.x = movementDirection.x;
        }
        else {
            animatorController.Play(AnimationId.Idle);

        }
        if (Mathf.Abs(movementDirection.y) > 0)
        {
            nonZeroMovementDirection.y = movementDirection.y;
        }
    }
    void HandleHeroMovement() {
        _rigidbody2D.velocity = new Vector2(movementDirection.x * speed, _rigidbody2D.velocity.y);



    }


}
