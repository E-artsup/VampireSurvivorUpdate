using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationWithRigidbody : MonoBehaviour
{
    //=======
    //VARIABLE
    //=======
    [SerializeField] private Animator animator;
    [SerializeField] private new Rigidbody rigidbody;

    //=======
    //MONOBEHAVIOR
    //=======
    private void Update()
    {
        float velocity = Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z);
        if (velocity > 1) velocity = 1;
        animator.SetFloat("velocity", velocity);
    }
}
