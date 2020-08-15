﻿using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private AmmoData _ammoData = default;
    [SerializeField] private float _areaOfEffect = 50f; // preciso implementar esse efeito de dano em area

    private Vector3 _screenBounds;

    ObjectPoolManager _objectPoolManager = null;

    private void Start()
    {
        _screenBounds = GameManager.GetScreenBounds;
    }

    private void Update()
    {
        if (_ammoData.MoveSpeed != 0f)
        {
            transform.position += transform.up * (_ammoData.MoveSpeed * Time.deltaTime);

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
        IDamageable<float> damageable = collision.GetComponentInParent<EnemyHealth>();
        if (damageable == null) return;
        damageable.TakeDamage(_ammoData.Damage, _ammoData.Type);
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
