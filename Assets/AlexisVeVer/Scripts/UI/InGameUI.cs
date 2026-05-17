using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlexisVeVer.Scripts.UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private GameObject _mainMenuDoubleCheck;

        public void ResumeGame()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public void OpenOptionsMenu()
        {
            _pauseMenu.SetActive(false);
            _optionsMenu.SetActive(true);
        }

        public void CloseOptionsMenu()
        {
            _optionsMenu.SetActive(false);
            _pauseMenu.SetActive(true);
        }

        public void MainMenuDoubleCheck()
        {
            _mainMenuDoubleCheck.SetActive(true);
            _pauseMenu.SetActive(false);
        }

        public void CloseMainMenuDoubleCheck()
        {
            _mainMenuDoubleCheck.SetActive(false);
            _pauseMenu.SetActive(true);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("AlexisVeVer/Scenes/MainMenu");
        }
    }
}
