using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu (menuName = "Iems/Weapons Item" )]
    public class WeaponItem : Item
    {
        public GameObject weaponPrefab;
        public bool IsUnarmed;
    }
}

