using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Hitted += EnableHitTrigger;
        _player.Healed += EnableHealTrigger;
        ChangeHealthValue();
        _player.HealthChanged += ChangeHealthValue;
    }

    private void OnDisable()
    {
        _player.Hitted -= EnableHitTrigger;
        _player.Healed -= EnableHealTrigger;
        _player.HealthChanged -= ChangeHealthValue;
    }

    private void ChangeHealthValue()
    {
        _animator.SetFloat(PlayerAnimatorController.Params.Health, _player.Health);

        if (_player.Health <= 0 && _animator.GetBool(PlayerAnimatorController.Params.IsDead) == false)
        {
            _animator.SetBool(PlayerAnimatorController.Params.IsDead, true);
            _animator.Play(PlayerAnimatorController.States.Dead);
        }
        else if (_player.Health > 0 && _animator.GetBool(PlayerAnimatorController.Params.IsDead) == true)
        {
            _animator.Play(PlayerAnimatorController.States.Recovered);
            StartCoroutine(WaitUntillRecoverStop());
        }
    }

    private IEnumerator WaitUntillRecoverStop()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _animator.SetBool(PlayerAnimatorController.Params.IsDead, false);
    }

    private void EnableHitTrigger()
    {
        if (_animator.GetBool(PlayerAnimatorController.Params.IsDead) == false)
            _animator.SetTrigger(PlayerAnimatorController.Params.Hit);
    }

    private void EnableHealTrigger()
    {
        if (_animator.GetBool(PlayerAnimatorController.Params.IsDead) == false)
            _animator.SetTrigger(PlayerAnimatorController.Params.Heal);
    }
}
