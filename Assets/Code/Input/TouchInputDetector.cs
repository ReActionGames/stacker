using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class TouchInputDetector : MonoBehaviour
    {

        #region InfoBox

#if UNITY_EDITOR
        [OnInspectorGUI, PropertyOrder(int.MinValue)]
        private void DrawInfoBox()
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.InfoMessageBox("Touch Input Type:\n0 = SwipeDown\n1 = TapLeft\n2 = TapCenter\n3 = TapRight");
        }
#endif

        #endregion

        public enum TouchInputType
        {
            SwipeDown,
            TapLeft,
            TapCenter,
            TapRight
        }

        public void InputDetected(int type)
        {

        }

    }
}