using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyShooterController : EnemyControllerBase
{
    protected Player_Controller _player;

    [Header("Shooting")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootSpeed;
    [SerializeField] protected float _angerRange;

    protected bool _IsAngry;
    protected bool _attacking;

   
    #region UnityMethods
    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<Player_Controller>();
        StartCoroutine(ScanForPlayer());
    }

    protected override void Update()
    {
        if (_IsAngry)
        {
            return;
        }
        base.Update();
    }
    #endregion

    #region PrivateMethods
    protected override void ChangeState(EnemyState state)
    {
        base.ChangeState(state);
        switch (state)
        {
            case EnemyState.Shoot:
                _attacking = true;
                _enemyRb.velocity = Vector2.zero;
                break;
        }

    }

    protected override void EndState()
    {
        switch (_currentState)
        {
            case EnemyState.Shoot:
                _attacking = false;
                _enemyAnimator.SetBool("Shoot", false);
                break;
        }
        
        base.EndState();
           
    }
    protected override void ResetState()
    {
        base.ResetState();
        _enemyAnimator.SetBool(EnemyState.Shoot.ToString(), false);

    }
    protected virtual void  DoStateAction()
    {
        switch (_currentState)
        {
            case EnemyState.Shoot:
                Shoot();
                break;
        }
    }

    protected IEnumerator ScanForPlayer()
    {
        while (true)
        {
            CheckPlayerInRange();
            yield return new WaitForSeconds(2f);
        }
    }

    protected virtual void CheckPlayerInRange()
    {
        if (_player == null || _attacking)
        {
            return;
        }

        if (Vector2.Distance(transform.position, _player.transform.position) < _angerRange)
        {
            _IsAngry = true;
            TurnForPlayer();
            ChangeState(EnemyState.Shoot);
        }
        else
        {
            _IsAngry = false;
        }

    }

    protected void TurnForPlayer()
    {
        if (_player.transform.position.x - transform.position.x > 0 && !_faceRight)
        {
            Flip();
        }
        else if (_player.transform.position.x - transform.position.x < 0 && _faceRight)
        {
            Flip();
        }
    }

    protected void Shoot()
    {
        GameObject shoot = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
        shoot.GetComponent<Rigidbody2D>().velocity = transform.right * _shootSpeed;
        shoot.GetComponent<SpriteRenderer>().flipX = !_faceRight;
        Destroy(shoot, 5f);
    }
    #endregion
}
