using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class SO_Enemy : ScriptableObject
{
    public string _name;

    [Header("Movements Variables")]
    [Tooltip("Determines if the enemy can follow the player")]
    public bool _canFollowPlayer;

    [Tooltip("The maximum speed it will reach when walking"), Range(0, 5)]
    public float _walkSpeed;

    [Tooltip("The maximum speed it will reach when running"), Range(0, 10)]
    public float _runSpeed;

    [Tooltip("The distance where it begins to follow player"), Range(2, 20)]
    public float _playerDistanceDetection;

    [Tooltip("The distance where it stops to follow player"), Range(2, 30)]
    public float _playerUnfollowDistance;

    [Header("Change Direction"), Range(2, 15)]
    public int _minTimeToChangeDirection;

    [ Range(2, 15)]
    public int _maxTimeToChangeDirection;

    [Header("Fight Variables")]
    [Tooltip("The original life points of the enemy"), Range(10, 100)]
    public float _originalLife;

    [Tooltip("The attack of the enemy"), Range(1, 25)]
    public int _attack;
}
