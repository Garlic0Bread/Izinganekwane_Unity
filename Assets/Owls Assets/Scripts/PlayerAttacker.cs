using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorManager animatorManager;

        private void Awake()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
        }

        public void HandleLightMelee_1(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.Attack1_Stab, true);
        }

        public void HandleLightMelee_2(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.Attack2_Slash, true);
        }


        public void HandleHeavyMelee_1(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.Attack3_HeavySlash, true);
        }
    }
}


