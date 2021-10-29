using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _player.OnHitted += EnableHitTrigger;
        _player.OnHealed += EnableHealTrigger;
        ChangeHealthValue();
        _player.OnHealthChanged += ChangeHealthValue;
    }
    private void OnDisable()
    {
        _player.OnHitted -= EnableHitTrigger;
        _player.OnHealed -= EnableHealTrigger;
        _player.OnHealthChanged -= ChangeHealthValue;
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
