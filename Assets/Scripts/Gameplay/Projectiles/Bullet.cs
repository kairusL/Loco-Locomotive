﻿using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AmmoData _ammoData = default;

    private Vector3 _screenBounds;
    private ObjectPoolManager _objectPoolManager = null;

    void Start()
    {
        _screenBounds = GameManager.GetScreenBounds;
    }

    private void FixedUpdate()
    {
        if (_ammoData.MoveSpeed != 0f)
        {
            transform.position += transform.up * (_ammoData.MoveSpeed * Time.fixedDeltaTime);

            // set activated false prefabs when touch the camera bounds
            if ((transform.position.x >= _screenBounds.x) ||
                (transform.position.x <= -_screenBounds.x) ||
                (transform.position.y >= _screenBounds.y) ||
                (transform.position.y <= -_screenBounds.y))
            {
                RecycleBullet();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageableType<float> damageable = collision.GetComponent<EnemyHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(_ammoData.Damage, _ammoData.Type);
        }
        RecycleBullet();
    }

    private void RecycleBullet()
    {
        if (_objectPoolManager == null)
        {
            _objectPoolManager = ServiceLocator.Get<ObjectPoolManager>();
        }
        _objectPoolManager.RecycleObject(gameObject);
    }
}
