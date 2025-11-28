using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Inventory.UI
{
    public class InventoryUIToggler : MonoBehaviour
    {
        public GameObject InventoryUI;
        public PlayerInput PlayerInput;
        public InventoryManager InventoryManager;
        public MonoBehaviour MovementController;

        private void Start()
        {
            if (PlayerInput == null)
                PlayerInput = FindObjectOfType<PlayerInput>();

            PlayerInput.actions["OpenInventory"].performed += OnTogglePerformed;
            InventoryUI.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (PlayerInput != null)
                PlayerInput.actions["OpenInventory"].performed -= OnTogglePerformed;
        }

        private void OnTogglePerformed(InputAction.CallbackContext ctx)
        {
            bool isActive = !InventoryUI.gameObject.activeSelf;
            InventoryUI.gameObject.SetActive(isActive);

            if (MovementController != null)
                MovementController.enabled = !isActive;

            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;

            if (!isActive)
            {
                // save on close
                InventoryManager.SaveInventory();
            }
        }
    }
}
