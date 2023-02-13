using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MB_AllyController : MonoBehaviour
{
    [Header("ENEMY TYPE")]
    [Tooltip("Drag and drop here the Scriptable Object of the ally you want it to be!")]
    public SO_Ally _soAlly;
    [Tooltip("Drag and drop here the Scriptable Object of the weapon you want the enemy to use!")]
    public SO_Weapon _soWeapon;

    //Behaviour Variables
    float _walkSpeed;
    float _runSpeed;

    Transform _destination;
    NavMeshAgent _enemyAgent;
    GameObject _player;
    bool _isFollowingPlayer;

    [Header("GIZMOS")]
    [SerializeField] private bool _enableDetectionGizmos;
    MB_EnemyAttack _enemyAttack;
    public bool isAttacking;


    [SerializeField] MB_PlayerLife _life;

   public  GameObject _target;
   public  GameObject _secondtarget;

    public MB_ClosestEnemyDetection _detector;

    // Start is called before the first frame update
    void Start()
    {
        //NavMesh Variables
        _enemyAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _enemyAttack = GetComponent<MB_EnemyAttack>();

        //Variables get Scriptables Value
        _walkSpeed = _soAlly._walkSpeed;
        _runSpeed = _soAlly._runSpeed;

       

    }

    // Update is called once per frame
    void Update()
    {
        if(_detector._target != null)
        {
            Debug.DrawLine(this.transform.position, _detector._target.transform.position, Color.red);
            _target = _detector._target;
        }
       
       
        //Distance with player detection
        float _playerDistance = Vector3.Distance(_player.transform.position, transform.position);


        if(_target != null)
        {
            float _enemyDistance = Vector3.Distance(_target.transform.position, transform.position);

            //If is close enough to attack player
            if (_enemyDistance <= _soAlly._ennemyAttackDistance)
            {

                _enemyAttack._canAttack = true;
            }
            else
            {
                _enemyAttack._canAttack = false;
            }
        }
    
        if(_life._isDead)
        {
            _enemyAgent.speed = 0;
        }
        else
        {

            //Distance Setter
            if (_playerDistance <= _soAlly._playerDistanceDetection && _playerDistance > _soAlly._playerDistanceToStop)
            {

                _enemyAgent.speed = _walkSpeed;
            }
            if (_playerDistance <= _soAlly._playerDistanceToStop)
            {

                _enemyAgent.speed = 0;
            }
            if (_playerDistance > _soAlly._playerDistanceDetection)
            {
                _enemyAgent.speed = _runSpeed;
            }
        }


    
       

        if (_detector._target != null)
        {
            _enemyAgent.SetDestination(_target.transform.position);
        }
        else
        {
            _enemyAgent.SetDestination(_player.transform.position);
        }

       
    }

    private void OnDrawGizmos()
    {
        if (_enableDetectionGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, _soAlly._playerDistanceDetection);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, _soAlly._ennemyAttackDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _soAlly._playerDistanceToStop);
        }

    }

   
}
