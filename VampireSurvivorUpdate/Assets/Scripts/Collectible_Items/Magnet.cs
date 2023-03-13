using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour, ICollectible
{
    
    //public delegate void MagnetCollected(int xpAmount);
    public static event Action OnMagnetCollected;
    
    public void Collect()
    {
        OnMagnetCollected?.Invoke();
        CollectiblePool.instance.magnetPool.Release(this);
    }
}
