#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawler.PlayerAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VContainer;

namespace  DungeonCrawler.PlayerMonoAssembly
{
    [RequireComponent(typeof(PlayerInput))]
    internal class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] float gridSize = 10;
        [SerializeField] float movementDuration = 0.1f;
        [SerializeField] float turnDuration = 0.1f;
        [SerializeField] bool instantTransition = false;
        
        [Header("References")]
        [SerializeField] PlayerInput playerInput;

        readonly Queue<MovementAction> _movements = new();
        [SerializeField] List<MovementAction> movementView;
        MovementAction _currentMovement;

        bool _inMotion;

        InputAction _moveAction;
        InputAction _turnAction;
        
        AnimationMonoSystem  _animationMonoSystem;

        DungeonSwitcher _dungeonSwitcher;

        [Inject]
        public void Construct(
            DungeonSwitcher dungeonSwitcher
        )
        {
            _dungeonSwitcher = dungeonSwitcher;
        }
        

        
        void TurnStep(Quaternion startRotation, Quaternion endRotation, float progress)
        {
            Quaternion rotation = Quaternion.Lerp(startRotation, endRotation, progress);
            transform.rotation = rotation;
        }
        
        void TurnComplete()
        {
            _currentMovement = MovementAction.None;
            _inMotion = false;
        }
        
        void MovementStep(Vector3 startPos, Vector3 endPos, float progress)
        {
            Vector3 movement = Vector3.Lerp(startPos, endPos, progress);
            transform.position = movement;
        }
        
        void MovementComplete()
        {
            Assert.IsNotNull(_moveAction);
            Assert.IsNotNull(_movements);
            if (
                _moveAction.IsPressed() && 
                !_moveAction.WasReleasedThisFrame() &&
                (
                    _movements.Count == 0 ||
                    _movements.Peek() != _currentMovement
                )
            )
            {
                Vector2 move = _moveAction.ReadValue<Vector2>();
        
                if (_currentMovement == MovementAction.Right && move.x > 0) _movements.Enqueue(MovementAction.Right);
                else if (_currentMovement == MovementAction.Left && move.x < 0) _movements.Enqueue(MovementAction.Left);
                else if (_currentMovement == MovementAction.Up && move.y > 0) _movements.Enqueue(MovementAction.Up);
                else if (_currentMovement == MovementAction.Down && move.y < 0) _movements.Enqueue(MovementAction.Down);
            }
        
            _currentMovement = MovementAction.None;
            _inMotion = false;
        }
        
        Vector3 GetNextPosition(MovementAction action)
        {
            return action switch
            {
                MovementAction.Left => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(0, 0, gridSize),
                MovementAction.Right => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(0, 0, -gridSize),
                MovementAction.Up => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(gridSize, 0, 0),
                MovementAction.Down => transform.position + Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(-gridSize, 0, 0),
                _ => transform.position,
            };
        }
        
        Quaternion GetNextRotation(MovementAction action)
        {
            return transform.rotation * (action switch
            {
                MovementAction.TurnRight => Quaternion.Euler(0, 90, 0),
                MovementAction.TurnLeft => Quaternion.Euler(0, -90, 0),
                _ => Quaternion.Euler(0, 0, 0),
            });
        }
        
        void Move(MovementAction action)
        {
            Vector3 newPosition = GetNextPosition(action);
            Quaternion newRotation = GetNextRotation(action);

            Vector2Int newGridPosition = GridConverter.WorldPositionToGridPosition(newPosition);
            if (_dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<CharacterWall>(newGridPosition) != null)
            {
                // Debug.Log("Move is cancelled because new position is wall");
                return;
            }
            else if (_dungeonSwitcher.CurrentDungeon.Map.GetSingleEntity<CharacterStairs>(newGridPosition) != null)
            {
                Debug.Log("Player is on stairs");
            }
            
            if (instantTransition)
            {
                transform.position = newPosition;
                transform.rotation = newRotation;
                _currentMovement = MovementAction.None;
            }
            else if (
                action == MovementAction.Right ||
                action == MovementAction.Left ||
                action == MovementAction.Up ||
                action == MovementAction.Down
            )
            {
                _inMotion = true;
                _animationMonoSystem.RequestAnimation(
                    this,
                    movementDuration,
                    (float progress) => MovementStep(transform.position, newPosition, progress),
                    () => MovementComplete()
                );
            }
            else if (
                action == MovementAction.TurnRight ||
                action == MovementAction.TurnLeft
            )
            {
                _inMotion = true;
                _animationMonoSystem.RequestAnimation(
                    this,
                    turnDuration,
                    (float progress) => TurnStep(transform.rotation, newRotation, progress),
                    () => TurnComplete()
                );
            }
        }
        
        void ProcessMovement()
        {
            if (_movements.Count > 0 && !_inMotion)
            {
                _currentMovement = _movements.Dequeue();
                Move(_currentMovement);
            }
        }
        
        void Awake()
        {
            if (playerInput == null) playerInput = GetComponent<PlayerInput>();
            _animationMonoSystem = new AnimationMonoSystem();

            _currentMovement = MovementAction.None;
        
            _moveAction = playerInput.actions["Movement"];
            _turnAction = playerInput.actions["Turn"];
        
            _moveAction.performed += e =>
            {
                Vector2 movement = e.ReadValue<Vector2>();
        
                if (movement.x > 0) _movements.Enqueue(MovementAction.Right);
                else if (movement.x < 0) _movements.Enqueue(MovementAction.Left);
                else if (movement.y > 0) _movements.Enqueue(MovementAction.Up);
                else if (movement.y < 0) _movements.Enqueue(MovementAction.Down);
            };
        
            _turnAction.performed += e =>
            {
                float turn = e.ReadValue<float>();
                if (turn == 1) _movements.Enqueue(MovementAction.TurnRight);
                else if (turn == -1) _movements.Enqueue(MovementAction.TurnLeft);
            };
        }
        
        void FixedUpdate()
        {
            movementView = _movements.ToList();
            ProcessMovement();
            _animationMonoSystem.Update();
        }
    }
}
