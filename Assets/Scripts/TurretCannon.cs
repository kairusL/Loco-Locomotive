﻿using UnityEngine;
using UnityEngine.UI;

public class TurretCannon : MonoBehaviour
{
    [Header("Properties")]
    public GameObject Bullets;
    public Transform CannonHandler;
    public Transform CannonFirePoint;
    public Text AmmoCountText;
    public Text TurretText;
    public Text TurretRepairText;
    public float FireRate = 100f;
    public float CannonHandlerSpeed = 10.0f;
    public int AmmoMax = 10;

    private int mCurrentAmmo;
    public float turretMaxHealth;
    private float mTurretHealth;
    private bool mRunOutHealth;

    private InputReciever mInputReciever;
    private float mTimeToFire = 0f;

    void Start()
    {
        mInputReciever = GetComponent<InputReciever>();
        mCurrentAmmo = AmmoMax;
        AmmoCountText.text = $"Ammo: {mCurrentAmmo}";
        mRunOutHealth = false;
        mTurretHealth = turretMaxHealth;
    }

    private void Update()
    {
        CannonHandler.transform.Rotate(0.0f, 0.0f, mInputReciever.GetDirectionalInput().x * CannonHandlerSpeed);
        Fire(mInputReciever.GetSecondaryHoldInput());
    }

    public void Fire(bool setFire)
    {
        if (mRunOutHealth == false && setFire && (mCurrentAmmo > 0) && (Time.time >= mTimeToFire))
        {
            mTimeToFire = Time.time + (1f / FireRate);
            var x = Instantiate(Bullets, CannonFirePoint.transform.position, Quaternion.identity);
            x.transform.rotation = CannonFirePoint.rotation;
            AmmoCountText.text = $"Ammo: {--mCurrentAmmo}";
        }
        if (mCurrentAmmo == 0)
        {
            AmmoCountText.text = $"Ammo ...... Run out ammo........!!";
        }
    }

    public void TakeDamage(float takingDamage)
    {
        mTurretHealth -= takingDamage;
        TurretText.text = $"! Warning !   Turret taking {takingDamage} damage ! Turret Health: {mTurretHealth}";
        if (mTurretHealth <= 0.0f)
        {
            TurretText.text = $"! Turret is Out Of Order ! ";
        }

    }

    public void Repair(float repairHealth)
    {
        mTurretHealth += repairHealth;
        TurretRepairText.text = $"Turret repairing: {repairHealth} , Turret Health: {mTurretHealth}";
        if (mTurretHealth >= turretMaxHealth)
        {
            mTurretHealth = turretMaxHealth;
            TurretRepairText.text = $"Turret health reached Maximum ";
        }
    }

    public void Reload(int ammo)
    {
        mCurrentAmmo += ammo;
        AmmoCountText.text = $"Ammo reloading: ... Current Ammo: {mCurrentAmmo}";
        if (mCurrentAmmo >= AmmoMax)
        {
            mCurrentAmmo = AmmoMax;
            AmmoCountText.text = $"Can not Reload Anymore ... Current Ammo: {mCurrentAmmo}";
        }
    }

    public void IsAlive()
    {
        if (mTurretHealth <= 0.0f)
        {
            mRunOutHealth = true;
        }
        else
        {
            mRunOutHealth = false;
        }
    }

    public void ReloadCannon(int amountAmmo)
    {
        mCurrentAmmo = amountAmmo;
    }

}