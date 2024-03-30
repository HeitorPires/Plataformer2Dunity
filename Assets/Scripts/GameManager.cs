using UnityEngine;
using Core.Singleton;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;
    
    [Header("Enemys")]
    public List<GameObject> Enemies;

    [Header("References")]
    public Transform startPoint;

    [Header("Animation")]
    public float duration = .5f;
    public float delay = .05f;
    public Ease ease = Ease.InOutQuart;

    private GameObject _currentPlayer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From();
    }
}
