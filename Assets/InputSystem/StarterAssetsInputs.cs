using System;
using UnityEngine;
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
        public bool attack1, attack2;
        public bool attacking;

        [Header("Movement Settings")] public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif
        private Gamepad _gamepad;

        private void Awake()
        {
            _gamepad = UnityEngine.InputSystem.Gamepad.current;
        }

        private void Update()
        {

            ///          Legacy Code, This is already threated by the Unity Input Actions
            /*
            if (Input.GetKeyDown(KeyCode.Q) && !attack1)
                attack1 = true;
            if (Input.GetKeyUp(KeyCode.Q) && attack1)
                attack1 = false;
            if (Input.GetKeyDown(KeyCode.E) && !attack2)
                attack2 = true;
            if (Input.GetKeyUp(KeyCode.E) && attack2)
                attack2 = false;*/

            /*
            attack1 = _gamepad.buttonWest.isPressed || attack1;
            attack2 = _gamepad.buttonNorth.isPressed || attack2;*/
        }
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnAttack1(InputValue value)
        {
            Attack1Input(value.isPressed);
        }

        public void OnAttack2(InputValue value)
        {
            Attack2Input(value.isPressed);
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
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

        public void Attack1Input(bool newAttackState)
        {
            attack1 = true;
        }

        public void Attack2Input(bool newAttackState)
        {
            attack2 = true;
        }

        public void EndAttack()
        {
            attacking = false;
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