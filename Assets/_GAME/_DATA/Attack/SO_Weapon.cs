using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 2)]
public class SO_Weapon : ScriptableObject
{
    public string _name;
    public int _attack;
    public float _timeBeforeFinishAttacking;
}
