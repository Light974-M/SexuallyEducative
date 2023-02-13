using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UPDB.Data.VisualNovelManager
{
    [CreateAssetMenu(fileName = "new Panel Data", menuName = "ScriptableObjects/VisualNovelManager/Panel Data")]
    public class PanelData : ScriptableObject
    {
        [SerializeField, Tooltip("prefab that contain panel to load")]
        private GameObject _panelPrefab;

        [SerializeField, Tooltip("is this panel to destroy, or just to disable for saving changes")]
        private bool _isSavingProof = true;

        [Header("TRANSFORM CAMERA")]
        [SerializeField, Tooltip("position of camera when current panel is loaded")]
        private Vector3 _cameraPosition;

        [SerializeField, Tooltip("rotation of camera when current panel is loaded")]
        private Quaternion _cameraRotation;

        #region Public API

        public GameObject PanelPrefab
        {
            get { return _panelPrefab; }
            set { _panelPrefab = value; }
        }

        public Vector3 CameraPosition
        {
            get { return _cameraPosition; }
            set { _cameraPosition = value; }
        }

        public Quaternion CameraRotation
        {
            get { return _cameraRotation; }
            set { _cameraRotation = value; }
        }

        public bool IsSavingProof => _isSavingProof;

        #endregion

        public void WriteCameraPosValue()
        {
            VisualNovelManager.Instance.Camera.transform.position = _cameraPosition;
            VisualNovelManager.Instance.Camera.transform.rotation = _cameraRotation;
        }
    }
}
