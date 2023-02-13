using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MB_EnemyController : MonoBehaviour
{
    [Header("ENEMY TYPE")]
    [Tooltip("Drag and drop here the Scriptable Object of the enemy you want it to be!")] 
    public SO_Enemy _soEnemy;
    [Tooltip("Drag and drop here the Scriptable Object of the weapon you want the enemy to use!")]
    public SO_Weapon _soWeapon;

    //Behaviour Variables
    float _walkSpeed;
    float _runSpeed;
    bool _canFollowplayer;


    Transform _destination;
    NavMeshAgent _enemyAgent;
    GameObject _player;
    bool _isFollowingPlayer;

    //Random Destinations Variables
    private GameObject[] _availableDestinations;
    Transform _randomDestination;
    [HideInInspector] public float _timeToChangeDirection;

    [Header("GIZMOS")]
    [SerializeField] private bool _enableDetectionGizmos;
    MB_EnemyAttack _enemyAttack;

    public MB_EnemyDetector _detector;

    // Start is called before the first frame update
    void Start()
    {
        //NavMesh Variables
        _enemyAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _availableDestinations = GameObject.FindGameObjectsWithTag("Destination");
        _randomDestination = _availableDestinations[Random.Range(0, _availableDestinations.Length)].transform;
        _enemyAttack = GetComponent<MB_EnemyAttack>();

        //Variables get Scriptables Value
        _walkSpeed = _soEnemy._walkSpeed;
        _runSpeed = _soEnemy._runSpeed;
        _canFollowplayer = _soEnemy._canFollowPlayer;

       
    }

    // Update is called once per frame
    void Update()
    {
      
        if(_detector._target != null)
        {
            _player = _detector._target;
        }
        else
        {
            _player = GameObject.FindWithTag("Player");
        }

        //Distance with player detection
        float _playerDistance = Vector3.Distance(_player.transform.position, transform.position);

        //Checking if the player is at the good distance to follow him
        if (!_isFollowingPlayer)
        {
            _enemyAgent.speed = _walkSpeed;

            if (_playerDistance <= _soEnemy._playerDistanceDetection)
            {
                _destination = _player.transform;
                _isFollowingPlayer = true;
            }
            else
            {
                _destination = _randomDestination.transform;
            }
        }
      

        //Checking if the player is at the good distance to unfollow him
        if(_isFollowingPlayer)
        {
            _enemyAgent.speed = _runSpeed;

            if (_playerDistance > _soEnemy._playerUnfollowDistance)
            {
                _destination = _randomDestination.transform;
                _isFollowingPlayer = false;
            }
            else
            {
                _destination = _player.transform;
            }
        }

        //If is close enough to attack player
        if (_playerDistance <= _soEnemy._playerAttackDistance)
        {
            _enemyAttack._canAttack = true;
        }
        else
        {
            _enemyAttack._canAttack = false;
        }

        //Random Destination Change
        if (!_isFollowingPlayer)
        {
            _timeToChangeDirection -= Time.deltaTime;

            if(_timeToChangeDirection <= 0)
            {
                Debug.Log("yes");
                _randomDestination = _availableDestinations[Random.Range(0, _availableDestinations.Length)].transform;
                _timeToChangeDirection = Random.Range(_soEnemy._minTimeToChangeDirection, _soEnemy._maxTimeToChangeDirection);
            }
        }

        if(_player == null)
        {
            _destination = _randomDestination.transform;
        }
        //Destination manager
        _enemyAgent.SetDestination(_destination.position);
    }

    private void OnDrawGizmos()
    {
        if(_enableDetectionGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, _soEnemy._playerDistanceDetection);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _soEnemy._playerUnfollowDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, _soEnemy._playerAttackDistance);
        }
    
    }
}