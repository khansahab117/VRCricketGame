// using UnityEngine;
// using System.Collections;
// using System;

// public class HandAnimatorManagerVR : MonoBehaviour
// {
// 	public StateModel[] stateModels;
// 	Animator handAnimator;

// 	public int currentState = 100;
// 	int lastState = -1;

// 	public bool action = false;
// 	public bool hold = false;

// 	//trackpad keys 8 or 9
// 	public string changeKey = "joystick button 9";
// 	//trigger keys 14 or 15
// 	public string actionKey = "joystick button 15";

// 	//grip axis 11 or 12
// 	public string holdKey = "Axis 12";

// 	public int numberOfAnimations = 8;

// 	// Use this for initialization
// 	void Start ()
// 	{
// 		string[] joys = UnityEngine.Input.GetJoystickNames ();
// 		foreach (var item in joys) {
// 			Debug.Log (item);
// 		}
// 		handAnimator = GetComponent<Animator> ();
// 	}
	
// 	// Update is called once per frame
// 	void Update ()
// 	{
// 		if (Input.GetKeyUp (changeKey)) {
// 			currentState = (currentState + 1) % (numberOfAnimations + 1);
// 		}

// 		if (Input.GetAxis (holdKey) > 0) {
// 			hold = true;
// 		} else
// 			hold = false;

// 		if (Input.GetKey (actionKey)) {
// 			action = true;
// 		} else
// 			action = false;


// 		if (lastState != currentState) {
// 			lastState = currentState;
// 			handAnimator.SetInteger ("State", currentState);
// 			TurnOnState (currentState);
// 		}

// 		handAnimator.SetBool ("Action", action);
// 		handAnimator.SetBool ("Hold", hold);

// 	}

// 	void TurnOnState (int stateNumber)
// 	{
// 		foreach (var item in stateModels) {
// 			if (item.stateNumber == stateNumber && !item.go.activeSelf)
// 				item.go.SetActive (true);
// 			else if (item.go.activeSelf)
// 				item.go.SetActive (false);
// 		}
// 	}


// }
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimatorManagerXR : MonoBehaviour
{
    public StateModel[] stateModels;
    Animator handAnimator;

    public int currentState = 100;
    int lastState = -1;

    public bool action = false;
    public bool hold = false;

    // Define your InputActions here
    public InputActionReference changeAction;
    public InputActionReference actionAction;
    public InputActionReference holdAction;

    public int numberOfAnimations = 8;

    void Start()
    {
        handAnimator = GetComponent<Animator>();

        // Subscribe to input events
        changeAction.action.performed += OnChangeAction;
        actionAction.action.performed += OnActionAction;
        actionAction.action.canceled += OnActionRelease;
        holdAction.action.performed += OnHoldAction;

        changeAction.action.Enable();
        actionAction.action.Enable();
        holdAction.action.Enable();
    }

    void OnChangeAction(InputAction.CallbackContext context)
    {
        currentState = (currentState + 1) % (numberOfAnimations + 1);
    }

    void OnActionAction(InputAction.CallbackContext context)
    {
        action = context.ReadValueAsButton();

        // Check if the action button is pressed
        if (action)
        {
            handAnimator.SetBool("Action", true);
            handAnimator.SetBool("Hold", hold);
            TurnOnState(currentState);
        }
    }

    void OnActionRelease(InputAction.CallbackContext context)
    {
        // When the action button is released, stop the animation and set parameters to false
        handAnimator.SetBool("Action", false);
        handAnimator.SetBool("Hold", false);
        TurnOffAllStates();
    }

    void OnHoldAction(InputAction.CallbackContext context)
    {
        hold = context.ReadValue<float>() > 0.5f;

        // Update the hold state if the action button is pressed
        if (action)
        {
            handAnimator.SetBool("Hold", hold);
        }
    }

    void Update()
    {
        // No need for animation updates here
    }

    void TurnOnState(int stateNumber)
    {
        foreach (var item in stateModels)
        {
            if (item.stateNumber == stateNumber && !item.go.activeSelf)
                item.go.SetActive(true);
            else if (item.go.activeSelf)
                item.go.SetActive(false);
        }
    }

    void TurnOffAllStates()
    {
        foreach (var item in stateModels)
        {
            if (item.go.activeSelf)
                item.go.SetActive(false);
        }
    }

    void OnDisable()
    {
        // Unsubscribe from input events
        changeAction.action.performed -= OnChangeAction;
        actionAction.action.performed -= OnActionAction;
        actionAction.action.canceled -= OnActionRelease;
        holdAction.action.performed -= OnHoldAction;

        changeAction.action.Disable();
        actionAction.action.Disable();
        holdAction.action.Disable();
    }
}

