using Sirenix.OdinInspector;
using Stacker.Enums;
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

        private static InputTriggerType inputTrigger = 0;        

        public enum TouchInputType
        {
            SwipeDown,
            TapLeft,
            TapCenter,
            TapRight
        }

        public void InputDetected(int type)
        {
            TouchInputType input = (TouchInputType)type;
            EvaluateInput(input);
        }

        private void EvaluateInput(TouchInputType type)
        {
            switch (type)
            {
                case TouchInputType.SwipeDown:
                    inputTrigger = InputTriggerType.TetroDrop;
                    break;
                case TouchInputType.TapCenter:
                    inputTrigger = InputTriggerType.TetroClockwise;
                    break;
                case TouchInputType.TapLeft:
                    inputTrigger = InputTriggerType.TetroLeft;
                    break;
                case TouchInputType.TapRight:
                    inputTrigger = InputTriggerType.TetroRight;
                    break;
            }
        }

        public static InputTriggerType GetTrigger()
        {
            return inputTrigger;
        }

        public static void ResetTrigger()
        {
            inputTrigger = 0;
        }

    }
}