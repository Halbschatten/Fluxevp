using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")] public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif
        private Gamepad _gamepad;
        private ThirdPersonController _controller;

        public void SetController(ThirdPersonController _ctrl)
        {
            _controller = _ctrl;
        }

        private void Awake()
        {
            _gamepad = UnityEngine.InputSystem.Gamepad.current;
        }


        private void Update()
        {

            ///          Legacy Code, This is already handled by the Unity Input Actions
            /*
            if (Input.GetKeyDown(KeyCode.Q) && !Q)
                Q = true;
            if (Input.GetKeyUp(KeyCode.Q) && Q)
                Q = false;
            if (Input.GetKeyDown(KeyCode.E) && !E)
                E = true;
            if (Input.GetKeyUp(KeyCode.E) && E)
                E = false;*/

            /*
            Q = _gamepad.buttonWest.isPressed || Q;
            E = _gamepad.buttonNorth.isPressed || E;*/
        }
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputAction.CallbackContext value)
        {
            MoveInput(value.ReadValue<Vector2>());
        }

        public void OnAttack1(InputAction.CallbackContext value)
        {
            if (value.performed)        // Sanitizes the trigger for only the frame while the key is pressed
            {
                _controller.Attack1();
            }
        }

        public void OnAttack2(InputAction.CallbackContext value)
        {
            if (value.performed)        // Sanitizes the trigger for only the frame while the key is pressed
            {
                _controller.Attack2();
            }
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext value)
        {
            if (value.performed)        // Sanitizes the trigger for only the frame while the key is pressed
            {
                _controller.JumpAction();
            }
        }

        public void OnSprint(InputAction.CallbackContext value)
        {
            SprintInput(value.performed);
        }

#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void EndAttack()
        {
            _controller.SetAttacking(false);
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif
    }
}