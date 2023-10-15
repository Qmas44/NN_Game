using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum UpgradeType
{
    ItemUpgrade,
    CharacterUpgrade,
    WeaponUpgrade,
    ItemUnlock,
    WeaponUnlock,
    AbilityUnlock,
    JutsuUnlock
}

public enum CharacterUpgradeType
{
    Default,
    Health,
    Speed,
    Damage,
    Ability
}

namespace MoreMountains.TopDownEngine
{

    [CreateAssetMenu]
    public class UpgradeData : ScriptableObject
    {
        public UpgradeType upgradeType;
        public CharacterUpgradeType characterUpgradeType;
        public string upgradeName;
        public string upgradeDescription;
        public Sprite upgradeSprite;
        public CharacterAbility ability;
        public Weapon weapon;
    }
}
