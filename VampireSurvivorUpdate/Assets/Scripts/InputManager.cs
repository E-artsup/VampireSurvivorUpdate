using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public PlayerInputActions playerInputActions;

    [Header("Player")] 
    [HideInInspector] public InputAction move;

    [Header("UI")]
    [HideInInspector] public InputAction navigate;
    [HideInInspector] public InputAction submit;
    [HideInInspector] public InputAction cancel;
    [HideInInspector] public InputAction point;
    [HideInInspector] public InputAction click;
    [HideInInspector] public InputAction scrollWheel;
    [HideInInspector] public InputAction middleClick;
    [HideInInspector] public InputAction rightClick;
    [HideInInspector] public InputAction trackedDevicePosition;
    [HideInInspector] public InputAction trackedDeviceOrientation;




    void Awake()
    {
        instance = this;
        playerInputActions = new PlayerInputActions();
        
        InitUIInputs();
        //OnEnableUIInputs();

        InitPlayerInputs();
        OnEnablePlayerInputs(); //TODO - Comment this line before build
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Init all fo the Inputs int the UI map from the custom PlayerInputActions
    /// </summary>
    public void InitUIInputs()
    {
        navigate = playerInputActions.UI.Navigate;
        submit = playerInputActions.UI.Submit;
        cancel = playerInputActions.UI.Cancel;
        point = playerInputActions.UI.Point;
        click = playerInputActions.UI.Click;
        scrollWheel = playerInputActions.UI.ScrollWheel;
        middleClick = playerInputActions.UI.MiddleClick;
        rightClick = playerInputActions.UI.RightClick;
        trackedDevicePosition = playerInputActions.UI.TrackedDevicePosition;
        trackedDeviceOrientation = playerInputActions.UI.TrackedDevicePosition;
    }
    
    /// <summary>
    /// Used to enable all of the Inputs in the UI map from the custom PlayerInputActions
    /// </summary>
    public void OnEnableUIInputs()
    {
        navigate.Enable();
        submit.Enable();
        cancel.Enable();
        point.Enable();
        click.Enable();
        scrollWheel.Enable();
        middleClick.Enable();
        rightClick.Enable();
        trackedDevicePosition.Enable();
        trackedDeviceOrientation.Enable();
    }
    
    /// <summary>
    /// Used to disable all of the Inputs in the UI map from the custom PlayerInputActions
    /// </summary>
    public void OnDisableUIInputs()
    {
        navigate.Disable();
        submit.Disable();
        cancel.Disable();
        point.Disable();
        click.Disable();
        scrollWheel.Disable();
        middleClick.Disable();
        rightClick.Disable();
        trackedDevicePosition.Disable();
        trackedDeviceOrientation.Disable();
    }


    /// <summary>
    /// Init all fo the Inputs int the Player map from the custom PlayerInputActions
    /// </summary>
    public void InitPlayerInputs()
    {
        move = playerInputActions.Player.Move;
    }

    /// <summary>
    /// Used to enable all of the Inputs in the Player map from the custom PlayerInputActions
    /// </summary>
    public void OnEnablePlayerInputs()
    {
        move.Enable();
    }

    /// <summary>
    /// Used to disable all of the Inputs in the Player map from the custom PlayerInputActions
    /// </summary>
    public void OnDisablePlayerInputs()
    {
        move.Disable();
    }
}
