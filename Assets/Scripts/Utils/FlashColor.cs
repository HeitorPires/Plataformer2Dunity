using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color flashColor = Color.red;
    public Color originalColor = Color.white;
    public float duration = .3f;

    private Tween _currentTween;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach(var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }


    public void Flash()
    {
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(i => i.color = originalColor);
        }

        foreach (var child in spriteRenderers)
        {
            _currentTween = child.DOColor(flashColor, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

}
