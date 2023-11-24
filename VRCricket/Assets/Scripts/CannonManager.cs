using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonManager : MonoBehaviour
{

    public GameObject cricketBallPrefab;

    //point at where the cricketBallPrefab shoots
    public Transform firePoint;

    //used to get player location   ***  TEMPORARY  ***  reference player directly using tag 
    public Transform player;

    //button used to shoot ball 
    public InputActionProperty shootBall;

    //ball shhpeed
    private Vector3 _initialVelocity;

    public float velocityMultiplier; //temporary 


    private int ballCount = 0;
    public int maxBallCount = 5; // Maximum number of balls allowed in the scene

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned. Please assign a camera in the Inspector.");
            return;
        }


        if (shootBall.action.WasPressedThisFrame())
        {
            _initialVelocity = (player.position - firePoint.position) * velocityMultiplier;
            _Fire();
            ballCount++;
        }


        // Check if the maximum ball count is reached, then destroy all balls in the scene
        if (ballCount > maxBallCount)
        {
            DestroyAllBalls();
        }

    }

    private void _Fire()
    {
        //idk what quaternion identity is ... something to do with rotation 
        // spawns a cricketball
        GameObject cricketBall = Instantiate(cricketBallPrefab, firePoint.position, Quaternion.identity);

        //apply force 
        Rigidbody rb = cricketBall.GetComponent<Rigidbody>();

        rb.AddForce(_initialVelocity, ForceMode.Impulse);  //no idea what ForceMode.Impulse is 
    }

    // Called when a ball is destroyed

    // Destroy all balls in the scene
    private void DestroyAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("CricketBall"); // Adjust the tag based on your setup
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }

        // Reset the ball count
        ballCount = 0;
    }


}


