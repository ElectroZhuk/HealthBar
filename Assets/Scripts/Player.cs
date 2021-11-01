using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _health;

    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
        }
    }
    public float MaxHealth => _maxHealth;
    public UnityAction HealthChanged;
    public UnityAction Healed;
    public UnityAction Hitted;



    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float tempHealth = Health;
        Health -= damage;

        if (tempHealth != Health)
        {
            HealthChanged?.Invoke();
            Hitted?.Invoke();
        }
    }

    public void TakeHeal(float heal)
    {
        float tempHealth = Health;
        Health += heal;

        if (tempHealth != Health)
        {
            HealthChanged?.Invoke();
            Healed?.Invoke();
        }
    }
}
