using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator grabAnimator;

    public InputActionProperty grabButton;

    void Start()
    {
       grabAnimator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(grabAnimator != null)
        {
            if(grabButton.action.IsPressed())
            {
                grabAnimator.SetTrigger("Grab");

            }
            if(grabButton.action.WasReleasedThisFrame())
            {
                grabAnimator.SetTrigger("NoGrab");

            }

        }
    }
}
