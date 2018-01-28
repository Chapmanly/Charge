using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGuide : MonoBehaviour {

    //public bool ShowArrows = false;

    List<SpriteRenderer> arrowRenderers;
    bool previousDisplay;

    bool R, L, U, D;
	// Use this for initialization
	void Awake () {
        arrowRenderers = new List<SpriteRenderer>();
        foreach (Transform child in transform)
        {
            arrowRenderers.Add(child.GetComponent<SpriteRenderer>());               
        }

        HideArrowTip(false);
    }
	
    public void HideArrowTip(bool show)
    {
        foreach(SpriteRenderer m in arrowRenderers)
        {
            m.enabled = show;
        }
        
    }
    public void SetArrows(bool right, bool left, bool up, bool down)
    {
        R = right;
        L = left;
        U = up;
        D = down;

        
        arrowRenderers[0].gameObject.SetActive(right);
        arrowRenderers[1].gameObject.SetActive(left);
        arrowRenderers[2].gameObject.SetActive(up);
        arrowRenderers[3].gameObject.SetActive(down);
        
    }
}
