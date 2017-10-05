using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {

    //Determnes how collision should be checked
    enum collisionMode { AABB, Circle};

    //By default the collision checking mode is set to AABB
    collisionMode currentMode = collisionMode.AABB;

    //A list holding all of the gameobject planets and their renderers.
    List<GameObject> planets;
    List<SpriteRenderer> planetRenderers;

    public GameObject player;
    SpriteRenderer playerRenderer;

    float playerRadius;
    float planetRadius;

   
     
	void Start () { 

       
        //Get all of the planets in the screen and store them
        planets = new List<GameObject>();
        planetRenderers = new List<SpriteRenderer>();
        planets.AddRange(GameObject.FindGameObjectsWithTag("Planet"));
        
       //Store the sprite renderer of each of the stored planets
        foreach (GameObject planet in planets)
        {
            planetRenderers.Add(planet.GetComponent<SpriteRenderer>());
        }

        //Store the player in the scene
        player = GameObject.FindGameObjectWithTag("player");
        playerRenderer = player.GetComponent<SpriteRenderer>();

        planetRadius = planetRenderers[0].bounds.extents.x;
        playerRadius = (playerRenderer.bounds.extents.x + playerRenderer.bounds.extents.y)/2;
     
	}

    // Update is called once per frame
    void Update() {

        //If the ship's color isnt white, set it to white by default
        if(playerRenderer.color!= Color.white)
        {
            playerRenderer.color = Color.white;
        }
      
        //Give the opportunity for the user to switch collision modes
        ChangeCollision();

        //Check for collision of the apropriate collision type
        //depending on the current mode
        if (currentMode == collisionMode.AABB)
        {
            AABB_Collision();
        }

        if( currentMode == collisionMode.Circle)
        {
            CircleCollision();
        }
    }

    void ChangeCollision()
    {
        playerRenderer.color = Color.white;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = collisionMode.AABB;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = collisionMode.Circle;
        }
    }

    //Check collision based on the rectangular sprites around planets and the player
    void AABB_Collision()
    {
        //For each planet, check to see if the rectangular bounds of the sprite containing the 
        //asteroid intersect the bounds of the player's sprite.
        foreach (SpriteRenderer planetRenderer in planetRenderers)
        {
            //Reset the color of the planet
            if(planetRenderer.color != Color.white)
            {
                planetRenderer.color = Color.white;
            }

           
            if (playerRenderer.bounds.min.x < planetRenderer.bounds.max.x)
            {
                if (playerRenderer.bounds.max.x > planetRenderer.bounds.min.x)
                {
                    if (playerRenderer.bounds.min.y < planetRenderer.bounds.max.y)
                    {
                        if (playerRenderer.bounds.max.y > planetRenderer.bounds.min.y)
                        {
                            //If the planet intersects the player, change both of their colors to red. 
                            planetRenderer.color = Color.red;
                            playerRenderer.color = Color.red;
                        }

                    }
               

                }

            }

        }
    }
    
    //foreach asteroid, if the radius of that planet intersects the radius of the player, turn both of them blue
    void CircleCollision()
    {
        foreach (SpriteRenderer renderer in planetRenderers)
        {
            if ((playerRenderer.bounds.center - renderer.bounds.center).magnitude < (playerRadius + planetRadius) )
            {
                playerRenderer.color = Color.blue;
                renderer.color = Color.blue;
        
            }
            else
            {
                renderer.color = Color.white;
            }
        }
    }
    
}
