using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private IControllable controllable;
    private List<IControllable> separateParts = new List<IControllable>();
    private int partIndex = 0;

    public bool MovementEnabled { get; set; } = true;

    [SerializeField] private SoulController soulController = null;


    [SerializeField] private MainPart mainPart = null;


    void Awake()
    {
        controllable = mainPart;
        separateParts.Add(mainPart);
    }

    void Update()
    {
        // switch controllable
        if (Input.GetKeyDown(KeyCode.Tab) && separateParts.Count > 1)
        {
            controllable.Stop();

            partIndex = (partIndex + 1) % separateParts.Count;
            IControllable nextPart = separateParts[partIndex];
            SwitchControllable(nextPart);
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            List<Attachable> partsInRange = mainPart.FindNearDetachedParts();
            if (controllable.Equals(mainPart))
            {     
                if (partsInRange.Count > 0)
                {
                    foreach (Attachable part in partsInRange)
                    {
                        part.Stop();
                        mainPart.AttachPart(part);
                        RemoveSeparatePart(part);
                    }
                    SwitchControllable(mainPart);
                }
                else
                {
                    Attachable detachedPart = mainPart.GetNextPart();
                    if (detachedPart != null)
                    {
                        mainPart.DetachPart(detachedPart);
                        AddSeparatePart(detachedPart);
                    }
                }
            }
            // controlling something else
            else
            {
                Attachable attachable = controllable.GetTransform.GetComponent<Attachable>();
                if (attachable && partsInRange.Contains(attachable))
                {
                    attachable.Stop();
                    mainPart.AttachPart(attachable);
                    RemoveSeparatePart(attachable);
                    SwitchControllable(mainPart);
                }
            }                 
        }
        if (MovementEnabled)
            controllable.Move();
    }

    private void SwitchControllable(IControllable c)
    {
        MovementEnabled = false;
        soulController.SwitchTarget(c.GetTransform);
        controllable = c;
    }

    public void AddSeparatePart(IControllable part)
    {
        separateParts.Add(part);
    }

    public void RemoveSeparatePart(IControllable part)
    {
        separateParts.Remove(part);
    }
}
