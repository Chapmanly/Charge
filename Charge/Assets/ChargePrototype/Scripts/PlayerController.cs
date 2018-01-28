using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    NodeManager nodeManager;
    public LayerMask nodeMask;
    [SerializeField]
    float Speed = 2;

    [SerializeField]
    private float MinSpeed = 2f;
    [SerializeField]
    private float MaxSpeed = 20f;
    [SerializeField]
    private float accelerationRate = 0.055f;
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
        //AddSpeed

        UpdatePlayerPosition();
	}
    void FixedUpdate()
    {
        
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

        if(other.tag == "Short")
        {

        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Node")
        {
            intersecting = false;
        }
        
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
                targetNode.ShowGuide(true);
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
                targetNode.ShowGuide(false);
                arrived = false;
                intersecting = false;
            }
        }

    }

    void UpdatePlayerPosition()
    {
        //This really only needs to do something between an intersection
        //Maybe Compare it against move direction.

        updateSpeed();

        transform.position += moveDirection.normalized * Speed * Time.fixedDeltaTime;
        /*if (targetNode != null && !VerifyNode())
        {
            transform.position = TargetPosition;
        }*/

    }
    bool VerifyNode()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, moveDirection.normalized, out hitinfo, 30f, nodeMask))
        {
            if(hitinfo.transform.tag == "node")
            {
                return true;
            }
        }
        return false;
    }
    void updateSpeed()
    {
        if (!intersecting)
        {
            Speed = Mathf.MoveTowards(Speed, MaxSpeed, accelerationRate);
            Speed = Mathf.Clamp(Speed,MinSpeed, MaxSpeed);

        }
    }
  


    void TallyEnergyValue()
    {

    }
}
