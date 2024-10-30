using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100f;
    
    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public float returnHealth()
    {
        return health;
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
    }
    public void setHealth(float healthMax)
    {
        health = healthMax;
    }
}
