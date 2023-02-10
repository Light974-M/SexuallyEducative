using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ally", order = 3)]
public class SO_Ally : ScriptableObject
{
    public string _name;
    public string _description;

 
    [Tooltip("The maximum speed it will reach when walking"), Range(0, 5)]
    public float _walkSpeed;

    [Tooltip("The maximum speed it will reach when running"), Range(0, 10)]
    public float _runSpeed;

    [Tooltip("The distance where it begins to follow player"), Range(2, 20)]
    public float _playerDistanceDetection;
    [Tooltip("The distance where it begins to follow player"), Range(2, 20)]
    public float _playerDistanceToStop;



    [Tooltip("The distance where it stops to follow player"), Range(0, 15)]
    public float _ennemyAttackDistance;

    [Header("Fight Variables")]
    [Tooltip("The original life points of the enemy"), Range(10, 100)]
    public float _originalLife;

    [Tooltip("The attack of the enemy"), Range(1, 25)]
    public int _attack;

    public float _minTimeBeforeAttack;
    public float _maxTimeBeforeAttack;
}
