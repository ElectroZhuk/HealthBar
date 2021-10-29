using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    public UnityAction OnHealthChanged;
    public UnityAction OnHealed;
    public UnityAction OnHitted;

    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            if (value < 0)
                _health = 0;
            else if (value > _maxHealth)
                _health = _maxHealth;
            else
                _health = value;
        }
    }
    public float MaxHealth => _maxHealth;

    private float _health;

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
            OnHealthChanged.Invoke();
            OnHitted.Invoke();
        }
    }

    public void TakeHeal(float heal)
    {
        float tempHealth = Health;
        Health += heal;

        if (tempHealth != Health)
        {
            OnHealthChanged.Invoke();
            OnHealed.Invoke();
        }
    }
}
