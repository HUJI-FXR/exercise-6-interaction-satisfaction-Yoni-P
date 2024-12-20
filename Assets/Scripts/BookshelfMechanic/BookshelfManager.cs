using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookshelfManager : MonoBehaviour
{
    [SerializeField] private BookshelfSlot[] bookshelfSlots;
    [SerializeField] private float ejectVelocity = 5f;

    private void OnEnable()
    {
        SubdribeToSlotEvents();
    }

    private void SubdribeToSlotEvents()
    {
        foreach (var slot in bookshelfSlots)
        {
            slot.OnObjectPlaced += OnObjectPlaced;
        }
    }

    private void OnObjectPlaced(GameObject placedObject)
    {
        Debug.Log("Object placed");
        var currentObjects = new List<GameObject>();
        foreach (var bookshelfSlot in bookshelfSlots)
        {
            if (bookshelfSlot.currentObject != null && bookshelfSlot.currentObject != placedObject)
            {
                currentObjects.Add(bookshelfSlot.currentObject);
            }
        }
        
        var randObject = currentObjects[UnityEngine.Random.Range(0, currentObjects.Count)];
        
        Debug.Log("Object chosen to eject: " + randObject.name);
        
        var randRigidbody = randObject.GetComponent<Rigidbody>();
        
        var player = GameObject.FindGameObjectWithTag("Player");
        
        var directionModifier = Mathf.Sign(Vector3.Dot(player.transform.position - randObject.transform.position,
            randObject.transform.up));

        // randRigidbody.velocity = transform.right * ejectVelocity * directionModifier;
        StartCoroutine(EjectObject(randObject, randObject.transform.up * directionModifier));
    }
    
    private IEnumerator EjectObject(GameObject objectToEject, Vector3 ejectDirection)
    {
        var rigidbody = objectToEject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = ejectDirection * ejectVelocity;
        Debug.DrawRay(objectToEject.transform.position, ejectDirection * ejectVelocity, Color.blue, 5f);
        objectToEject.GetComponent<XRGrabInteractable>().enabled = false;
        yield return new WaitForSeconds(1f);
        objectToEject.GetComponent<XRGrabInteractable>().enabled = true;
    }

    private void OnDisable()
    {
        UnsubscribeFromSlotEvents();
    }

    private void UnsubscribeFromSlotEvents()
    {
        foreach (var slot in bookshelfSlots)
        {
            slot.OnObjectPlaced -= OnObjectPlaced;
        }
    }
}
