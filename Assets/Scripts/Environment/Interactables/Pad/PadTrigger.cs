using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTrigger : MonoBehaviour
{

	public Animator padAnim;
	public Animator doorAnim;
    private int numObjects = 0;

    void OnTriggerEnter(Collider other) {
    	//if (other.gameObject.tag == "Player") {
        if (numObjects == 0)
        {
            Debug.Log(numObjects);
            padAnim.SetTrigger("down");
            doorAnim.SetTrigger("open");
        }
        numObjects++;	
		//}
    }

    void OnTriggerExit(Collider other) {
        //if (other.gameObject.tag == "Player") {
        numObjects--;
        if (numObjects == 0)
        {
            Debug.Log(numObjects);
            padAnim.SetTrigger("up");
            doorAnim.SetTrigger("close");
        }
            
		//}
    }
}
