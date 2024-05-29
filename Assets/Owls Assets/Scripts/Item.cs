using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")]

        public Sprite itemIcon;
        public string itemName;

        [Header("One Handed Attack Animations")]
        public string Attack1_Stab;
        public string Attack2_Slash;
        public string Attack3_HeavySlash;
    }
}

