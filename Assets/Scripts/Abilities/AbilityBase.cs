using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    // class that every Ability is based on

    public int baseCD;
    public int charges;

    private float nextCast;
    private int remainingCD;

    public abstract void SetUpAbility();
    public abstract void TriggerAbility();
}
