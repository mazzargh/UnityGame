using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Attachable : MonoBehaviour, IControllable
{
    public bool Attached { get; private set; }
    PlayerController playerController;
    [SerializeField] private float runSpeed = 5f;

    public Transform GetTransform => transform;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }


    public void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime, playerController.YSpeed, 0);
        playerController.Move(dir);
    }

    public void Stop()
    {
        playerController.Stop();
    }

    public void Detach()
    {
        transform.SetParent(null);
        Attached = false;
        playerController.EnableGravity();
    }

    public void Attach(MainPart main, Vector3 pos)
    {
        playerController.DisableGravity();
        transform.SetParent(main.transform);
        transform.localPosition = pos;
        Attached = true;
    }


}