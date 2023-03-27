using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    //=======
    //VARIABLE
    //=======
    [SerializeField] private Animator animator;
    private Vector3 oldPosition = Vector3.zero;
    private float acceleration = 0.3f;
    //=======
    //MONOBEHAVIOR
    //=======
    private void Update()
    {
        float desireVelocity = ((transform.position - oldPosition).magnitude) / Time.deltaTime;
        oldPosition = transform.position;
        if (desireVelocity > 1) desireVelocity = 1;
        float velocity = Mathf.Lerp(animator.GetFloat("velocity"), desireVelocity, acceleration);
        animator.SetFloat("velocity", velocity);
    }
}
