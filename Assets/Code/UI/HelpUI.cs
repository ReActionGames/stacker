using DoozyUI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Stacker
{
    public class HelpUI : MonoBehaviour
    {
        [TabGroup("Screens")]
        [SerializeField] private UIElement mainMenu, help1, help2, help3;

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
                                        transitionLeftOutAnimCategory, transitionLeftOutAnimName,
                                        playButtonLoopAnimCategory, playButtonLoopAnimName;

        [TabGroup("Elements")]
        [SerializeField] private string screenCategory, mainMenuName, helpName, help1Name, help2Name, help3Name;

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
        private Loop playButtonLoopAnim
        {
            get
            {
                return UIAnimatorUtil.GetLoop(playButtonLoopAnimCategory, playButtonLoopAnimName);
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

        public void ShowHelp()
        {
            PlayerPrefsX.SetBool("first-time", false);

            SetPlayButtonInteractable(false);

            NavigationPointer mainMenuPointer = new NavigationPointer(screenCategory, mainMenuName);
            NavigationPointer helpPointer = new NavigationPointer(screenCategory, helpName);
            NavigationPointer help1Pointer = new NavigationPointer(screenCategory, help1Name);
            NavigationPointerData data = new NavigationPointerData(true);
            data.show.Add(helpPointer);
            data.show.Add(help1Pointer);
            data.hide.Add(mainMenuPointer);

            var temp = mainMenu.outAnimations.Copy();
            mainMenu.outAnimations = lastOutAnim.Copy();

            UINavigation.Show(data.show);
            UINavigation.Hide(data.hide);
            UINavigation.AddItemToHistory(data);

            mainMenu.outAnimations = temp;
        }

        public void HideHelp()
        {
            SetPlayButtonInteractable(false);

            NavigationPointer mainMenuPointer = new NavigationPointer(screenCategory, mainMenuName);
            NavigationPointer helpPointer = new NavigationPointer(screenCategory, helpName);
            NavigationPointerData data = new NavigationPointerData(true);

            data.show.Add(mainMenuPointer);
            data.hide.Add(helpPointer);

            var temp = mainMenu.inAnimations.Copy();
            mainMenu.inAnimations = firstInAnim.Copy();

            UINavigation.Show(data.show, true);
            UINavigation.Hide(data.hide);
            UINavigation.AddItemToHistory(data);

            help1.Hide(true);
            help2.Hide(true);
            help3.Hide(true);

            mainMenu.inAnimations = temp;
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

            SetPlayButtonInteractable(true);
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

        private void SetPlayButtonInteractable(bool interactable)
        {
            playButton.Interactable = interactable;

            if (interactable)
            {
                playButtonText.color = textColorNormal;
                //playButton.normalLoop = playButtonLoopAnim;
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            }
            else
            {
                //playButton.normalLoop = null;
                EventSystem.current.SetSelectedGameObject(null);
                playButtonText.color = textColorDisabled;
            }
        }

        public void RegularPlayButtonOnClick()
        {
            bool firstTime = PlayerPrefsX.GetBool("first-time", true);
            if (firstTime == false)
            {
                LoadScene();
                return;
            }

            ShowHelp();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene("Game");
        }
    }
}