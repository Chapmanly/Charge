using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    NodeManager nodeManager;

    float Speed;

    [SerializeField]
    private float MinSpeed = 0f;
    [SerializeField]
    private float MaxSpeed = 2f;
    [SerializeField]
    private float accelerationRate = 0.1f;
    [SerializeField]
    private float decelerationPenaltyRate = .5f;

    float deadzone = 0.01f;

    [HideInInspector]
    public Vector3 moveDirection;
    [HideInInspector]
    public Vector3 InputDirection;
    Vector3 DesiredDirection;
    [SerializeField]
    private float nodeDistanceThreshold = 0.2f;

    public void AssignNodeManager(NodeManager nManager)
    {
        nodeManager = nManager;
    }

    bool intersecting = false;
    bool arrived = false;

    Vector3 TargetPosition;
    NodeBehaviour targetNode;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //REset
        ResetInputDirection();
        //GetInput
        HandlePlayerInput();
        //CompareDistance (if intersecting)
        CompareDistanceToNode();
        //GetNewDirection
        UpdatePlayerPosition();
        //AddSpeed

	}

    void ResetInputDirection()
    {
        InputDirection = Vector3.zero;
    }
    void HandlePlayerInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if (horizontal >= deadzone || horizontal <= -deadzone)
        {
            InputDirection += new Vector3(horizontal, 0, 0);
        }
        if (vertical >= deadzone || vertical <= -deadzone)
        {
            InputDirection += new Vector3(0, 0, vertical);
        }

        InputDirection.Normalize();
    }
    

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Node")
        {
            TargetPosition = other.transform.position;
            targetNode = other.GetComponent<NodeBehaviour>();
            intersecting = true;
            Debug.Log("We have entered " + targetNode.name);
        }
    }
    void OnTriggerExit(Collider other)
    {
       /* if(other.tag == "Node")
        {
            intersecting = false;
        }
        */
    }


    void AquireDirection(NodeBehaviour node)
    {
        DesiredDirection = node.DetermineNewDirection(moveDirection, InputDirection);

    }

    void CompareDistanceToNode()
    {
        if (intersecting && !arrived)
        {
            if (Vector3.Distance(transform.position, TargetPosition) < nodeDistanceThreshold)
            {
                AquireDirection(targetNode);
                transform.position = TargetPosition;
                moveDirection = DesiredDirection;
                arrived = true;
                Debug.Log("We are at the node");
            }
            
        }
        if (arrived)
        {
            if (transform.position == TargetPosition)
            {
                AquireDirection(targetNode);
                moveDirection = DesiredDirection;
                Debug.Log("Getting new Direction");
            }
            else
            {
                arrived = false;
                intersecting = false;
            }
        }

    }

    void UpdatePlayerPosition()
    {
        //This really only needs to do something when we hit an intersection
        //Maybe Compare it against move direction.

        updateSpeed();

        transform.position += moveDirection.normalized * Speed * Time.deltaTime;

    }
    void updateSpeed()
    {
        if (Speed > MaxSpeed) Speed = MaxSpeed;
        Speed += accelerationRate;
    }
}
