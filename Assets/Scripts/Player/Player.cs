using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Rigidbody2D _myRigidiBody;

    [Header("Speed setup")]
    public float speed = 10;
    public float speedRun = 20;
    public float jumpForce = 15;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDuration = .2f;
    public Ease ease = Ease.Linear;
    public float landScaleX = 1.5f;
    public float landScaleY = .7f;
    public float landAnimationDuration = .2f;

    private Vector2 friction = new Vector2(-.3f, 0);
    private float _currentSpeed;
    private bool _isGrounded = false;

    private void Awake()
    {
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

        if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;

        if (Input.GetKey(KeyCode.A))
            _myRigidiBody.velocity = new Vector2(-_currentSpeed, _myRigidiBody.velocity.y);
        else if (Input.GetKey(KeyCode.D))
            _myRigidiBody.velocity = new Vector2(_currentSpeed, _myRigidiBody.velocity.y);

        if (_myRigidiBody.velocity.x > 0)
            _myRigidiBody.velocity += friction;
        else if (_myRigidiBody.velocity.x < 0)
            _myRigidiBody.velocity -= friction;

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myRigidiBody.velocity = Vector2.up * jumpForce;
            _myRigidiBody.transform.localScale = Vector2.one;
            DOTween.Kill(_myRigidiBody.transform);
            HandleScaleJump();
            _isGrounded = false;
        }
    }

    private void HandleScaleJump()
    {
        _myRigidiBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _myRigidiBody.transform.DOScaleX(jumpScaleX, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void HandleScaleLand()
    {
        _myRigidiBody.transform.localScale = Vector2.one;
        DOTween.Kill(_myRigidiBody.transform);
        _myRigidiBody.transform.DOScaleX(landScaleX, landAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
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
