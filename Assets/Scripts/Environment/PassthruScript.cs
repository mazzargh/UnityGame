using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthruScript : MonoBehaviour
{

	public Collider collider1;
	public Collider playerCollider;


	private void OnTriggerEnter(Collider other) {
		//Debug.Log("enter");
		if (other.gameObject.tag == "Player") {
			Physics.IgnoreCollision(collider1, other, true);
		}
	}

	private void OnTriggerExit(Collider other) {
		//Debug.Log("Exit");
		if (other.gameObject.tag == "Player") {
			Physics.IgnoreCollision(collider1, other, false);
		}
	}
}
