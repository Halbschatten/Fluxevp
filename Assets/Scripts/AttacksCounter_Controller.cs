using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    static class AttacksCounter_Controller
    {
        [Tooltip("Global Attack 1 (Punch) Counter")]
        private static int AttackCounter1 = 0;
        [Tooltip("Global Attack 2 (Punch) Counter")]
        private static int AttackCounter2 = 0;


        public static void INCREASEATTACK1()
        {
            AttackCounter1++;
        }

        public static void INCREASEATTACK2() 
        {         
            AttackCounter2++;
        }

        public static int GetATTACK1Counter()
        {
            return AttackCounter1;
        }

        public static int GetATTACK2Counter()
        {
            return AttackCounter2;
        }


    }

}
