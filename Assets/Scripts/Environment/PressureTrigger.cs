using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		//Debug.Log("enter");
		if (other.gameObject.tag == "Player") {
			
		}
	}

	private void OnTriggerExit(Collider other) {
		//Debug.Log("Exit");
		if (other.gameObject.tag == "Player") {
			
		}
	}
}
