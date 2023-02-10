using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_EnemyDetector : MonoBehaviour
{
    public GameObject _target;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ally")
        {
            _target = other.gameObject;
        }
    }
}
