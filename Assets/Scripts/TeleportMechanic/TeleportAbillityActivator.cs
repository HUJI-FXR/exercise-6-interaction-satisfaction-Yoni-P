using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbillityActivator : MonoBehaviour
{
    public TeleportAbilityActivation teleportAbilityActivation;

    private void OnEnable()
    {
        if (teleportAbilityActivation == null)
        {
            teleportAbilityActivation = FindObjectOfType<TeleportAbilityActivation>();
        }
        teleportAbilityActivation.AddActivator(this);
    }

    private void OnDisable()
    {
        teleportAbilityActivation.Activate(this);
    }
}
