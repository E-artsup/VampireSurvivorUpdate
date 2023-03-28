using System;
using UnityEngine;

namespace Collectible_Items
{
    public class Heal : MonoBehaviour, ICollectible
    {

        public static event Action OnHealCollected;
    
        public void Collect()
        {
            OnHealCollected?.Invoke();
            CollectiblePool.instance.healPool.Release(this);
        }
    }
}
