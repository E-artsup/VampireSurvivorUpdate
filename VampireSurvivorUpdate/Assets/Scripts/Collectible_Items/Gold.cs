using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, ICollectible
{
    [Tooltip("The amount of Gold that this collectible will give to the player")]
    [SerializeField] private int goldAmount;

    public delegate void GoldCollected(int goldAmount);
    public static event GoldCollected OnGoldCollected;
    
    public void Collect()
    {
        OnGoldCollected?.Invoke(goldAmount);

        switch (goldAmount)
        {
            case 1:
                CollectiblePool.instance.smallGoldPool.Release(this);
                break;
            case 25:
                CollectiblePool.instance.midGoldPool.Release(this);
                break;
            case 100:
                CollectiblePool.instance.bigGoldPool.Release(this);
                break;
            default:
                break;
        }
    }
}
