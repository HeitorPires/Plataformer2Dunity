using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SOPlayer : ScriptableObject
{

    public Animator player;

    #region speed Setup
    [Header("Speed setup")]
    public float speed = 10;
    public float speedRun = 20;
    public float jumpForce = 15;
    public Vector2 friction = new Vector2(-.3f, 0);
    #endregion

    #region Animation setup
    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDuration = .2f;
    public float landScaleX = 1.5f;
    public float landScaleY = .7f;
    public float landAnimationDuration = .2f;
    public Ease ease = Ease.Linear;
    public float playerSwipeDuration = .1f;
    #endregion

    #region Animator player
    [Header("Animator player")]
    public string boolWalk = "Walk";
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    #endregion

    #region Jump Setup
    [Header("Jump setup")]
    public int maxNumberOfJumps;
    #endregion
}
