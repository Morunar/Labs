﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBossController : EnemyShooterController
{
    [Header("Strike")]
    [SerializeField] private Transform _strikePoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _strikeRange;
    [SerializeField] private LayerMask _enemies;

    [Header("PowerStrike")]
    [SerializeField] private Collider2D _strikeCollider;
    [SerializeField] private int _powerStrikeDamage;
    [SerializeField] private float _powerStrikeSpeed;
    [SerializeField] private float _powerStrikeRange;

    [Header("Transition")]
    [SerializeField] private float _waitTime;

    private float _currentStrikeRange;
    private bool _fightStarted;
    private bool _inRage;

    private ServiceManager _serviceManager;

    private EnemyState _stateOnHold;
    private EnemyState[] _attackStates = { EnemyState.Strike, EnemyState.PowerStrike, EnemyState.Shoot };
    #region UnityMethods

    protected override void FixedUpdate()
    {
        if (_currentState == EnemyState.Death)
        {
            return;
            
        }
        if (IsGroundEnding())
        {
            Flip();
        }
        if (_currentState == EnemyState.Move)
        {
            Move();
        }

        if (_currentState == EnemyState.Move && _attacking)
        {
            TurnForPlayer();
            if (CanAttack())
            {
                ChangeState(_stateOnHold);
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_strikePoint.position , new Vector3(_strikeRange, _strikeRange, 0));
    }
    #endregion

    #region PublicMethods
    public override void TakeDamage(int damage, DamageType type = DamageType.Casual, Transform player = null)
    {
        if(_currentState==EnemyState.PowerStrike && type != DamageType.Projectile || _currentState == EnemyState.Hurt)
        {
            return;
        }
        base.TakeDamage(damage, type, player);

        if(_currentHP <= _maxHP / 2 && !_inRage)
        {
            _inRage = true;
            ChangeState(EnemyState.Hurt);
        }
    }

    #endregion

    #region PrivateMethods
    protected override void ChangeState(EnemyState state)
    {
        base.ChangeState(state);
        switch (_currentState)
        {
            case EnemyState.PowerStrike:
            case EnemyState.Strike:
                _attacking = true;
                _currentStrikeRange = state == EnemyState.Strike ? _strikeRange : _powerStrikeRange;
                _enemyRb.velocity = Vector2.zero;
                if (!CanAttack())
                {
                    _stateOnHold = state;
                    _enemyAnimator.SetBool(_currentState.ToString(), false);
                    ChangeState(EnemyState.Move);
                }
                break;
            case EnemyState.Hurt:
                _attacking = false;
                _enemyRb.velocity = Vector2.zero;
                StopAllCoroutines();
                break;
        }

    }

    protected override void DoStateAction()
    {
        base.DoStateAction();
        switch (_currentState)
        {
            case EnemyState.Strike:
                Strike();
                break;
            case EnemyState.PowerStrike:
                StrikedWithPower();
                break;
        }
    }

    protected override void EndState()
    {
        switch (_currentState)
        {
            case EnemyState.PowerStrike:
                EndPowerStrike();
                _attacking = false;
                _enemyAnimator.SetBool("PowerStrike", false);
                break;
            case EnemyState.Strike:
                _attacking = false;
                _enemyAnimator.SetBool("Strike", false);
                break;
            case EnemyState.Hurt:
                _enemyAnimator.SetBool("Rage", true);
                _fightStarted = false;
                break;

        }
        base.EndState();
        if(_currentState == EnemyState.Shoot || _currentState == EnemyState.PowerStrike || _currentState == EnemyState.Strike || _currentState == EnemyState.Hurt)
        {
            StartCoroutine(BeginNewCircle());
        }
    }

    protected override void ResetState()
    {
        base.ResetState();
        _enemyAnimator.SetBool(EnemyState.Hurt.ToString(), false);
        _enemyAnimator.SetBool(EnemyState.PowerStrike.ToString(), false);
        _enemyAnimator.SetBool(EnemyState.Strike.ToString(), false);
    }

    protected override void CheckPlayerInRange()
    {
        if (_player == null || _IsAngry)
        {
            return;
        }
        if (Vector2.Distance(transform.position, _player.transform.position) < _angerRange)
        {
            _IsAngry = true;
            if (!_fightStarted)
            {
                StopAllCoroutines();
                StartCoroutine(BeginNewCircle());

            }
        }
        else
        {
            _IsAngry = false;
        }
    }

    protected void ChooseNextAttackState()
    {
        int state = UnityEngine.Random.Range(0, _attackStates.Length);
        ChangeState(_attackStates[state]);
    }

    protected void Strike()
    {
        Collider2D player = Physics2D.OverlapBox(_strikePoint.position, new Vector2(_strikeRange, _strikeRange), 0, _enemies);
        if (player != null)
        {
            Player_Controller playerController = player.GetComponent<Player_Controller>();
            int damage = _inRage ? _damage * 2 : _damage;
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }
        }
    }
    protected void StrikedWithPower()
    {
        _strikeCollider.enabled = true;
        _enemyRb.velocity = transform.right * _powerStrikeSpeed;
    }
    protected void EndPowerStrike()
    {
        _strikeCollider.enabled = false;
        _enemyRb.velocity = Vector2.zero;
    }
    protected override void TryToDamage(Collider2D enemy)
    {
        if (_currentState == EnemyState.Idle || _currentState == EnemyState.Shoot || _currentState == EnemyState.Hurt)
        {
            return;
        }
        base.TryToDamage(enemy);
    }
    private bool CanAttack()
    {
        return Vector2.Distance(transform.position, _player.transform.position) < _currentStrikeRange;

    }

    private IEnumerator BeginNewCircle()
    {
        if(_currentState == EnemyState.Death)
        {
            yield break;
        }
        if (_fightStarted)
        {
            _IsAngry = false;
            CheckPlayerInRange();
            if (!_IsAngry)
            {
                _fightStarted = false;
                StartCoroutine(ScanForPlayer());
                yield break;
            }
            yield return new WaitForSeconds(_waitTime);
        }
        _fightStarted = true;
        TurnForPlayer();
        ChooseNextAttackState();
    }
#endregion

}
