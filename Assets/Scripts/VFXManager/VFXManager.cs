using Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2
    }

    public List<VFXManagerSetup> vfxSetup;

    public void PlayVFXByType(VFXType type, Vector3 position)
    {
        foreach (var i in vfxSetup) 
        {
            if(i.vfxType == type)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item, 2f);
                break;
            }
        }
    }

}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}