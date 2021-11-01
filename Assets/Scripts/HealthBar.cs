using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _percentForSecond;
    [SerializeField] private Text _currentHealthText;
    [SerializeField] private Text _maxHealthText;

    private float _target;
    private Slider _bar;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
        _bar.value = _player.Health / _player.MaxHealth;
        _target = _bar.value;
        _maxHealthText.text = _player.MaxHealth.ToString();
        _currentHealthText.text = _player.Health.ToString();
    }

    private void OnEnable()
    {
        _player.HealthChanged += ChangeSliderValue;
        _player.HealthChanged += ChangeTextValue;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ChangeSliderValue;
        _player.HealthChanged -= ChangeTextValue;
    }

    private void ChangeTextValue()
    {
        _currentHealthText.text = _player.Health.ToString();
    }

    private void ChangeSliderValue()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _target = _player.Health / _player.MaxHealth;
        _currentCoroutine = StartCoroutine(ChangeSmooth());
    }

    private IEnumerator ChangeSmooth()
    {
        while (_bar.value != _target)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _target, (_percentForSecond / 100) * Time.deltaTime);

            yield return null;
        }
    }
}
