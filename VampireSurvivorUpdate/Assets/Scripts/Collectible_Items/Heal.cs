using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heal : MonoBehaviour, ICollectible
{

    public static event Action OnHealCollected;
    
    public void Collect()
    {
        OnHealCollected?.Invoke();
        CollectiblePool.instance.healPool.Release(this);
    }
}
