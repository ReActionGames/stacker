using DoozyUI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class HelpUI : MonoBehaviour
    {
        [TabGroup("Screens")]
        [SerializeField] private UIElement help1;
        [TabGroup("Screens")]
        [SerializeField] private UIElement help2;
        [TabGroup("Screens")]
        [SerializeField] private UIElement help3;

        [TabGroup("Buttons")]
        [SerializeField] private UIButton playButton;
        [TabGroup("Buttons")]
        [SerializeField] private UIButton next1, next2, previous1, previous2;

        [TabGroup("Play Text")]
        [SerializeField] private TMPro.TextMeshProUGUI playButtonText;
        [TabGroup("Play Text")]
        [SerializeField] private Color textColorDisabled;
        [TabGroup("Play Text")]
        [SerializeField] private Color textColorNormal;

        [TabGroup("Anims")]
        [SerializeField] private string firstInAnimCategory, firstInAnimName, lastOutAnimCategory, lastOutAnimName, 
                                        transitionRightInAnimCategory, transitionRightInAnimName, 
                                        transitionRightOutAnimCategory, transitionRightOutAnimName,
                                        transitionLeftInAnimCategory, transitionLeftInAnimName,
                                        transitionLeftOutAnimCategory, transitionLeftOutAnimName;

        private Anim firstInAnim
        {
            get
            {
                return UIAnimatorUtil.GetInAnim(firstInAnimCategory, firstInAnimName);
            }
        }
        private Anim lastOutAnim
        {
            get
            {
                return UIAnimatorUtil.GetOutAnim(lastOutAnimCategory, lastOutAnimName);
            }
        }
        private Anim transitionRightInAnim
        {
            get
            {
                return UIAnimatorUtil.GetInAnim(transitionRightInAnimCategory, transitionRightInAnimName);
            }
        }
        private Anim transitionRightOutAnim
        {
            get
            {
                return UIAnimatorUtil.GetOutAnim(transitionRightOutAnimCategory, transitionRightOutAnimName);
            }
        }
        private Anim transitionLeftInAnim
        {
            get
            {
                return UIAnimatorUtil.GetInAnim(transitionLeftInAnimCategory, transitionLeftInAnimName);
            }
        }
        private Anim transitionLeftOutAnim
        {
            get
            {
                return UIAnimatorUtil.GetOutAnim(transitionLeftOutAnimCategory, transitionLeftOutAnimName);
            }
        }

        private void Awake()
        {
            next1.OnClick.AddListener(Next1ButtonOnClick);
            next2.OnClick.AddListener(Next2ButtonOnClick);
            previous1.OnClick.AddListener(Previous1ButtonOnClick);
            previous2.OnClick.AddListener(Previous2ButtonOnClick);

            InitialSetUp();
        }

        private void InitialSetUp()
        {
            help1.inAnimations = firstInAnim;
            help1.outAnimations = transitionLeftOutAnim;
            help2.inAnimations = transitionRightInAnim;
            help2.outAnimations = transitionLeftOutAnim;
            help3.inAnimations = transitionRightInAnim;
            help3.outAnimations = transitionRightOutAnim;

            playButton.Interactable = false;
            playButtonText.color = textColorDisabled;
        }

        private void Next1ButtonOnClick()
        {
            help1.outAnimations = transitionLeftOutAnim;
            help2.inAnimations = transitionRightInAnim;

            help1.Hide(false);
            help2.Show(false);
        }

        private void Next2ButtonOnClick()
        {
            help2.outAnimations = transitionLeftOutAnim;
            help3.inAnimations = transitionRightInAnim;

            help2.Hide(false);
            help3.Show(false);
        }

        private void Previous1ButtonOnClick()
        {
            help2.outAnimations = transitionRightOutAnim;
            help1.inAnimations = transitionLeftInAnim;

            help2.Hide(false);
            help1.Show(false);
        }

        private void Previous2ButtonOnClick()
        {
            help3.outAnimations = transitionRightOutAnim;
            help2.inAnimations = transitionLeftInAnim;

            help3.Hide(false);
            help2.Show(false);
        }
    }
}