using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] AnimatorController animatorController;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [Header("Hero States")]
    public bool isFacingRight = false;
    public bool heroIsOnGround = false;
    public bool heroIsJumping = false;
    [Header("Interruption Variables")]



    [Header("Layer Checkers")]
    [SerializeField] LayerChecker footA;
    [SerializeField] LayerChecker footB;


    private Rigidbody2D _rigidbody2D;


    //Control Variables
    private bool jumpButtonPressed = false;
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
        CheckGround();
        HandleFacingDirection();
        HandleHeroMovement();
        HandleJump();
    }
    void HandleJump() {
        if (jumpButtonPressed && heroIsOnGround&&!heroIsJumping) {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            StartCoroutine(HandleJumpAnimation());
        }
        if (heroIsJumping&&heroIsOnGround) {
            heroIsJumping = false;
        }
       
    }

    IEnumerator HandleJumpAnimation() {
        yield return new WaitForSeconds(0.2f);
        heroIsJumping = true;
        animatorController.Play(AnimationId.Jump);

    }

    void CheckGround() {
        heroIsOnGround = footA.isTouching || footB.isTouching;
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
        jumpButtonPressed = Input.GetButtonDown("Jump");

     




        if (Mathf.Abs(movementDirection.y) > 0)
        {
            nonZeroMovementDirection.y = movementDirection.y;
        }
    }
    void HandleHeroMovement() {
        _rigidbody2D.velocity = new Vector2(movementDirection.x * speed, _rigidbody2D.velocity.y);

        if (!heroIsJumping && heroIsOnGround)
        {
            if (Mathf.Abs(movementDirection.x) > 0)
            {
                animatorController.Play(AnimationId.Run);

                nonZeroMovementDirection.x = movementDirection.x;
            }
            else
            {
                animatorController.Play(AnimationId.Idle);

            }
        }
    }


}
