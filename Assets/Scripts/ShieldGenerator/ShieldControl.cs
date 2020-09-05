﻿using System;
using UnityEngine;

public class ShieldControl : MonoBehaviour
{
    public event Action<bool> OnControllShield;

    private InputReciever _inputReciever;

    public SpriteRenderer SpriteShieldController { get; private set; }

    private void Awake()
    {
        SpriteShieldController = GetComponent<SpriteRenderer>();

        if (!TryGetComponent(out _inputReciever))
        {
            Debug.LogWarning($"[ShiledGenerator] -- Failed to get the component: {_inputReciever.name}");
        }
    }

    private void Update()
    {
        OnControllShield?.Invoke(_inputReciever.GetSecondaryInput());
    }

}