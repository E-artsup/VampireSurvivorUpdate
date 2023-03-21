using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //=========
    //VARIABLE
    //=========
    [SerializeField] private float rotationSpeed = 10;
    //=========
    //MONEBEHAVIOUR
    //=========
    private void Update()
    {
        //Get Input
        Vector3 input = new(InputManager.instance.move.ReadValue<Vector2>().normalized.x, 0, InputManager.instance.move.ReadValue<Vector2>().normalized.y);
        if (input == Vector3.zero) return;

        //Rotate PLayer to Input
        Quaternion toRotation = Quaternion.LookRotation(input, Vector3.up);

        //Smooth Rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
    }
}
