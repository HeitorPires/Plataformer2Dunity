using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float duration = .5f;
    public float delay = .05f;
    public Ease ease = Ease.InOutQuart;

    private void OnEnable()
    {
        HideButtons();
        ShowwButtons();
    }

    private void ShowwButtons()
    {
        
        StartCoroutine(ShowButtonswithDelay());
    }

    private void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.transform.localScale = Vector3.zero;
            button.SetActive(false);
        }
    }

    IEnumerator ShowButtonswithDelay()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
            button.transform.DOScale(1, duration).SetEase(ease);
            yield return new WaitForSeconds(delay);
        }
    }
}
