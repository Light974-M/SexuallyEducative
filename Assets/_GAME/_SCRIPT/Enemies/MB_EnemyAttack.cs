using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_EnemyAttack : MonoBehaviour
{
    MB_EnemyController _enemyController;
    SO_Enemy _enemySo;
    SO_Weapon _weaponSO;
    [HideInInspector]
    public bool _isAttacking;

    [SerializeField] private GameObject _weapon;

    int _attack;
    float _attackTimer;

    MB_WeaponController _weaponController;

    public bool _canAttack;
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<MB_EnemyController>();
        _weaponController = GetComponentInChildren<MB_WeaponController>();
        _enemySo = _enemyController._soEnemy;
        _weaponSO = _enemyController._soWeapon;
        _attack = _enemySo._attack;
        _weaponController._attack = _enemySo._attack + _weaponSO._attack;
    }

    // Update is called once per frame
    void Update()
    {
        if(_canAttack)
        {
            _attackTimer -= Time.deltaTime;
        }
        

        if(_attackTimer <= 0)
        {
            _attackTimer = Random.Range(_enemySo._minTimeBeforeAttack, _enemySo._maxTimeBeforeAttack);
            Attack();
        }
    }

    void Attack()
    {
        _isAttacking = true;
        _weapon.SetActive(true);
        Invoke("FinishAttack", _weaponSO._timeBeforeFinishAttacking);
    }

    void FinishAttack()
    {
        _isAttacking = false;
        _weapon.SetActive(false);
    }
}
