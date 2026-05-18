using System;
using Baptiste.script;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexisVeVer.Scripts.UI
{
    public class MainSceneUI : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private GameObject _creditsMenu;
        [SerializeField] private GameObject _quitDoubleCheck;
        [SerializeField] private GameObject _transition;

        [SerializeField] private float _timerForTransition = 2;
        [SerializeField] private float _transitionTime;
        [SerializeField] private bool _doTransition;
        
        void Awake()
        {
            Time.timeScale = 1;
            _transition.SetActive(false);
        }
        
        public void Play()
        {
            _doTransition = true;
            _transition.SetActive(true);
        }

        public void OpenOptionsMenu()
        {
            _mainMenu.SetActive(false);
            _optionsMenu.SetActive(true);
        }

        public void CloseOptionsMenu()
        {
            _optionsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void OpenCreditsMenu()
        {
            _creditsMenu.SetActive(true);
            _mainMenu.SetActive(false);
        }

        public void CloseCreditsMenu()
        {
            _creditsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void LeaveGame()
        {
            _quitDoubleCheck.SetActive(true);
        }

        public void CloseLeaveDoubleCheck()
        {
            _quitDoubleCheck.SetActive(false);
        }

        public void LeaveChecked()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (_doTransition)
            {
                _transitionTime += Time.deltaTime;
            }
            
            if (_transitionTime >= _timerForTransition)
            {
                SceneManager.LoadScene("AlexisVeVer/Scenes/SceneProd");
                _transitionTime = 0;
                _doTransition = false;
                Time.timeScale = 1;
            }
        }
    }
}