using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class PathAngle
{
    public float dotProduct;
    public Vector3 direction;
    public PathAngle(Vector3 v, float dot)
    {
        dotProduct = dot;
        direction = v;
    }


}
public class NodeBehaviour : MonoBehaviour {
    NodeManager myManager;
    public List<Transform> AdjoiningNodes;

    public bool isIntersection = false;

    public bool haultPlayer = false;

    List<Vector3> availableDirections;

    float dotProductThreshold = 0.2f;
    
	// Use this for initialization
	void Awake ()
    {
        myManager = transform.parent.GetComponent<NodeManager>();
        availableDirections = GetNodeDirections();

	}
	
    List<Vector3> GetNodeDirections()
    {
        List<Vector3> dirs = new List<Vector3>();
        Vector3 tempDir;
        if(AdjoiningNodes.Count > 0)
        { 
            for(int i = 0; i < AdjoiningNodes.Count; i++)
            {
                tempDir = AdjoiningNodes[i].position - transform.position;
                dirs.Add(tempDir);
            }
        }

        return dirs;
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
    public Vector3 DetermineNewDirection(Vector3 moveDirection, Vector3 inputDirection)
    {
        Vector3 resultDirection = Vector3.zero;

        if(inputDirection.magnitude > 0.1f)    //base direction off of input
        {
            if(AdjoiningNodes.Count < 2)
            {
                resultDirection = availableDirections[0];
            }
            else
            {
                
                List<PathAngle> paths = new List<PathAngle>();
                foreach (Vector3 d in availableDirections)
                {
                    paths.Add(new PathAngle(d, Vector3.Angle(inputDirection, d)));
                }
                paths = paths.OrderBy(path => path.dotProduct).ToList();
                Debug.Log(paths[0].dotProduct + "Lowest Angle");
                resultDirection = paths[0].direction;
                /*for(int i = 0; i < availableDirections.Count; i++)
                {
                    if (Vector3.Dot(inputDirection, availableDirections[i]) > dotProductThreshold)
                    {
                        resultDirection = availableDirections[i];
                    }
                }*/
            }
        }
                                            //base direction off of default direction
        else
        {
            if (AdjoiningNodes.Count < 2)
            {
                resultDirection = availableDirections[0];
            }
            else if(AdjoiningNodes.Count == 2)
            {
                for (int i = 0; i < availableDirections.Count; i++)
                {
                    if (Vector3.Dot(moveDirection, availableDirections[i]) > dotProductThreshold)
                    {
                        resultDirection = availableDirections[i];
                    }
                }
            }
            else
            {
                resultDirection = Vector3.zero;
            }
        }
        

        
        return resultDirection;
    }
}
