using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Menu
{
    public class MenuToggleService : MonoBehaviour
    {
        [Header("Menu Settings")]
        public GameObject MenuGameObject;
        public GameObject MouseLockHelper;
        public PlayerInput PlayerInput;
        
        public bool IsMenuOpen { get; private set; }

        private void Start()
        {
            if (MenuGameObject != null)
            {
                MenuGameObject.SetActive(false);
            }
            if (MouseLockHelper != null)
            {
                MouseLockHelper.SetActive(false);
            }
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            {
                ToggleMenu();
            }
        }

        public void ToggleMenu()
        {
            if (IsMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        public void OpenMenu()
        {
            if (MenuGameObject != null)
            {
                MenuGameObject.SetActive(true);
            }
            if (MouseLockHelper != null)
            {
                MouseLockHelper.SetActive(false);
            }
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            IsMenuOpen = true;
        }

        public void CloseMenu()
        {
            if (MenuGameObject != null)
            {
                MenuGameObject.SetActive(false);
            }
            if (MouseLockHelper != null)
            {
                MouseLockHelper.SetActive(true);
            }
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            IsMenuOpen = false;
        }

        private void OnDestroy()
        {
        }
    }
}

