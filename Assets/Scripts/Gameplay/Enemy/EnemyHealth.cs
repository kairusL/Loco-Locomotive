using Interfaces;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageableType<float>
{
    // regular type 
    private SpriteRenderer _spriteDamageIndicator = null;
    private readonly WaitForSeconds _waitForSecondsDamage = new WaitForSeconds(0.05f);
    private Color _defaultColor;

    public SpriteRenderer SpriteColor { get { return _spriteDamageIndicator; } set { _spriteDamageIndicator=value; } }

    private bool reHealth=false;
    public bool ReSetHealth { get { return reHealth; } set {reHealth=value; } }
    private void Awake()
    {
        _spriteDamageIndicator = GetComponentInChildren<SpriteRenderer>();
        _defaultColor = _spriteDamageIndicator.color;
    }

    private float m_health = 0.0f;

    public float health
    {
        get { return m_health; }
        set { m_health = value; }
    }
    private void Update()
    {
        if (reHealth)
        {
            _spriteDamageIndicator.color = _defaultColor;
            reHealth = false;
        }
    }
    public void TakeDamage(float takingDamage, DispenserData.Type damageType)
    {
        if (CompareTag("ShieldEnemy"))
        {
            if (!GetComponentInChildren<EnemyShieldHealth>().ShieldIsActive)
            {
                HpLogic(takingDamage, damageType);
            }
            else
            {
                ShieldLogic(takingDamage, damageType);
            }
        }


        if (CompareTag("Enemy"))
        {
            HpLogic(takingDamage, damageType);
        }
    }

    public bool IsAlive()
    {
        return health > 0.0f;
    }

    void HpLogic(float takingDamage, DispenserData.Type damageType)
    {

        health -= takingDamage;
        Debug.Log(tag + "Lost " + takingDamage + "hp. Current health: " + health);
        StartCoroutine(DamageIndicator());
        //if (health <= 0.0f)
        //{
        //    Destroy(gameObject);
        //    Debug.Log("I will be back!");
        //}
    }

    void ShieldLogic(float takingDamage, DispenserData.Type damageType)
    {

        if (damageType != DispenserData.Type.LaserBeam)
        {
            GetComponentInChildren<EnemyShieldHealth>().TakeDamage(takingDamage);
        }

    }


    private IEnumerator DamageIndicator()
    {
        _spriteDamageIndicator.color = Color.red;
        yield return _waitForSecondsDamage;
        _spriteDamageIndicator.color = _defaultColor;
    }
}
