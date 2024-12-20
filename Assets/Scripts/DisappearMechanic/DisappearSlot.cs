using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearSlot : MonoBehaviour
{
    [SerializeField] private GameObject expectedObject;
    
    public Action CorrectObjectPlaced;
    public Action CorrectObjectRemoved;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == expectedObject)
        {
            CorrectObjectPlaced?.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == expectedObject)
        {
            CorrectObjectRemoved?.Invoke();
        }
    }
}
