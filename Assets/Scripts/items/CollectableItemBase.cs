using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemBase : MonoBehaviour
{
    private bool _canCollect = true;
    public string playerTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToDestroy = 3f;
    public GameObject graphicItem;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canCollect && collision.gameObject.CompareTag(playerTag))
        {
            _canCollect = false;
            Collect();
        }
    }

    protected virtual void Collect() 
    {
        OnCollect();
    }

    private void HideObject()
    {
        if (graphicItem != null)
            graphicItem.gameObject.SetActive(false);
    }

    private void PlayParticleSystem()
    {
        
        if(particleSystem != null)
            particleSystem.Play();

    }

    protected virtual void OnCollect() 
    { 
        HideObject();
        PlayParticleSystem();
        Destroy(gameObject, timeToDestroy);
    }
}
