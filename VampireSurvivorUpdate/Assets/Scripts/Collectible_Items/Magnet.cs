using System;
using UnityEngine;

namespace Collectible_Items
{
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
}
