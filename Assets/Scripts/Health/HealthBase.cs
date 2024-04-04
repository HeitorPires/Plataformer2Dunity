using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{

    public Action onKill;

    public float startLife = 10;

    public bool destroyOnKill = true;
    public float delayToKill = .5f;

    private float _currentLife;
    private bool _isDead;


    [SerializeField] private FlashColor _flashColor;

    private void Awake()
    {
        Init();
        if(_flashColor == null)
            _flashColor = GetComponent<FlashColor>();
    }

    private void Init()
    {
        _currentLife = startLife;
        _isDead = false;
    }

    public void Damage(int damage)
    {
        if(_isDead) return;

        _currentLife -= damage;

        if( _currentLife <= 0 )
        {
            Kill();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if(destroyOnKill )
        {
            Destroy(gameObject, delayToKill);
        }

        onKill?.Invoke();
    }
}
