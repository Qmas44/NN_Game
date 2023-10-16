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

public enum UpgradeLevel
{
    Default,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
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
        public UpgradeLevel upgradeLevel;
        public List<UpgradeData> nextUpgrades; // the next upgrade in the same category
        public string upgradeName;
        public string upgradeDescription;
        public Sprite upgradeSprite;
        public CharacterAbility ability;
        public Weapon weapon;
        public float weaponDamageCaused;
        public int weaponHealth;
        public float weaponSpeed;
    }
}
