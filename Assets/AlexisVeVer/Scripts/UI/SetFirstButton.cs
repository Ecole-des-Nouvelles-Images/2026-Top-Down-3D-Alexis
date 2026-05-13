using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.UI
{
    public class SetFirstButton : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Selectable _buttonToSelect;
    
        private GameObject _lastSelected;

        public void OnEnable()
        {
            SelectButton();
            _lastSelected = _buttonToSelect.gameObject;
        }

        private void Update()
        {
            if (_eventSystem.currentSelectedGameObject != null)
            {
                _lastSelected = _eventSystem.currentSelectedGameObject;
            }

            if (_eventSystem.currentSelectedGameObject == null)
            {
                _eventSystem.SetSelectedGameObject(_lastSelected);
            }
        }
        private void SelectButton()
        {
            if (_buttonToSelect == null) return;
            _eventSystem.SetSelectedGameObject(_buttonToSelect.gameObject);
        }
    }
}
