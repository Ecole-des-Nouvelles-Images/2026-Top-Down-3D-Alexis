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

        public void Play()
        {
            SceneManager.LoadScene("AlexisVeVer/Scenes/SceneProd");
            Time.timeScale = 1;
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
    }
}