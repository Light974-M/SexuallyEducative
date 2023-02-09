using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UPDB.Data.VisualNovelManager
{
    public class VisualNovelManager : MonoBehaviour
    {
        #region Singleton Initialize

        /// <summary>
        /// unique instance of GameManager class
        /// </summary>
        private static VisualNovelManager _instance;

        public static VisualNovelManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<VisualNovelManager>();

                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        #endregion

        [SerializeField, Tooltip("camera used by game")]
        private Camera _camera;

        [SerializeField, Tooltip("first panel loaded at loading of scene")]
        private PanelData _startPanel;

        /// <summary>
        /// game object to store every panels object
        /// </summary>
        private GameObject _panelsParent;

        #region Public API

        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        #endregion

        /// <summary>
        /// awake is called when script instance is being loaded
        /// </summary>
        private void Awake()
        {
            Initialize();

            if (_startPanel != null)
            {
                if (_panelsParent.transform.childCount == 0)
                    Instantiate(_startPanel.PanelPrefab, _panelsParent.transform);

                _camera.transform.position = _startPanel.CameraPosition;
                _camera.transform.rotation = _startPanel.CameraRotation; 
            }
        }

        /// <summary>
        /// OnDrawGizmos is called each time scene refresh
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                Initialize();
        }

        private void Initialize()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                if (Application.isPlaying)
                    Destroy(this);
                else
                    DestroyImmediate(this);
            }

            if (_camera == null)
            {
                _camera = Camera.main;

                if (_camera == null)
                {
                    _camera = FindObjectOfType<Camera>();

                    if (_camera == null)
                        _camera = new GameObject("Camera").AddComponent<Camera>();
                }
            }

            if (transform.Find("Panels") != null)
                _panelsParent = transform.Find("Panels").gameObject;

            if (_panelsParent == null)
            {
                _panelsParent = new GameObject("Panels");
                _panelsParent.transform.parent = transform;
                _panelsParent.AddComponent<RectTransform>().localPosition = Vector3.zero;
                _panelsParent.GetComponent<RectTransform>().localScale = Vector3.one;
                _panelsParent.gameObject.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<CanvasScaler>().referenceResolution;
            }
        }
    }
}
