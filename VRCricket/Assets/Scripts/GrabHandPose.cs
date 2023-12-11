using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData leftHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;

    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;




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

            SetHandDataValues(handData, leftHandPose);

            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);

            // if (handData == null)
            // {
            //     gameObject.SetActive(false);
            // }
        }

    }


    public void SetHandDataValues(HandData h1, HandData h2)
    {
        // startingHandPosition = new Vector3( 
        //     h1.root.localPosition.x / h1.root.localScale.x,
        //     h1.root.localPosition.y / h1.root.localScale.y, 
        //     h1.root.localPosition.z / h1.root.localScale.z);

        // finalHandPosition = new Vector3( 
        //     h2.root.localPosition.x / h2.root.localScale.x,
        //     h2.root.localPosition.y / h2.root.localScale.y, 
        //     h2.root.localPosition.z / h2.root.localScale.z);

        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }

    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = Quaternion.Inverse(newRotation);//newRotation; 

        // Invert only the rotation around the y-axis
        
        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

}
