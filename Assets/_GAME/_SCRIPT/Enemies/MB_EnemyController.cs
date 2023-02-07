using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MB_EnemyController : MonoBehaviour
{
    [Tooltip("Drag and drop here the Scriptable Object of the enemy you want it to be!")] 
    public SO_Enemy _soEnemy;

    //Behaviour Variables
    float _walkSpeed;
    float _runSpeed;
    bool _canFollowplayer;


    Transform _destination;
    NavMeshAgent _enemyAgent;
    GameObject _player;
    bool _isFollowingPlayer;

    //Random Destinations Variables
    [SerializeField, Tooltip("Drag and drop here the Transforms that will be the destinations when the enemy isn't following the player")] 
    private GameObject[] _availableDestinations;
    Transform _randomDestination;
    public float _timeToChangeDirection;

    // Start is called before the first frame update
    void Start()
    {
        //NavMesh Variables
        _enemyAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _availableDestinations = GameObject.FindGameObjectsWithTag("Destination");
        _randomDestination = _availableDestinations[Random.Range(0, _availableDestinations.Length)].transform;

        //Variables get Scriptables Value
        _walkSpeed = _soEnemy._walkSpeed;
        _runSpeed = _soEnemy._runSpeed;
        _canFollowplayer = _soEnemy._canFollowPlayer;
    }

    // Update is called once per frame
    void Update()
    {
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

        //Random Destination Change
        if(!_isFollowingPlayer)
        {
            _timeToChangeDirection -= Time.deltaTime;

            if(_timeToChangeDirection <= 0)
            {
                Debug.Log("yes");
                _randomDestination = _availableDestinations[Random.Range(0, _availableDestinations.Length)].transform;
                _timeToChangeDirection = Random.Range(_soEnemy._minTimeToChangeDirection, _soEnemy._maxTimeToChangeDirection);
            }
        }


        //Destination manager
        _enemyAgent.SetDestination(_destination.position);
    }
}