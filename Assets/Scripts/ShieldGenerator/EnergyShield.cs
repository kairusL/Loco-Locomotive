﻿using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
