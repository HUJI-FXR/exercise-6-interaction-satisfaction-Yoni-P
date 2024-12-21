using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Throw : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor interactor;
    [SerializeField] private ActionBasedController controller;

    [SerializeField] private Transform playerHead;

    private GameObject objectToThrow;
    
    private void OnEnable()
    {
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);

        controller.activateAction.action.performed += ThrowObject;
    }

    private void ThrowObject(InputAction.CallbackContext obj)
    {
        Debug.Log("Throw");
        if (objectToThrow != null)
        {
            if (!objectToThrow.TryGetComponent(out Rigidbody rb))
            {
                return;
            }
            
            StartCoroutine(DisableAndEnableInteractable(1, objectToThrow.GetComponent<XRBaseInteractable>()));
            
            rb.transform.SetParent(null);
            
            rb.isKinematic = false;
            
            var direction = rb.transform.position - playerHead.position;
            rb.AddForce(direction * 10, ForceMode.Impulse);
            
            objectToThrow = null;
        }
    }

    private IEnumerator DisableAndEnableInteractable(float time, XRBaseInteractable interactable)
    {
        interactable.enabled = false;
        yield return new WaitForSeconds(time);
        interactable.enabled = true;
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        objectToThrow = arg0.interactableObject.transform.gameObject;
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(OnSelectEntered);
        interactor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        objectToThrow = null;
    }
}
