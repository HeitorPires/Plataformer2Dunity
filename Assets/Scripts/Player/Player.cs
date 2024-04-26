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

    #region jump setups
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround;
    public ParticleSystem jumpVFX;
    private int numberOfJumpsLeft;
    #endregion

    private float _currentSpeed;

    private void OnEnable()
    {
        if(healthBase == null)
            healthBase = GetComponent<HealthBase>();

        if(_myRigidiBody == null)
            _myRigidiBody = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        ResetNumberOfJumps();

        if (healthBase != null)
        {
            healthBase.onKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if (collider2D != null)
            distToGround = collider2D.bounds.extents.y;

    }

    void Update()
    {
        HandleJump();
        HandleMovement();
    }
    

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
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
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            //animator.SetBool(boolJumpUp, true);
            _myRigidiBody.velocity = Vector2.up * soPlayerSetup.jumpForce;
            _myRigidiBody.transform.localScale = Vector2.one;

            DOTween.Kill(_myRigidiBody.transform);
            HandleScaleJump();
            PlayJumpVFX();
            numberOfJumpsLeft--;
        }

        /*else
            animator.SetBool(boolJumpUp, false);*/
    }

    private void PlayJumpVFX()
    {
        /*if(jumpVFX != null)
            jumpVFX.Play(); */
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
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
            HandleScaleLand();
            ResetNumberOfJumps();
        }

    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    

    private void ResetNumberOfJumps()
    {
        numberOfJumpsLeft = soPlayerSetup.maxNumberOfJumps;
    }


    private bool CanJump()
    {
        return  numberOfJumpsLeft > 0;
    }


    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

}
