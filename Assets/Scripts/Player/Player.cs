using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidiBody;

    [Header("Speed setup")]
    public float speed = 10;
    public float speedRun = 20;
    public float jumpForce = 15;
    public Vector2 friction = new Vector2(-.3f, 0);

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDuration = .2f;
    public Ease ease = Ease.Linear;
    public float landScaleX = 1.5f;
    public float landScaleY = .7f;
    public float landAnimationDuration = .2f;
    public float playerSwipeDuration = .1f;

    [Header("Animator player")]
    public string boolWalk = "Walk";
    public string boolRun = "Run";
    public string boolJumpUp = "Jump Up";
    public string boolJumpDown = "Jump Down";
    public Animator animator;

    private bool _isGrounded = false;
    private float _currentSpeed;


    private void OnEnable()
    {
        if(_myRigidiBody == null)
            _myRigidiBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();

    }

    private void HandleMovement()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.SetBool(boolRun, true);

        }
        else
        {
            _currentSpeed = speed;
            animator.SetBool(boolRun, false);

        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (_myRigidiBody.transform.localScale.x != -1)
                _myRigidiBody.transform.DOScaleX(-1, playerSwipeDuration);
            _myRigidiBody.velocity = new Vector2(-_currentSpeed, _myRigidiBody.velocity.y);
            animator.SetBool(boolWalk, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (_myRigidiBody.transform.localScale.x != 1)
                _myRigidiBody.transform.DOScaleX(1, playerSwipeDuration);
            _myRigidiBody.velocity = new Vector2(_currentSpeed, _myRigidiBody.velocity.y);
            animator.SetBool(boolWalk, true);
        }
        else
            animator.SetBool(boolWalk, false);


        if (_myRigidiBody.velocity.x > 0)
            _myRigidiBody.velocity += friction;
        else if (_myRigidiBody.velocity.x < 0)
            _myRigidiBody.velocity -= friction;

    }

    

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetBool(boolJumpUp, true);
            _myRigidiBody.velocity = Vector2.up * jumpForce;
            DOTween.Kill(_myRigidiBody.transform);
            HandleScaleJump();
            _isGrounded = false;
        }

        /*else
            animator.SetBool(boolJumpUp, false);*/
    }

    private void HandleScaleJump()
    {
        _myRigidiBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _myRigidiBody.transform.DOScaleX(jumpScaleX * _myRigidiBody.transform.localScale.x, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void HandleScaleLand()
    {
        DOTween.Kill(_myRigidiBody.transform);
        _myRigidiBody.transform.DOScaleX(landScaleX * _myRigidiBody.transform.localScale.x, landAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _myRigidiBody.transform.DOScaleY(landScaleY, landAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if(!_isGrounded)
            {
                HandleScaleLand();
                _isGrounded = true;
            }
        }

    }

    



}
