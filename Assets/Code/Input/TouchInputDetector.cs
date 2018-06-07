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
            Sirenix.Utilities.Editor.SirenixEditorGUI
                .InfoMessageBox("Touch Input Type:\n0 = SwipeDown\n1 = TapLeft\n2 = TapCenter\n3 = TapRight\n4 = SwipeLeft\n5 = SwipeRight\n6 = TapAnywhere");
        }
#endif

        #endregion

        private static InputTriggerType inputTrigger = 0;        

        public enum TouchInputType
        {
            SwipeDown,
            TapLeft,
            TapCenter,
            TapRight,
            SwipeLeft,
            SwipeRight,
            TapAnywhere
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
                case TouchInputType.TapAnywhere:
                    inputTrigger = InputTriggerType.TetroClockwise;
                    break;
                case TouchInputType.TapLeft:
                case TouchInputType.SwipeLeft:
                    inputTrigger = InputTriggerType.TetroLeft;
                    break;
                case TouchInputType.TapRight:
                case TouchInputType.SwipeRight:
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