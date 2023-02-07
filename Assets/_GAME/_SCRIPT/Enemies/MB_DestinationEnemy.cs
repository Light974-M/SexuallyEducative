using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_DestinationEnemy : MonoBehaviour
{
    void OnDrawGizmos()
    {
       
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(this.transform.position, 0.75f);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z), new Vector3(0.5f, 2, 0.5f));
    }
}
