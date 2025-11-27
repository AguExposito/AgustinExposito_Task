using Code.Canvas;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code.Interactables
{
    public class InteractionManager : MonoBehaviour
    {
        private BaseInteractable CurrentInteractableTarget;
        private InteractionType CurrentInteractionType;
        public InteractableUI InteractableUI;
        public bool IsInInteraction;
        public PlayerInput PlayerInput;
        public CameraToolUI CameraToolUI;

        private void Start()
        {
            PlayerInput.actions["Interact"].performed += HandleInteractionInput;
        }

        public void OnDestroy()
        {
            if(PlayerInput != null)
                PlayerInput.actions["Interact"].performed -= HandleInteractionInput;
        }

       

        public bool CanInteract()
        {
            if (CameraToolUI == null || CameraToolUI.RootNode == null)
            {
                return true; // If camera tool UI doesn't exist, allow interaction
            }
            return !CameraToolUI.RootNode.activeSelf;
        }

        private void HandleInteractionInput(InputAction.CallbackContext obj)
        {
            if (!CanInteract())
            {
                return;
            }

            if (CurrentInteractableTarget != null)
            {
                IsInInteraction = true;
                CurrentInteractableTarget.HandleInteraction();
            }
        }

        // this gets called every frame to see whats in front of us to interact with and sets the target 
        public void SetCurrentInteractableTarget(BaseInteractable interactable)
        {
            if (!CanInteract())
            {
                if (InteractableUI != null)
                {
                    InteractableUI.DisableUI();
                }
                return;
            }

            if (interactable == null || !interactable.IsInteractable)
            {
                SetEmptyInteractableTarget();
                return;
            }

            // disable the old interactable outlines
            if (CurrentInteractableTarget != null && interactable != CurrentInteractableTarget)
            {
                CurrentInteractableTarget.DisableAllOutlines();
            }

            CurrentInteractableTarget = interactable;
            CurrentInteractableTarget.EnableAllOutlines();
            CurrentInteractionType = InteractionType.TOUCH;

            if (InteractableUI != null)
            {
                InteractableUI.SetImageAndText(
                    interactable.TextToDisplay,
                    interactable.IconToDisplay
                );
                InteractableUI.EnableUI();
            }
        }

        public void EndInteraction()
        {
            IsInInteraction = false;
            SetEmptyInteractableTarget();
        }

        public void SetEmptyInteractableTarget()
        {
            if (CurrentInteractableTarget != null)
            {
                CurrentInteractableTarget.DisableAllOutlines();
            }

            if (InteractableUI != null)
            {
                InteractableUI.DisableUI();
            }
            CurrentInteractableTarget = null;
        }
    }
}