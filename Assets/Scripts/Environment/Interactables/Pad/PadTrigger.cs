using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTrigger : MonoBehaviour
{

	public Animator padAnim;
	public Animator doorAnim;

    void OnTriggerEnter(Collider other) {
    	if (other.gameObject.tag == "Player") {
    		padAnim.SetTrigger("down");
    		doorAnim.SetTrigger("open");
		}
    }

    void OnTriggerExit(Collider other) {
    	if (other.gameObject.tag == "Player") {
    		padAnim.SetTrigger("up");
    		doorAnim.SetTrigger("close");
		}
    }
}
