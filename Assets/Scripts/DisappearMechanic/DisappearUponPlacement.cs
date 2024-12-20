using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearUponPlacement : MonoBehaviour
{
    [SerializeField] private DisappearSlot[] slots;
    
    private int _correctObjectsPlaced;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void OnEnable()
    {
        foreach (var slot in slots)
        {
            slot.CorrectObjectPlaced += OnCorrectObjectPlaced;
            slot.CorrectObjectRemoved += OnCorrectObjectRemoved;
        }
    }

    private void OnCorrectObjectRemoved()
    {
        _correctObjectsPlaced--;
        if (_correctObjectsPlaced < slots.Length)
        {
            _meshRenderer.forceRenderingOff = false;
        }
    }

    private void OnCorrectObjectPlaced()
    {
        _correctObjectsPlaced++;
        if (_correctObjectsPlaced == slots.Length)
        {
            _meshRenderer.forceRenderingOff = true;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in slots)
        {
            slot.CorrectObjectPlaced -= OnCorrectObjectPlaced;
            slot.CorrectObjectRemoved -= OnCorrectObjectRemoved;
        }
    }
}
