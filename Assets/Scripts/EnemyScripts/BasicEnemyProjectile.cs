﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyProjectile : MonoBehaviour
{
    public float speed;
    public Vector2 targetPos;
    Vector2 direction;
    Vector2 currentPos;
    public float damage;

    float screenBottom;

    void Start()
    {

        //Camera MainCam = Camera.main;
        //screenBounds = MainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCam.transform.position.z));

        screenBottom = -GameManager.GetScreenBounds.y;


    }

    void Update()
    {

        currentPos = gameObject.transform.position;
        direction = targetPos - currentPos;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (currentPos.y <= screenBottom)
        {
            gameObject.SetActive(false);

        }
        //float dis = Vector3.Distance(currentPos, targetPos);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>())
        {
            return; // when release the EMP just ignore the bullets
        }

        if (collision.CompareTag("Bullet")) // When bullet comming from Turret, just set false the enemy bullet
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.GetComponentInParent<TrainHealth>())
        {
            collision.gameObject.GetComponentInParent<TrainHealth>().TakeDamage(damage);
            //Debug.Log("basic doing damage to train");
            gameObject.SetActive(false);
        }

        if (collision.gameObject.GetComponent<TurretHealth>())
        {
            collision.gameObject.GetComponent<TurretHealth>().TakeDamage(damage);
            Debug.Log("basic doing damage to Turret");
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("basic doing damage to Player");
            gameObject.SetActive(false);
        }

        var shieldGen = collision.GetComponent<ShieldTurret>(); // when bullet hit the ShieldGenerator GameObject
        if (shieldGen)
        {
            shieldGen.IDamageble.TakeDamage(damage);
            gameObject.SetActive(false);
        }

        var empGen = collision.GetComponent<EMPTurret>(); // when bullet hit the EMPGenerator GameObject
        if (empGen)
        {
            empGen.IDamageable.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        //  if (!collision.gameObject.GetComponent<TurretHealth>() || !collision.gameObject.GetComponentInParent<TrainHealth>())
        //  {
        //      collision.isTrigger = false;
        //  }
        // else 
        // {
        //     //  Debug.Log("BasicEnemy Projectile set to false");
        //     //  gameObject.SetActive(false);
        // }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("BasicEnemy Projectile set to false");
            gameObject.SetActive(false);
        }
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        //    Debug.Log("basic doing damage to Player");
        //    gameObject.SetActive(false);
        //}

    }


}
