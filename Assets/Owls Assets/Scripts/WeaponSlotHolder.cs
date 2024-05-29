using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotHolder : MonoBehaviour
    {
        public Transform parrentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if(currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }
        public void UnloadWeapon_And_Destroy()
        {
            if(currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeapon_And_Destroy();
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.weaponPrefab) as GameObject;
            if(model != null )
            {
                if(parrentOverride != null)
                {
                    model.transform.parent = parrentOverride;
                }

                else
                {
                    model.transform.parent = transform;
                }

                currentWeaponModel = model;
            }
        }
    }
}







