using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_Heal : MonoBehaviour
{
    MB_PlayerLife _playerLife;
    SO_Enemy _soLife;
    [SerializeField] private int lifeToGive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            _playerLife = other.gameObject.GetComponent<MB_PlayerLife>();
            _soLife = _playerLife._enemySo;
            _playerLife._life += _soLife._originalLife / 3;
        }
    }
}
