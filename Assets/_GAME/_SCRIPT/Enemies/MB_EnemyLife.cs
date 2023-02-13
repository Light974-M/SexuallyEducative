using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MB_EnemyLife : MonoBehaviour
{
    MB_EnemyController _enemyController;
    SO_Enemy _enemySo;

    float _life;

    //Life Bar Variables
    [SerializeField, Tooltip("The bar following equals to the life")] Transform jaugeTransform;
    [SerializeField, Tooltip("The bar following the first bar for a better effect")] Transform secondaryJauge;
    MB_WeaponController _weaponController;
    bool canDie;
    SegarioFeedbackMaker _feedbackMaker;
    [SerializeField] private GameObject _deathFX;
    [SerializeField] GameObject _cam;
    [SerializeField] private bool _isBoss;
    [SerializeField] MB_EnemyAttack _ennemyAttack;
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<MB_EnemyController>();
        _enemySo = _enemyController._soEnemy;
        _life = _enemySo._originalLife;
        canDie = true;
        _feedbackMaker = FindObjectOfType<SegarioFeedbackMaker>();

    }

    // Update is called once per frame
    void Update()
    {
        //If the life is inferior to 0
        if (_life <= 0 && canDie)
        {
            _ennemyAttack._canAttack = false;
            _feedbackMaker.ScreenShake(_cam, 1, 1);
            _feedbackMaker.FreezeFrame(1);
            _feedbackMaker.InstantiateFX(_deathFX, this.transform.position);
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
        if(_isBoss)
        {
            SceneManager.LoadScene("Hub");
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            _feedbackMaker.ScreenShake(_cam, 1, 1);
            _feedbackMaker.FreezeFrame(1);
            Debug.Log("Hit Enemy");
            _weaponController = collision.gameObject.GetComponent<MB_WeaponController>();
            _life -= _weaponController._attack;
        }
    }
}
