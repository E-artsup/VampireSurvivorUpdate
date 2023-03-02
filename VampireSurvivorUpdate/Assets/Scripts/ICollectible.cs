using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    /// <summary>
    /// Will trigger the corresponding Collect function for every ICollectible children
    /// </summary>
    public void Collect();
}
