using DoozyUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class HelpUI : MonoBehaviour
    {
        [SerializeField] private UIElement help1;
        [SerializeField] private UIElement help2;
        [SerializeField] private UIElement help3;

        [SerializeField] private UIButton playButton;

        [SerializeField] private Color textColorDisabled;
        [SerializeField] private Color textColorNormal;

        [SerializeField] private string firstInAnim;
        [SerializeField] private string lastOutAnim;
        [SerializeField] private string transitionInAnim;
        [SerializeField] private string transitionOutAnim;
    }
}