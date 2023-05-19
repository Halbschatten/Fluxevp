using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {


        [Tooltip("Attack 1 (Punch) Counter")]
        public TextMeshProUGUI attack1Counter;
        [Tooltip("Attack 2 (Kick) Counter")]
        public TextMeshProUGUI attack2Counter;

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;


        private void Awake() // Refresh Before starting
        {
            RefeshAttack1Counter();
            RefeshAttack2Counter();
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }

        public void RefeshAttack1Counter() // Refreshes the counter for the Attack 1
        {
            attack1Counter.text = AttacksCounter_Controller.GetATTACK1Counter().ToString();
        }
        public void RefeshAttack2Counter() // Refreshes the counter for the Attack 2
        {
            attack2Counter.text = AttacksCounter_Controller.GetATTACK2Counter().ToString();
        }

    }

}
