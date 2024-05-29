using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponSlotHolder RightHandSlot;
        WeaponSlotHolder LeftHandSlot;

        private void Awake()
        {
            WeaponSlotHolder[] weaponSlotHolders = GetComponentsInChildren<WeaponSlotHolder>();
            foreach(WeaponSlotHolder weaponSlot in weaponSlotHolders)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    LeftHandSlot = weaponSlot;
                }
                else if(weaponSlot.isRightHandSlot)
                {
                    RightHandSlot = weaponSlot;
                }
            }
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                LeftHandSlot.LoadWeaponModel(weaponItem);
            }
            else
            {
                RightHandSlot.LoadWeaponModel(weaponItem);
            }
        }
    }
}

