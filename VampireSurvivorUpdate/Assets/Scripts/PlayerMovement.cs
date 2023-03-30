using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    [Tooltip("This will adjust the movement speed of the player in all axis")]
    public float speed = 2; //Could be placed in the Player Stats script if needed but this will need to be get here

    [Header("Private variables")]
    private Rigidbody rb; //Used for the movement
    private float speedModifier = 1f; //Should be used for speed upgrades/modifiers instead of directly modifying the speed of the player. (If the speed variable is still in this script and not in the Player Stats)
    
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// Will move the player at a certain speed and in the direction pressed by the player
    /// </summary>
    private void Movement()
    {
        rb.velocity = new Vector3(InputManager.instance.move.ReadValue<Vector2>().normalized.x * (speed * speedModifier), 0,
                InputManager.instance.move.ReadValue<Vector2>().normalized.y * (speed * speedModifier));
        if(rb.velocity == Vector3.zero) SoundManager.instance.playSound("Character_Movement");
    }
    
}
