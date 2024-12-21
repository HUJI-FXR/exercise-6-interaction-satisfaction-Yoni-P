using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportAbilityActivation : MonoBehaviour
{
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private GameObject[] objectsForActivation;

    private HashSet<TeleportAbillityActivator> activators = new HashSet<TeleportAbillityActivator>();

    private void Start()
    {
        foreach (var obj in objectsForActivation)
        {
            if (!obj.TryGetComponent(out TeleportAbillityActivator teleportAbillityActivator))
            {
                obj.AddComponent<TeleportAbillityActivator>().teleportAbilityActivation = this;
            }
        }
    }

    public void AddActivator(TeleportAbillityActivator teleportAbillityActivator)
    {
        activators.Add(teleportAbillityActivator);
        teleportationProvider.enabled = false;
    }

    public void Activate(TeleportAbillityActivator teleportAbillityActivator)
    {
        activators.Remove(teleportAbillityActivator);
        if (activators.Count == 0)
        {
            if (teleportationProvider != null)
                teleportationProvider.enabled = true;
        }
    }
}
