using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MainPart : MonoBehaviour, IControllable
{
    [SerializeField] private List<Attachable> validParts = new List<Attachable>();
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float partDetectionRadius = 5f;
    [SerializeField] private LayerMask playerMask = 0;

    private Dictionary<Attachable, Vector3> validPartPositions = new Dictionary<Attachable, Vector3>();
    public List<Attachable> attachedParts = new List<Attachable>();

    private PlayerController playerController;

    public Transform GetTransform => transform;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.EnableGravity();
    }

    // temporary start method
    private void Start()
    {
        foreach (Attachable part in validParts)
        {
            validPartPositions.Add(part, part.transform.localPosition);
            attachedParts = validParts;
        }
    }

    public List<Attachable> FindNearDetachedParts()
    {
        List<Attachable> nearParts = new List<Attachable>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, partDetectionRadius, playerMask);
        foreach (Collider c in hitColliders)
        {
            Attachable part = c.GetComponent<Attachable>();
            if (part && !attachedParts.Contains(part))
            {
                nearParts.Add(part);
            }
        }
        return nearParts;
    }

    public void AttachPart(Attachable part)
    {
        if (validPartPositions.ContainsKey(part))
        {
            part.Attach(this, validPartPositions[part]);
            attachedParts.Add(part);
        }
    }

    public Attachable GetNextPart()
    {
        if (attachedParts.Count > 0)
        {
            return attachedParts[0];
        }
        return null;
    }

    public void DetachPart(Attachable part)
    {
        if (attachedParts.Contains(part))
        {
            part.Detach();
            attachedParts.Remove(part);
        }
    }

    public void Move()
    {
        // hop if one leg attached (with slower speed)
        // run if both attached
        // only rotate character if no legs attached

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*runSpeed, playerController.YSpeed, 0);
        playerController.Move(dir);
    }

    public void Stop()
    {
        playerController.Stop();
    }
}
