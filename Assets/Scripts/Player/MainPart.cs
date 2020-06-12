using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MainPart : MonoBehaviour, IControllable
{
    [SerializeField] private List<Attachable> validParts = new List<Attachable>();
    [SerializeField] private Attachable leftLeg = null;
    [SerializeField] private Attachable rightLeg = null;
    [SerializeField] private float runSpeed = 1.5f;
    [SerializeField] private float hopSpeed = 1f;
    [SerializeField] private float partDetectionRadius = 5f;
    [SerializeField] private LayerMask attachableLayerMask = 0;

    private Dictionary<Attachable, Vector3> validPartPositions = new Dictionary<Attachable, Vector3>();
    private List<Attachable> attachedParts = new List<Attachable>();



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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, partDetectionRadius, attachableLayerMask);
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
            if (part.partType == BodyPart.LEFTLEG || part.partType == BodyPart.RIGHTLEG)
            {
                AttachLeg(part);
            }
        }
    }

    private void AttachLeg(Attachable part)
    {
        if (part.partType == BodyPart.LEFTLEG)
        {
            if (!rightLeg)
            {
                // attaching one leg so increase size of player controller
                playerController.ChangeSize(2.5f);
            }
            leftLeg = part;
        }
        else if (part.partType == BodyPart.RIGHTLEG)
        {
            if (!leftLeg)
            {
                // attaching one leg so increase size of player controller
                playerController.ChangeSize(2.5f);
            }
            rightLeg = part;
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
            if (part.partType == BodyPart.LEFTLEG || part.partType == BodyPart.RIGHTLEG)
            {
                DetachLeg(part);
            }
        }
    }

    private void DetachLeg(Attachable part)
    {
        if (part.partType == BodyPart.LEFTLEG)
        {
            if (!rightLeg)
            {
                // detaching one leg so increase size of player controller
                playerController.ChangeSize(2f);
            }
            leftLeg = null;
        }
        else if (part.partType == BodyPart.RIGHTLEG)
        {
            if (!leftLeg)
            {
                // attaching one leg so increase size of player controller
                playerController.ChangeSize(2f);
            }
            rightLeg = null;
        }
    }

    public void Move()
    {
        float speed = 0;
        if (leftLeg && rightLeg)
        {
            // run
            speed = runSpeed;
        }
        else if (leftLeg || rightLeg)
        {
            // hop
            speed = hopSpeed;
        }
        else
        {
            // only rotate direction
        }

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*speed, playerController.YSpeed, 0);
        playerController.Move(dir);
    }

    public void Stop()
    {
        playerController.Stop();
    }
}
