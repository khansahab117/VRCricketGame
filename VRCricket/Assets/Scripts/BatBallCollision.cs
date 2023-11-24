using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BatBallCollision : MonoBehaviour
{
    [Range(0, 1)]
    public float vibrationIntensity;
    public float vibrationDuration;


    [SerializeField] XRBaseController leftController;

    // This function is called when the Collider other enters the collision.
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the ball
        if (collision.gameObject.CompareTag("CricketBall"))
        {
            leftController.SendHapticImpulse(vibrationIntensity, vibrationDuration);
        }
        if (collision.gameObject.CompareTag("debugBall"))
        {
            leftController.SendHapticImpulse(vibrationIntensity, vibrationDuration);
        }
    }
}