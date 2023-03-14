using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour, ICollectible
{
    [Tooltip("The amount of XP that this collectible will give to the player")]
    [SerializeField] private int xpAmount;

    public delegate void XPCollected(int xpAmount);
    public static event XPCollected OnXPCollected;
    
    public void Collect()
    {
        OnXPCollected?.Invoke(xpAmount);
        
        switch (xpAmount)
        {
            case 1:
                CollectiblePool.instance.blueXpPool.Release(this);
                break;
            case 3:
                CollectiblePool.instance.greenXpPool.Release(this);
                break;
            case 20:
                CollectiblePool.instance.redXpPool.Release(this);
                break;
            default:
                break;
        }
    }
}
