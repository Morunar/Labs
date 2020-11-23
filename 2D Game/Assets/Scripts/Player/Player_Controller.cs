using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private ServiceManager _serviceManager;
    [SerializeField] private int _maxHP;
    private int _currentHP;
    [SerializeField] private int _maxMP;
    private int _currentMP;
    [Header("Armour")]
    [SerializeField] private int _armour;
    private int _maxArmour;
    [SerializeField] private int _numOfArmour;
    [SerializeField] Image[] armour;
    [SerializeField] Sprite fullArmour;
    [SerializeField] Sprite emptyArmour;

    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _mpSlider;

    Movement_controller _playerMovement;
    Vector2 _startPosition;

    private bool _canBeDamaged = true;

    void Start()
    {
        _playerMovement = GetComponent<Movement_controller>();
        _playerMovement.OnGetHurt += OnGetHurt;
        _currentHP = _maxHP;
        _currentMP = _maxMP;
        _maxArmour = _armour;
        _hpSlider.maxValue = _maxHP;
        _hpSlider.value = _maxHP;
        _mpSlider.maxValue = _maxMP;
        _mpSlider.value = _maxMP;
        _startPosition = transform.position;
        _serviceManager = ServiceManager.Instanse;
    }
    private void Update()
    {
        if( _armour > _numOfArmour)
        {
            _armour = _numOfArmour;
        }


        for (int i = 0; i < armour.Length; i++)
        {
            if (i < _armour)
            {
                armour[i].sprite = fullArmour;
            }
            else
            {
                armour[i].sprite = emptyArmour;
            }
            if (i < _numOfArmour)
            {
                armour[i].enabled = true;
            }
            else
            {
                armour[i].enabled = false;
            }
        }
    }
    public void TakeDamage(int damage, DamageType type = DamageType.Casual, Transform enemy = null)
    {
        if (!_canBeDamaged)
        {
            return;
        }
        if (_armour > 0)
        {
            _currentHP -= damage / 2;
            Debug.Log("Current morale = " + _currentHP);
            _armour -= 1;
        }
        else
        {
            _currentHP -= damage;
            Debug.Log("Current damage = " + _currentHP);
        }
        
       
        
        if (_currentHP <= 0)
        {
            OnDeath();
        }

        switch (type)
        {
            case DamageType.PowerStrike:
                _playerMovement.GetHurt(enemy.position);
                break;
        }
        _hpSlider.value = _currentHP;
    }

    private void OnGetHurt(bool canBeDamaged)
    {
        _canBeDamaged = canBeDamaged;
    }


    public void RestroreHP(int hp)
    {
        _currentHP += hp;
        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
        _hpSlider.value = _currentHP;
    }

    public void RestroreMP(int mp)
    {
        _currentMP += mp;
        if (_currentMP > _maxMP)
        {
            _currentMP = _maxMP;
        }
        _mpSlider.value = _currentMP;
    }
    public void RestoreArmour(int armour)
    {
        _armour += armour;
        if (_armour > _maxArmour)
        {
            _armour = _maxArmour;
        }
        
    }
    public bool ChangeMP(int value)
    {
        if(value < 0 && _currentMP < Mathf.Abs(value))
        {
            return false;
        }
        
        _currentMP += value;
        if (_currentMP > _maxMP)
        {
            _currentMP = _maxMP;      
        }
        _mpSlider.value = _currentMP;
        return true;
    }
    public void OnDeath()
    {
        _serviceManager.Restart();
    }
    
}
