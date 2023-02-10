using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_ClosestEnemyDetection : MonoBehaviour
{
    public GameObject _target;
     GameObject _secondTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            _target = _secondTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy")
        {
            _secondTarget = other.gameObject;
            if (_secondTarget == null)
            {
                
            }
        }
      

    }
}
