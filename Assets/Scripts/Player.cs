﻿using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public AmmoCreat ammoCreat;
    public Repairkitcrate repairkitcrate;
    public TurretLoader turretLoader;
    public bool isHoldingAmmo;
    public bool isHoldingRepairKit;
    public GameObject ammoSprite;
    public GameObject repairKitSprite;

    public LadderController LadderController { get; set; }
    public PlayerController PlayerController { get; set; }

    [Header("Properties")]
    [SerializeField]
    private float movementSpeed = 3.0f;
    [SerializeField]
    private float gravity = 5.0f;

    private Rigidbody2D mRigidBody;
    private InputReciever mInputReceiver;
    private float mPlayerHeight;
    
    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mInputReceiver = GetComponent<InputReciever>();
        mPlayerHeight = GetComponent<CapsuleCollider2D>().size.y;
        ammoSprite.SetActive(false);
        repairKitSprite.SetActive(false);
    }
    private void Update()
    {
        if(ammoCreat !=null&&mInputReceiver.GetSecondaryInput())
        {
            isHoldingAmmo = true;
            ammoSprite.SetActive(true);
        }
        else if (isHoldingAmmo = true && mInputReceiver.GetSecondaryInput())
        {
            ammoSprite.SetActive(false);
            isHoldingAmmo = false;
        }
        else if(turretLoader!=null&& mInputReceiver.GetSecondaryInput())
        {
            
            isHoldingAmmo = false;
            ammoSprite.SetActive(false);
        }
        if (repairkitcrate != null && mInputReceiver.GetSecondaryInput())
        {
            isHoldingRepairKit = true;
            repairKitSprite.SetActive(true);
        }
        else if (isHoldingRepairKit = true && mInputReceiver.GetSecondaryInput())
        {
            repairKitSprite.SetActive(false);
            isHoldingRepairKit = false;
        }
        else if (turretLoader != null && mInputReceiver.GetSecondaryInput())
        {

            isHoldingRepairKit = false;
            repairKitSprite.SetActive(false);
        }

    }
    private void FixedUpdate()
    {
        float horizontal = Mathf.Round(mInputReceiver.GetHorizontalInput() + 0.2f);
        float vertical = Mathf.Round(mInputReceiver.GetVerticalInput() + 0.2f);

        mRigidBody.velocity = new Vector2(horizontal * movementSpeed, mRigidBody.velocity.y);

        if (LadderController)
        {
            mRigidBody.gravityScale = 0.0f;

            if (vertical != 0.0f)
            {
                mRigidBody.velocity = new Vector2(0.0f, vertical * movementSpeed);
                transform.position = new Vector2(LadderController.transform.position.x, Mathf.Min(transform.position.y, LadderController.GetLadderTopPosition().y + mPlayerHeight * 0.5f));
            }
            else
            {
                mRigidBody.velocity = new Vector2(mRigidBody.velocity.x, 0.0f);
            }
        }
        else
        {
            mRigidBody.gravityScale = gravity;
        }
    }
}