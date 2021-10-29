using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _percentForSecond;
    [SerializeField] private Text _currentHealth;
    [SerializeField] private Text _maxHealth;

    private float _target;
    private Slider _bar;

    private void Start()
    {
        _player.OnHealthChanged += ChangeSliderValue;
        _player.OnHealthChanged += ChangeTextValue;
        _bar = GetComponent<Slider>();
        _bar.value = _player.Health / _player.MaxHealth;
        _target = _bar.value;
        _maxHealth.text = _player.MaxHealth.ToString();
        _currentHealth.text = _player.Health.ToString();
        StartCoroutine(ChangeSmooth());
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= ChangeSliderValue;
        _player.OnHealthChanged -= ChangeTextValue;
    }

    private void ChangeTextValue()
    {
        _currentHealth.text = _player.Health.ToString();
    }

    private void ChangeSliderValue()
    {
        _target = _player.Health / _player.MaxHealth;
    }

    private IEnumerator ChangeSmooth()
    {
        while (true)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _target, (_percentForSecond / 100) * Time.deltaTime);

            yield return null;
        }
    }
}
