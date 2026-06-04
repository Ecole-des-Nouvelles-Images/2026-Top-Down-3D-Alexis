using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SetFirstButton : MonoBehaviour
    {
        [FormerlySerializedAs("_eventSystem")] [SerializeField] private EventSystem eventSystem;
        [FormerlySerializedAs("_buttonToSelect")] [SerializeField] private Selectable buttonToSelect;
    
        private GameObject _lastSelected;

        public void OnEnable()
        {
            SelectButton();
            _lastSelected = buttonToSelect.gameObject;
        }

        private void Update()
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                _lastSelected = eventSystem.currentSelectedGameObject;
            }

            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(_lastSelected);
            }
        }
        private void SelectButton()
        {
            if (buttonToSelect == null) return;
            eventSystem.SetSelectedGameObject(buttonToSelect.gameObject);
        }
    }
}