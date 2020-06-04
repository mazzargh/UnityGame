using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float speed = 1f;

    [SerializeField] private InputController inputController = null;

    private bool switchingTarget;

    // Update is called once per frame
    private void Update()
    {
        if (switchingTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                EnterTarget();
            }
        }
    }

    private void EnterTarget()
    {
        switchingTarget = false;
        transform.position = target.transform.position;
        transform.SetParent(target.transform);
        inputController.MovementEnabled = true;
    }

    public void SwitchTarget(Transform t)
    {
        Debug.Log("switching");
        transform.SetParent(null);
        target = t;
        switchingTarget = true;
    }
}
