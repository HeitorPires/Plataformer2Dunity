using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public HealthBase healthBase;
    private Rigidbody2D _myRigidiBody;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;

    #region Animator player
    [Header("Animator player")]
    private Animator _currentPlayer;
    #endregion

    private bool _isGrounded = false;
    private float _currentSpeed;


    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.onKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
    }

    private void OnEnable()
    {
        if(healthBase == null)
            healthBase = GetComponent<HealthBase>();

        if(_myRigidiBody == null)
            _myRigidiBody = GetComponent<Rigidbody2D>();
    }

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
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
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);

        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);

        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (_myRigidiBody.transform.localScale.x != -1)
                _myRigidiBody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            _myRigidiBody.velocity = new Vector2(-_currentSpeed, _myRigidiBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolWalk, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (_myRigidiBody.transform.localScale.x != 1)
                _myRigidiBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            _myRigidiBody.velocity = new Vector2(_currentSpeed, _myRigidiBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolWalk, true);
        }
        else
            _currentPlayer.SetBool(soPlayerSetup.boolWalk, false);


        if (_myRigidiBody.velocity.x > 0)
            _myRigidiBody.velocity += soPlayerSetup.friction;
        else if (_myRigidiBody.velocity.x < 0)
            _myRigidiBody.velocity -= soPlayerSetup.friction;

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetBool(boolJumpUp, true);
            _myRigidiBody.velocity = Vector2.up * soPlayerSetup.jumpForce;
            DOTween.Kill(_myRigidiBody.transform);
            HandleScaleJump();
            _isGrounded = false;
        }

        /*else
            animator.SetBool(boolJumpUp, false);*/
    }

    private void HandleScaleJump()
    {
        _myRigidiBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        _myRigidiBody.transform.DOScaleX(soPlayerSetup.jumpScaleX * _myRigidiBody.transform.localScale.x, soPlayerSetup.jumpAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    private void HandleScaleLand()
    {
        DOTween.Kill(_myRigidiBody.transform);
        _myRigidiBody.transform.DOScaleX(soPlayerSetup.landScaleX * _myRigidiBody.transform.localScale.x, soPlayerSetup.landAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        _myRigidiBody.transform.DOScaleY(soPlayerSetup.landScaleY, soPlayerSetup.landAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);

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

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
