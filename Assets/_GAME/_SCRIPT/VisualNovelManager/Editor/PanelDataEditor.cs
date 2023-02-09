using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UPDB.Data.VisualNovelManager
{
    [CustomEditor(typeof(PanelData)), CanEditMultipleObjects]
    public class PanelDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PanelData myTarget = (PanelData)target;

            base.OnInspectorGUI();

            if (VisualNovelManager.Instance.Camera != null)
                myTarget.WriteCameraPosValue();
        }
    }
}
