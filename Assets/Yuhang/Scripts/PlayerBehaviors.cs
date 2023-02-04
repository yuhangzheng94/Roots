using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
    public GameObject playerCamera;
    public Transform cameraTransform;
    public float maxDistance = 2.0f;
    public float movingStep = 2.0f;
    public GameObject destinationFloor;
    
    private const int FORWARD = 0;

    private const int BACKWARD = 1;

    private const int LEFT = 2;

    private const int RIGHT = 3;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = playerCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RequestMoving(FORWARD);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            RequestMoving(BACKWARD);
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            RequestMoving(LEFT);
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            RequestMoving(RIGHT);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RequestTurning(LEFT);
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RequestTurning(RIGHT);
        }
        
    }

    void RequestMoving(int movingDirection)
    {
        // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        
        Vector3 v;
        if (movingDirection == FORWARD)
        {
            v = Vector3.forward;
        } else if (movingDirection == BACKWARD)
        {
            v = Vector3.back;
        } else if (movingDirection == LEFT)
        {
            v = Vector3.left;
        } else if (movingDirection == RIGHT)
        {
            v = Vector3.right;
        }
        else
        {
            Debug.Log("error: moving direction not specified");
            return;
        }

        Vector3 rayDirection = cameraTransform.TransformDirection(v);
        
        if (Physics.Raycast(cameraTransform.position, rayDirection, maxDistance))
        {
            Debug.Log("something in the way");
        }
        else
        {
            Debug.Log("ALL CLEAR!");
            MovePlayer(movingDirection);
        }
    }

    void RequestTurning(int turningDirection)
    {
        // https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
        
        if (turningDirection == LEFT)
        {
            playerCamera.transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
        } else if (turningDirection == RIGHT)
        {
            playerCamera.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        }
        else
        {
            Debug.Log("error: turning direction not specified");
            return;
        }
        
    }

    void MovePlayer(int movingDirection)
    {
        Vector3 v;
        if (movingDirection == FORWARD)
        {
            v = Vector3.forward;
        } else if (movingDirection == BACKWARD)
        {
            v = Vector3.back;
        } else if (movingDirection == LEFT)
        {
            v = Vector3.left;
        } else if (movingDirection == RIGHT)
        {
            v = Vector3.right;
        }
        else
        {
            Debug.Log("error: moving direction not specified");
            return;
        }
        
        cameraTransform.Translate(v * movingStep);
        CheckDestination();
    }

    void CheckDestination()
    {
        Vector2 des = new Vector2(destinationFloor.transform.position.x, destinationFloor.transform.position.y);
        Vector2 currentPos = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.y);

        Debug.Log("current distance: " + Vector2.Distance(des, currentPos));
        
        if ( Vector2.Distance(des, currentPos) <= 1.5f )
        {
            Debug.Log("GAME CLEAR!");
        }
    }
    
}
