using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SegarioFeedbackMaker : MonoBehaviour
{
    //Shake Varibales
    bool _isShaking;
    float _timerToShake;
    GameObject _shakeObject;
    float _thisForce;

    //Freeze Frame Variables
    bool _isFreezed;
    float _timeFreezed;

    //Transition Variables
    GameObject _transitionObject;
    float _sceneChangeTimer;
    string _nameOfScene = "SampleScene";
    bool _isTransitionning;

    //UI Flash Variables
    bool _isFlashing;
    Image _flashImage;
    float _timeForFlash;
    float _timerForFlash;
    float _maxFlashIntensity;

    //Light Generator Variables
    bool _isLightGeneratorActivated;
    float _timeForLightGenerator;
    bool _hasAnEnd;
    Light _thisLight;
    float _timerForLightGenerator;
    float _maxintensity;

    //Destroying Varibales
    GameObject _toDestroyObject;



    /// <summary>
    /// A function to instantiate a particle at a position
    /// </summary>
    /// <param name="_particle"></param> particleleleleleelelele
    /// <param name="_instantiatePosition"></param>
    public void InstantiateFX(GameObject _particle, Vector3 _instantiatePosition)
    {
        Instantiate(_particle, _instantiatePosition, Quaternion.identity);
    }

    /// <summary>
    /// Use this to shake objects (like screenShake for example). 
    /// </summary>
    /// <param name="_objectToShake"></param>
    /// <param name="_force"></param>
    /// <param name="_timeToShake"></param>
    public void ScreenShake(GameObject _objectToShake, float _force, float _timeToShake)
    {
        _shakeObject = _objectToShake;
        _thisForce = _force;
        _timerToShake = _timeToShake;
        _isShaking = true;
    }


    /// <summary>
    /// Use this function to Create a freeze frame (screen freeze during a certain time). The freeze time is the duration of the Freeze frame;
    /// </summary>
    /// <param name="_particle"></param> particleleleleleelelele
    /// <param name="_instantiatePosition"></param>
    public void FreezeFrame(float _freezeTime)
    {
        _isFreezed = true;
        _timeFreezed = _freezeTime;
    }

    /// <summary>
    /// Use this function to Create a transition between scenes;
    /// </summary>
    /// <param name="_particle"></param> particleleleleleelelele
    /// <param name="_instantiatePosition"></param>
    public void Transition(GameObject _transitionPanel, float _timeBeforeChangingScene, string _sceneName)
    {
        _isTransitionning = true;
        _transitionObject = _transitionPanel;
        _sceneChangeTimer = _timeBeforeChangingScene;
        _nameOfScene = _sceneName;
        _transitionObject.SetActive(true);
    }

    /// <summary>
    /// Use this function to Create Flash effects on Screen (using UI)
    /// </summary>
    /// <param name="_particle"></param> particleleleleleelelele
    /// <param name="_instantiatePosition"></param>
    public void Flash(Image _flashPanel, float _flashDuration, float _maxIntensity)
    {
        _isFlashing = true;
        _flashImage = _flashPanel;
        _timeForFlash = _flashDuration;
        _timerForFlash = _flashDuration;
        _maxFlashIntensity = _maxIntensity;
    }

    /// <summary>
    /// Use this function to Create Light effects 
    /// </summary>
    /// <param name="_particle"></param> particleleleleleelelele
    /// <param name="_instantiatePosition"></param>
    public void GenerateLight(Light _lightSource, bool _canLightFade, float _lifetimeOfLight, float _maxIntensity)
    {
        _thisLight = _lightSource;
        _hasAnEnd = _canLightFade;
        _timeForLightGenerator = _lifetimeOfLight;
        _timerForLightGenerator = _timeForLightGenerator;
        _isLightGeneratorActivated = true;
        _maxintensity = _maxIntensity;
    }

    /// <summary>
    /// A function to delay the destroy of a gameobject
    /// </summary>
    /// <param name="_objectToDestroy"></param>
    /// <param name="_timeBeforeDestroying"></param>
    public void DestroyWithDelay(GameObject _objectToDestroy, float _timeBeforeDestroying)
    {
        _toDestroyObject = _objectToDestroy;
        Invoke("Destroyer", _timeBeforeDestroying);

    }

    private void Update()
    {
        //SHAKE MANAGER
        if (_isShaking)
        {
            _timerToShake -= Time.deltaTime;
            Vector3 originPos = _shakeObject.transform.position;
            Vector3 positionShake = Random.onUnitSphere * (_thisForce / 100) + originPos;
            _shakeObject.transform.position = positionShake;
        }

        if (_timerToShake <= 0)
        {

            _isShaking = false;
        }



        //FREEZE FRAME MANAGEMENT
        if (_isFreezed)
        {
            _timeFreezed -= Time.deltaTime * 100;
            Time.timeScale = 0.01f;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (_timeFreezed <= 0)
        {
            _isFreezed = false;
        }



        //TRANSITION MANAGEMENT
        if (_isTransitionning)
        {
            _sceneChangeTimer -= Time.deltaTime;

            if (_sceneChangeTimer <= 0)
            {

                SceneManager.LoadScene(_nameOfScene);
                _isTransitionning = false;
            }
        }




        //LIGHT GENERATOR MANAGEMENT
        if (_isLightGeneratorActivated)
        {
            _timerForLightGenerator -= Time.deltaTime;


            if (_hasAnEnd)
            {
                if (_timerForLightGenerator > _timeForLightGenerator / 2)
                {
                    _thisLight.intensity = (_maxintensity / (_timerForLightGenerator / (_timeForLightGenerator)) - _maxintensity) * 2;
                }
                else
                {
                    _thisLight.intensity = _maxintensity * 2 - (_maxintensity / (_timerForLightGenerator / (_timeForLightGenerator)) - (_maxintensity * 2)) / 2;
                }

                if (_timerForLightGenerator <= 0)
                {
                    _isLightGeneratorActivated = false;
                    _thisLight.intensity = 0;
                }
            }
            else
            {
                if (_timerForLightGenerator >= 0)
                {
                    _thisLight.intensity = _maxintensity - (_maxintensity * (_timerForLightGenerator / _timeForLightGenerator));
                }

                if (_timerForLightGenerator >= _maxintensity)
                {
                    _thisLight.intensity = _maxintensity;
                }
            }
        }


        //UI sFLASH MANAGEMENT
        if (_isFlashing)
        {
            Color _flashColor = _flashImage.color;

            _timerForFlash -= Time.deltaTime;

            if (_timerForFlash > _timeForFlash / 2)
            {
                _flashColor.a = (_maxFlashIntensity / (_timerForFlash / (_timeForFlash)) - _maxFlashIntensity) * 2;
            }
            else
            {
                _flashColor.a = _maxFlashIntensity * 2 - (_maxFlashIntensity / (_timerForFlash / (_timeForFlash)) - _maxFlashIntensity / 2) / 2;
            }

            if (_timerForFlash <= 0)
            {
                _isFlashing = false;
                _flashColor.a = 0;
            }
            _flashImage.color = _flashColor;
        }
    }

    void Destroyer()
    {
        GameObject.Destroy(_toDestroyObject);
    }
}
