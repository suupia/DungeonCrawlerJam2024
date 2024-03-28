#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  DungeonCrawler.PlayerAssembly.Classes
{
    [RequireComponent(typeof(PlayerInput))]
    internal sealed class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _interactionRadius = 0.1f;

        private bool _isInteracting;
        private PlayerInput _playerInput;
        private InputAction _interactAction;

        void StartInteraction(IInteractable interactable)
        {
            interactable.Interact(this, out _isInteracting);
        }

        void EndInteraction()
        {
            _isInteracting = false;
        }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();

            _interactAction = _playerInput.actions["Interact"];
        }

        private void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(_interactionPoint.position, _interactionRadius, _interactionLayer);

            if (_interactAction.WasPressedThisFrame())
            {
                if (colliders.Length == 0) EndInteraction();

                for (int i = 0; i < colliders.Length; i++)
                {
                    IInteractable interactable = colliders[i].GetComponent<IInteractable>();
                    if (interactable != null) StartInteraction(interactable);
                }
            }
        }
    }
}
