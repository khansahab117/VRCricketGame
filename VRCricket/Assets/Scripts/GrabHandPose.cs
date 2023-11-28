using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData leftHandPose;
    public GameObject XRLeftHand;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);

        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {

        if (arg.interactorObject is XRDirectInteractor)
        {
            // gameObject.SetActive(false);
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            //HandData handData = XRLeftHand.GetComponentInChildren<HandData>();

            handData.animator.enabled = false;

            
            if (handData == null)
            {
                gameObject.SetActive(false);
            }
        }

    }

}
