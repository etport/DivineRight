using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS; 

public class UserInput : MonoBehaviour 
{
    #region Fields
    Player player;
    #endregion


    #region Private Methods 
    ///<summary>
    /// Use this for initialization
    ///</summary>
    void Start ()
	{
        player = transform.root.GetComponent<Player>(); 
	}
	
	///<summary>
	/// Update is called once per frame
	///</summary>
	void Update ()
	{
	    if (player.human)
        {
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }	
	}

    void MoveCamera()
    {
        float xPosition = Input.mousePosition.x;
        float yPosition = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement throught edge screen scrolling
        //if mouseX position is between 0 and 15 pixels, move in the negative (left) direction
        if ((xPosition >= 0 && xPosition < ResourceManager.ScrollWidth) || Input.GetAxis("Horizontal") < 0)
        {
            movement.x -= ResourceManager.ScrollSpeed; 
        }
        // if the mouseX position is between the edge tolerance and the right side of screen move in the positive (right) direction
        else if ((xPosition <=Screen.width && xPosition > Screen.width - ResourceManager.ScrollWidth) || Input.GetAxis("Horizontal") > 0)
        {
            movement.x += ResourceManager.ScrollSpeed; 
        }

        //vertical camera movement through edge screen scrolling

        //if mouseY position is between 0 and 15 pixels on the bottom move camera in the negative Y (back) direction
        if ((yPosition >= 0 && yPosition < ResourceManager.ScrollWidth) || Input.GetAxis("Vertical") < 0)
        {
            movement.z -= ResourceManager.ScrollSpeed;
        }
        // if the mouseY position is between the top of the screen and the max scroll pixels move the camera in the positive y (forward) direction
        else if ((yPosition <= Screen.height && yPosition > Screen.height - ResourceManager.ScrollWidth) || Input.GetAxis("Vertical") > 0)
        {
            movement.z += ResourceManager.ScrollSpeed;
        }
        //movement panning, constrain height
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //movement zoom 
        Vector3 forwardVector = Camera.main.transform.forward; 
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            movement = forwardVector * ResourceManager.ScrollSpeed; 
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            movement = -forwardVector * ResourceManager.ScrollSpeed;
        }
        
        //calculate camera destination 
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z; 

        //clamp zoom 
        if (destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        } else if (destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //update position 
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin; 

        //detect rotation amount if correct input keys are down 
        if (Input.GetKey(KeyCode.Mouse2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateSpeed;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateSpeed; 
        }
        else if (Input.GetAxis("Rotate") != 0)
        {
            Vector3 rotationAxis = new Vector3(0, 1, 0); 
            
            if (Input.GetAxis("Rotate") > 0)
            {
                Camera.main.transform.Rotate(rotationAxis, Space.World); 
                
            }
            else
            {

                Camera.main.transform.Rotate(-rotationAxis, Space.World);

            }
        }

        //update rotation 
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }

    void MouseActivity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseClick(); 
        } else if (Input.GetMouseButton(1))
        {
            RightMouseClick(); 
        }
    }

    void LeftMouseClick()
    {
        if (player.hud.MouseInBounds())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint(); 
            if (hitObject && hitPoint != ResourceManager.InvalidPosition)
            {
                if (player.SelectedObject)
                {
                    player.SelectedObject.MouseClick(hitObject, hitPoint, player); 
                }
                else if (hitObject.name != "Ground")
                {
                    WorldObject worldObject = hitObject.transform.root.GetComponent<WorldObject>();
                    if (worldObject)
                    {
                        player.SelectedObject = worldObject;
                        worldObject.SetSelected(true, player.hud.GetPlayingArea()); 
                    }
                }
            }
        }
    }
    //Deselect on right click
    void RightMouseClick()
    {
        if (player.hud.MouseInBounds() && player.SelectedObject)
        {
            player.SelectedObject.SetSelected(false, player.hud.GetPlayingArea());
            player.SelectedObject = null;
        }
    }

    GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject; 
        }
        return null; 
    }

    Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point; 
        }
        return ResourceManager.InvalidPosition; 
    }
    #endregion
}
