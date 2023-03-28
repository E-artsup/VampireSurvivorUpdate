using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drop : MonoBehaviour
{
    [Tooltip("Only allowed names : blueXp, greenXp, redXp, smallGold, midGold, bigGold, magnet, heal, nuke(WIP)")] //TODO change the (WIP) next to nuke once it's done
    [SerializeField] private List<string> objectPoolNames;
    
    private int dropIndex;

    private void OnDisable() //Use this if you disable the object otherwise use OnDestroy()
    {
        OnDrop();
    }

    private void OnDestroy() //Use this if you destroy the object otherwise use OnDisable()
    {
        OnDrop();
    }

    /// <summary>
    /// When called, get an item from the corresponding pool and set the position of the item to the position of the obstacle
    /// </summary>
    private void OnDrop()
    {
        //Create another random number if you want to have a drop rate instead of a 100% drop rate
        
        dropIndex = Random.Range(0,objectPoolNames.Count); //Only works if the drop rate is the same for EVERY possible item to drop, otherwise a switch is needed
        
        Transform itemToDrop = CollectiblePool.instance.GetPoolObjectTransformByPoolName(objectPoolNames[dropIndex]); //Will be getting the transform of the item dropped
        
        itemToDrop.position = transform.position;
    }
}
