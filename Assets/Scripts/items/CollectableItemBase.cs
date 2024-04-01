using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemBase : MonoBehaviour
{

    public string compareTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(compareTag))
            Collect();
    }

    protected virtual void Collect() 
    {
        OnCollect();
        Destroy(gameObject);
    }

    protected virtual void OnCollect() { }
}
