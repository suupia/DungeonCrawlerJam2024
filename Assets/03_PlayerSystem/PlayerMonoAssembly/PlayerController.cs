#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  DungeonCrawler.PlayerMonoAssembly
{
    [RequireComponent(typeof(PlayerInput))]
    internal class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _gridSize = 10;
        [SerializeField] private float _movementDuration = 0.1f;
        [SerializeField] private float _turnDuration = 0.1f;
        [SerializeField] private bool _instantTransition = false;

        [Header("References")]
        [SerializeField] private PlayerInput _playerInput;

        private Queue<MovementAction> _movements;
        [SerializeField] private List<MovementAction> _movementView;
        private MovementAction _currentMovement;

        private bool _inMotion = false;

        private InputAction _moveAction;
        private InputAction _turnAction;

        // private void TurnStep(Quaternion startRotation, Quaternion endRotation, float progress)
        // {
        //     Quaternion rotation = Quaternion.Lerp(startRotation, endRotation, progress);
        //     transform.rotation = rotation;
        // }
        //
        // private void TurnComplete()
        // {
        //     _currentMovement = MovementAction.None;
        //     _inMotion = false;
        // }
        //
        // private void MovementStep(Vector3 startPos, Vector3 endPos, float progress)
        // {
        //     Vector3 movement = Vector3.Lerp(startPos, endPos, progress);
        //     transform.position = movement;
        // }
        //
        // private void MovementComplete()
        // {
        //     if (
        //         _moveAction.IsPressed() && 
        //         !_moveAction.WasReleasedThisFrame() &&
        //         (
        //             _movements.Count == 0 ||
        //             _movements.Peek() != _currentMovement
        //         )
        //     )
        //     {
        //         Vector2 move = _moveAction.ReadValue<Vector2>();
        //
        //         if (_currentMovement == MovementAction.Right && move.x > 0) _movements.Enqueue(MovementAction.Right);
        //         else if (_currentMovement == MovementAction.Left && move.x < 0) _movements.Enqueue(MovementAction.Left);
        //         else if (_currentMovement == MovementAction.Up && move.y > 0) _movements.Enqueue(MovementAction.Up);
        //         else if (_currentMovement == MovementAction.Down && move.y < 0) _movements.Enqueue(MovementAction.Down);
        //     }
        //
        //     _currentMovement = MovementAction.None;
        //     _inMotion = false;
        // }
        //
        // private Vector3 GetNextPosition(MovementAction action)
        // {
        //     return action switch
        //     {
        //         MovementAction.Left => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(0, 0, _gridSize),
        //         MovementAction.Right => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(0, 0, -_gridSize),
        //         MovementAction.Up => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(_gridSize, 0, 0),
        //         MovementAction.Down => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(-_gridSize, 0, 0),
        //         _ => transform.position,
        //     };
        // }
        //
        // private Quaternion GetNextRotation(MovementAction action)
        // {
        //     return transform.rotation * (action switch
        //     {
        //         MovementAction.TurnRight => Quaternion.Euler(0, 90, 0),
        //         MovementAction.TurnLeft => Quaternion.Euler(0, -90, 0),
        //         _ => Quaternion.Euler(0, 0, 0),
        //     });
        // }
        //
        // private void Move(MovementAction action)
        // {
        //     Vector3 newPosition = GetNextPosition(action);
        //     Quaternion newRotation = GetNextRotation(action);
        //
        //     if (_instantTransition)
        //     {
        //         transform.position = newPosition;
        //         transform.rotation = newRotation;
        //         _currentMovement = MovementAction.None;
        //     }
        //     else if (
        //         action == MovementAction.Right ||
        //         action == MovementAction.Left ||
        //         action == MovementAction.Up ||
        //         action == MovementAction.Down
        //     )
        //     {
        //         _inMotion = true;
        //         GameManager.GetMonoSystem<IAnimationMonoSystem>().RequestAnimation(
        //             this,
        //             _movementDuration,
        //             (float progress) => MovementStep(transform.position, newPosition, progress),
        //             () => MovementComplete()
        //         );
        //     }
        //     else if (
        //         action == MovementAction.TurnRight ||
        //         action == MovementAction.TurnLeft
        //     )
        //     {
        //         _inMotion = true;
        //         GameManager.GetMonoSystem<IAnimationMonoSystem>().RequestAnimation(
        //             this,
        //             _turnDuration,
        //             (float progress) => TurnStep(transform.rotation, newRotation, progress),
        //             () => TurnComplete()
        //         );
        //     }
        // }
        //
        // private void ProcessMovement()
        // {
        //     if (_movements.Count > 0 && !_inMotion)
        //     {
        //         _currentMovement = _movements.Dequeue();
        //         Move(_currentMovement);
        //     }
        // }
        //
        // private void Awake()
        // {
        //     if (_playerInput == null) _playerInput = GetComponent<PlayerInput>();
        //
        //     _movements = new Queue<MovementAction>();
        //     _currentMovement = MovementAction.None;
        //
        //     _moveAction = _playerInput.actions["Movement"];
        //     _turnAction = _playerInput.actions["Turn"];
        //
        //     _moveAction.performed += e =>
        //     {
        //         Vector2 movement = e.ReadValue<Vector2>();
        //
        //         if (movement.x > 0) _movements.Enqueue(MovementAction.Right);
        //         else if (movement.x < 0) _movements.Enqueue(MovementAction.Left);
        //         else if (movement.y > 0) _movements.Enqueue(MovementAction.Up);
        //         else if (movement.y < 0) _movements.Enqueue(MovementAction.Down);
        //     };
        //
        //     _turnAction.performed += e =>
        //     {
        //         float turn = e.ReadValue<float>();
        //         if (turn == 1) _movements.Enqueue(MovementAction.TurnRight);
        //         else if (turn == -1) _movements.Enqueue(MovementAction.TurnLeft);
        //     };
        // }
        //
        // private void FixedUpdate()
        // {
        //     _movementView = _movements.ToList();
        //     ProcessMovement();
        // }
    }
}
