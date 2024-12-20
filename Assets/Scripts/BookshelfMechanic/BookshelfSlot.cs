using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class BookshelfSlot : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    
    private GameObject _currentObject;
    public Action<GameObject> OnObjectPlaced;
    public GameObject currentObject { get => socketInteractor.GetOldestInteractableSelected()?.transform.gameObject;}
    
    private bool intialized = false;

    private void Start()
    {
        intialized = true;
    }

    private void OnSelectEntered(SelectEnterEventArgs enterEventArgs)
    {
        if (!intialized) return;
        
        OnObjectPlaced?.Invoke(enterEventArgs.interactable.gameObject);
    }
    
    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
    }
    
    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
    }
}
