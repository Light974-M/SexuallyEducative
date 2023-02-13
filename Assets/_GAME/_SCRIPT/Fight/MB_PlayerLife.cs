using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_PlayerLife : MonoBehaviour
{

   public SO_Enemy _enemySo;
    [SerializeField] MB_WeaponController _weaponController;

    [HideInInspector]
    public float _life;
    [SerializeField] SO_Weapon _weaponSO;
    //Life Bar Variables
    [SerializeField, Tooltip("The bar following equals to the life")] Transform jaugeTransform;
    [SerializeField, Tooltip("The bar following the first bar for a better effect")] Transform secondaryJauge;

    bool canDie;
    SegarioFeedbackMaker _feedbackMaker;
    [SerializeField] private GameObject _deathFX;
    [SerializeField] GameObject _cam;

    [SerializeField] private Animator _allyAnim;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private GameObject _visu;

    public bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _weaponController._attack = _enemySo._attack + _weaponSO._attack;
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
            Invoke("Death", 1);
            
            _feedbackMaker.FreezeFrame(0.6f);
            _feedbackMaker.InstantiateFX(_deathFX, this.transform.position);
            _life = 0;
           
            canDie = false;
        }

        if (_life >=  _enemySo._originalLife)
        {
            _life = _enemySo._originalLife;
        }

        //Life Bar Management
        Vector3 interpolatedScale = Vector3.Lerp(secondaryJauge.transform.localScale, jaugeTransform.transform.localScale, Time.deltaTime * 3);
        secondaryJauge.transform.localScale = interpolatedScale;
        jaugeTransform.localScale = new Vector3((float)(_life / _enemySo._originalLife), 1, 1);

    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "EnemyWeapon")
        {
            _feedbackMaker.ScreenShake(_cam, 1, 1);
            _feedbackMaker.FreezeFrame(1);
            Debug.Log("Hit Player");
            _weaponController = collision.gameObject.GetComponent<MB_WeaponController>();
            _life -= _weaponController._attack;
        }
    }

    //Death of the enemy
    void Death()
    {
        if(_isPlayer)
        {
            Destroy(_visu);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
