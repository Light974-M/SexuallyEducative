using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UPDB.Data.VisualNovelManager
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField, Tooltip("list of all panels linked to this panel")]
        private PanelData[] _linkedPanels;

        [SerializeField, Tooltip("last save of instance prefab")]
        private GameObject _panelInstancePrefabSave;

        #region Public API

        public GameObject PanelInstancePrefabSave
        {
            get { return _panelInstancePrefabSave; }
            set { _panelInstancePrefabSave = value; }
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadPanel(int index)
        {
            if (index >= 0 && index < _linkedPanels.Length)
            {
                if (!_linkedPanels[index].IsSavingProof || _linkedPanels[index].PanelPrefab.GetComponent<PanelManager>().PanelInstancePrefabSave == null))
                    Instantiate(_linkedPanels[index].PanelPrefab, transform.parent);

                VisualNovelManager.Instance.Camera.transform.position = _linkedPanels[index].CameraPosition;
                VisualNovelManager.Instance.Camera.transform.rotation = _linkedPanels[index].CameraRotation;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError($"you are trying to access index N°{index} wich doesn't exist in {_linkedPanels} that has a length of {_linkedPanels.Length}");
            }
        }
    }
}
