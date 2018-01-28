using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {
    PlayerController plyr;

    List<NodeBehaviour> NodeObjects;

    public NodeBehaviour startNode;
    public NodeBehaviour endNode;



    void Awake()
    {
        plyr = GameObject.Find("Player").GetComponent<PlayerController>();
        if(plyr != null)
        {
            plyr.AssignNodeManager(this);
        }
        NodeObjects = new List<NodeBehaviour>();
        foreach(Transform t in transform)
        {
            NodeObjects.Add(t.GetComponent<NodeBehaviour>());
        }
    }

    // Use this for initialization
    void Start ()
    {
        PlayerStart();
	}

    public void PlayerStart()
    {
        plyr.transform.position = startNode.transform.position;
        plyr.moveDirection = startNode.DetermineNewDirection(Vector3.zero, Vector3.zero);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
