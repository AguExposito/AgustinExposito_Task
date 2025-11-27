using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Interactables
{
    public class InteractableDetector : MonoBehaviour
    {
        public Dictionary<Transform, BaseInteractable> SceneInteractables = new();
        public Transform PlayerRoot;
        public Transform PlayerFeet;
        public Vector3 verticalSquishInteractableDistance = new(1, .5f, 1);
        [Tooltip("Enable to log information about interactable detection in the console")] public bool DebugLogs;
        [Tooltip("Draw gizmos for interactable detection when the object is selected")] public bool DrawGizmos = true;
        private InteractionManager InteractionManager;

        public void Start()
        {
            InteractionManager = GameObject.FindWithTag("GameMain").GetComponent<InteractionManager>();
        }

        private void Update()
        {
            if (DebugLogs && Input.GetKeyDown(KeyCode.Alpha9))
            {
                DebugPrintInteractables();
            }
            //todo: also we prob dont need this EVERY frame, maybe just every 3 frames, and only when we can interact.
            if (true)
            {
                var currentMinAngle = 180f;
                BaseInteractable currentBestInteractable = null;
                //for every interactable in the dictionary see if we are close to it, and if so see if its in front of us 
                foreach (var kvp in SceneInteractables)
                {
                    var interactableTransform = kvp.Key;
                    var interactable = kvp.Value;

                    //if not active skip
                    if (!interactable.GetInteractableAtThisTime())
                    {
                        // Debug.Log($"{interactableTransform.name} is not interactable");
                        continue;
                    }

                    var squishedDistance = Vector3.Scale(
                        interactableTransform.position -
                        PlayerFeet.position - PlayerRoot.forward * .5f,
                        verticalSquishInteractableDistance
                    );

                    //checkt distance - if we have set InteractDistance, then use that, else use default Max distance
                    if (interactable.InteractDistance == 0
                        ? Vector3.Magnitude(squishedDistance) < 1.25f
                        : Vector3.Magnitude(squishedDistance) < interactable.InteractDistance)
                    {
                        var angleToInteractable = Vector3.Angle(
                            PlayerRoot.forward,
                            Vector3.Normalize(
                                new Vector3(
                                    interactableTransform.position.x - PlayerFeet.position.x,
                                    0,
                                    interactableTransform.position.z - PlayerFeet.position.z
                                )
                            )
                        );

                        //check angle
                        if (angleToInteractable < 40)
                        {
                            if (currentBestInteractable == null || angleToInteractable < currentMinAngle)
                            {
                                currentBestInteractable = interactable;
                                currentMinAngle = angleToInteractable;
                            }
                        }
                    }
                }

                InteractionManager.SetCurrentInteractableTarget(currentBestInteractable);
            }
        }

        public void RegisterInteractableObject(BaseInteractable interactable, Transform transform)
        {
            SceneInteractables[transform] = interactable;
        }

        public void UnregisterInteractableObject(Transform transform)
        {
                SceneInteractables.Remove(transform);
        }

        [ContextMenu("Debug Print Interactables")]
        public void DebugPrintInteractables()
        {
            Debug.Log($"[InteractableDetector] Registered interactables: {SceneInteractables.Count}");
            foreach (var kvp in SceneInteractables)
            {
                Debug.Log($" - {kvp.Value.name} (active: {kvp.Value.IsInteractable})");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!DrawGizmos || PlayerFeet == null) return;
            Gizmos.color = Color.yellow;
            // visualize detection cone and distance
            Gizmos.DrawLine(PlayerFeet.position, PlayerFeet.position + PlayerRoot.forward * 1.25f);
            foreach (var kvp in SceneInteractables)
            {
                if(kvp.Key != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(kvp.Key.position, 0.1f);
                    Gizmos.DrawLine(PlayerFeet.position, kvp.Key.position);
                }
            }
        }
    }
}