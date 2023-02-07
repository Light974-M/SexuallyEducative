using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_EnemyLife : MonoBehaviour
{
    MB_EnemyController _enemyController;
    SO_Enemy _enemySo;

    float _life;

    //Life Bar Variables
    [SerializeField, Tooltip("The bar following equals to the life")] Transform jaugeTransform;
    [SerializeField, Tooltip("The bar following the first bar for a better effect")] Transform secondaryJauge;

    bool canDie;
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<MB_EnemyController>();
        _enemySo = _enemyController._soEnemy;
        _life = _enemySo._originalLife;
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If the life is inferior to 0
        if (_life <= 0 && canDie)
        {
            _life = 0;
            Invoke("Death", 1);
            canDie = false;
        }

        //Life Bar Management
        Vector3 interpolatedScale = Vector3.Lerp(secondaryJauge.transform.localScale, jaugeTransform.transform.localScale, Time.deltaTime * 3);
        secondaryJauge.transform.localScale = interpolatedScale;
        jaugeTransform.localScale = new Vector3((float)(_life / _enemySo._originalLife), 1, 1);

    }

    //Death of the enemy
    void Death()
    {
        Destroy(gameObject);
    }
}
