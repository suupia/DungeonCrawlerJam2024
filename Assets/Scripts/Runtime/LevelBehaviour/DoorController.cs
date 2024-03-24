using DungeonCrawler.Core;
using DungeonCrawler.Runtime.Interactable;
using DungeonCrawler.Runtime.MonoSystems.Animation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonCrawler.Runtime.LevelBehaviour
{
    internal sealed class DoorController : MonoBehaviour, IInteractable
    {
        [Header("Parameters")]
        private float _rotationDuration = 1.0f;
        [SerializeField][Range(-1, 1)] private float _forwardDirection = 0;

        private Vector3 _forward;
        private Vector3 _startRotation;

        bool _isOpen = false;
        bool _isChanging = false;

        private void OpenStep(float dir, Quaternion start, float progress, Quaternion? endRot = null)
        {
            Quaternion end;
            if (dir >= _forwardDirection) end = endRot ?? start * Quaternion.Euler(0, -90, 0);
            else end = endRot ?? start * Quaternion.Euler(0, 90, 0);

            transform.parent.rotation = Quaternion.Slerp(start, end, progress);
        }

        private void OpenDoor(Vector3 playerPos)
        {
            if (!_isChanging)
            {
                _isChanging = true;
                Quaternion StartRot = transform.parent.rotation;
                float dir = Vector3.Dot(_forward, (playerPos - transform.position).normalized);
                GameManager.GetMonoSystem<IAnimationMonoSystem>().RequestAnimation(
                    this,
                    _rotationDuration,
                    (float progress) => OpenStep(dir, StartRot, progress),
                    () => {
                        _isChanging = false;
                        _isOpen = true;
                    }
                );
            }
        }

        private void CloseDoor()
        {
            if (!_isChanging)
            {
                _isChanging = true;
                Quaternion StartRot = transform.rotation;
                GameManager.GetMonoSystem<IAnimationMonoSystem>().RequestAnimation(
                    this,
                    _rotationDuration,
                    (float progress) => OpenStep(0, StartRot, progress, Quaternion.Euler(_startRotation)),
                    () => {
                        _isChanging = false;
                        _isOpen = false;
                    }
                );
            }
        }

        public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void EndInteraction()
        {

        }

        public void Interact(Interactor interactor, out bool successful)
        {
            Debug.Log("Here!");
            if (_isOpen) CloseDoor();
            else OpenDoor(interactor.transform.position);
            successful = true;
        }

        private void Awake()
        {
            _startRotation = transform.parent.rotation.eulerAngles;
            _forward = -transform.parent.right;
        }
    }
}
