﻿using UnityEngine;

public class ShieldTurret : MonoBehaviour
{
    public IShieldGenerator IShieldGenerator { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            player.shieldGenerator = IShieldGenerator;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            player.shieldGenerator = null;
        }
    }
}