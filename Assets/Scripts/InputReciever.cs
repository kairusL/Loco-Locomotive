﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    private PlayerInput mPlayerInput;

    private bool mInUse = false;
    private bool mIsPlayer = false;
    private bool mPrimaryHeld = false;
    private bool mSecondaryHeld = false;

    //Public functions
    public bool SetPlayerInput(ref PlayerInput playerInput)
    {
        if (!mInUse)
        {
            mPlayerInput = playerInput;
            mInUse = true;
            mPlayerInput.actions["PrimaryHold"].started += StartPrimaryHold;
            mPlayerInput.actions["PrimaryHold"].canceled += EndPrimaryHold;
            mPlayerInput.actions["SecondaryHold"].started += StartSecondaryHold;
            mPlayerInput.actions["SecondaryHold"].canceled += EndSecondaryHold;
            return true;
        }
        return false;
    }

    public void DisablePlayerInput()
    {
        if (mInUse)
        {
            mInUse = false;
            mPrimaryHeld = false;
            mSecondaryHeld = false;
            mPlayerInput.actions["PrimaryHold"].started -= StartPrimaryHold;
            mPlayerInput.actions["PrimaryHold"].canceled -= EndPrimaryHold;
            mPlayerInput.actions["SecondaryHold"].started -= StartSecondaryHold;
            mPlayerInput.actions["SecondaryHold"].canceled -= EndSecondaryHold;
            mPlayerInput = null;
        }
    }

    public Vector2 GetDirectionalInput()
    {
        if (mPlayerInput)
        {
            return mPlayerInput.actions["Directional"].ReadValue<Vector2>();
        }
        else
        {
            return Vector2.zero;
        }
    }

    public float GetHorizontalInput()
    {
        if (mPlayerInput)
        {
            return mPlayerInput.actions["Directional"].ReadValue<Vector2>().x;
        }
        else
        {
            return 0.0f;
        }
    }

    public float GetVerticalInput()
    {
        if (mPlayerInput)
        {
            return mPlayerInput.actions["Directional"].ReadValue<Vector2>().y;
        }
        else
        {
            return 0.0f;
        }
    }

    public bool GetPrimaryInput()
    {
        if (mPlayerInput)
        {
            return mPlayerInput.actions["Primary"].triggered;
        }
        else
        {
            return false;
        }
    }

    public bool GetPrimaryHoldInput()
    {
        return mPrimaryHeld;
    }

    public bool GetSecondaryInput()
    {
        if (mPlayerInput)
        {
            return mPlayerInput.actions["Secondary"].triggered;
        }
        else
        {
            return false;
        }
    }

    public bool GetSecondaryHoldInput()
    {
        return mSecondaryHeld;
    }

    public bool IsUsingGamepad()
    {
        return mPlayerInput.defaultControlScheme.Equals("Gamepad");
    }

    //Private functions
    private void Start()
    {
        if (GetComponent<Player>())
        {
            mIsPlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!mIsPlayer && collision.CompareTag("Player"))
        {
            InputReciever inputReciever = this;
            collision.GetComponent<Player>().PlayerController.SetControllable(ref inputReciever);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!mIsPlayer && collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().PlayerController.SetNoControllable();
        }
    }

    private void StartPrimaryHold(InputAction.CallbackContext ctx)
    {
        mPrimaryHeld = true;
    }

    private void EndPrimaryHold(InputAction.CallbackContext ctx)
    {
        mPrimaryHeld = false;
    }

    private void StartSecondaryHold(InputAction.CallbackContext ctx)
    {
        mSecondaryHeld = true;
    }

    private void EndSecondaryHold(InputAction.CallbackContext ctx)
    {
        mSecondaryHeld = false;
    }

    public bool GetInUse()
    {
        return mInUse;
    }
}
