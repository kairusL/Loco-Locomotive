﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Basic Enemy Stuffs....
    public float fallSpeed = 1.0f;
    public float health;
    public float damage;
    public float gravity = 1.0f;
    public GameObject target; 
    public GameObject topWagonCollider;

    private Vector3 targetPos;
    private Vector3 currentPos;
    private Vector3 direction;

    private TrainHealth trainHealth;
    private Vector2 mColliderSize;
    public GameObject groundArea;


    private float mTakeDamageDelay=1.5f;

    // Change Enemy State .
    private State mCurrentState = State.InSky;
    enum State
    {
        InSky,
        OnTrain,
        OnGround,
        OnAttack,
        Nothing
    }
    private void Start()
    {
        trainHealth = GetComponent<TrainHealth>();
        mColliderSize = GetComponentInChildren<BoxCollider2D>().size;
    }
    // Update is called once per frame
    void Update()
    {
       targetPos =(target.transform.localPosition);
       currentPos =transform.position;
       direction = targetPos - currentPos;
       direction.Normalize();
       float dis = Vector3.Distance(currentPos, targetPos);

        if (mCurrentState == State.InSky)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            transform.position = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
        }
        else if (mCurrentState == State.OnTrain)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            transform.Translate(direction * gravity * Time.deltaTime,Space.World);

            if (dis<= mColliderSize.x)
            {
                transform.position = currentPos;
                mCurrentState = State.OnAttack;
            }

        }
        else if(mCurrentState == State.OnAttack)
        {
            if (mTakeDamageDelay < Time.time)
            {
                mTakeDamageDelay = Time.time + 0.5f;
                target.GetComponent<TrainHealth>().TakeDamage(10.0f);
            }

        }
        else if (mCurrentState == State.OnGround)
        {
            Destroy(gameObject);
        }
    }

    public float GetDamage() { return damage; }
    public void TakeDamage(float takingDamage)
    {
        health -= takingDamage;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
            Debug.Log("See you next time!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == topWagonCollider)
        {
            transform.parent = topWagonCollider.gameObject.transform;
            mCurrentState = State.OnTrain;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == groundArea)
        {
            Destroy(gameObject);
            mCurrentState = State.OnGround;
        }
    }
}
