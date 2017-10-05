using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vehicle class
// Placed on the Tank prefab
// Move a vehicle around the screen

public class Vehicle : MonoBehaviour 
{

    // Fields
    //[SerializeField]					// private field but shows in the inspector

    private Vector3 vehiclePosition = Vector3.zero; // position of the vehicle in a given frame

    public Vector3 direction = Vector3.up;           // way the object is facing

    public Vector3 velocity = Vector3.zero;

    Vector3 acceleration = Vector3.zero;

    float accelerationRate = 0.003f; 

    float decceleration = .95f;


    float maxSpeed = 1f;
    // Use this for initialization


    public Camera mainCamera;
	void Start () 
	{
		// Capture the vehicle's starting position
	
		vehiclePosition = transform.position;

   

	}
	
	// Update is called once per frame
	void Update () 
	{


        if (Input.GetKey(KeyCode.I))
        {
            //Calculate the vehicles acceleration vector
            acceleration = direction * accelerationRate;

            //Add the acceleration to the velocity
            velocity += acceleration;
        }
        //If the user isnt holding the buttton, cause the vehicle to deccelerate
        else
        {
            velocity *= decceleration;
        }
      



        //Cap off the velocity so that the vehicle can't move too fast
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //Move the vehicle using the velocity vector.
        vehiclePosition += velocity;




        //Set the position.
        transform.position = vehiclePosition;

        screenWrap();

    }

    //Check to see if the vehicle is outside of the camera's range. If it is, invert its position across the origin.
    void screenWrap()
    {

        if (transform.position.x > mainCamera.orthographicSize*(mainCamera.aspect)|| transform.position.x < -(mainCamera.orthographicSize * ( mainCamera.aspect)))
        {
            vehiclePosition = new Vector3(-transform.position.x, transform.position.y, 0);
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }

        if (transform.position.y > mainCamera.orthographicSize || transform.position.y < -(mainCamera.orthographicSize))
        {
            vehiclePosition = new Vector3(transform.position.x, -transform.position.y, 0);
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0);
        }

    }

}




