using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Unity.VisualScripting;

namespace MoreMountains.TopDownEngine
{
    public class LevelUpManager : MMMonoBehaviour, MMEventListener<TopDownEngineExperiencePointEvent>
    {
        [Tooltip("The experience points of the player.")]
        [SerializeField] private int experiencePoints = 0;

        [Tooltip("The required experience points of the player to level.")]
        [SerializeField] private int requiredExperiencePoints = 100;

        [Tooltip("The level of the player.")]
        [SerializeField] private int playerLevel = 1;

        [SerializeField] private LevelProgressUI levelProgressUI;

        /// the feedback to play when the player levels up
        [Tooltip("the panel to open when player levels up")]
        [SerializeField] private UpgradeManager upgradePanel;


        [Header("Managers")]
        [SerializeField] private PassiveItems passiveItems;
        [SerializeField] private AbilityManager abilityManager;
        [SerializeField] private CharacterUpgradeManager characterUpgradeManager;
        [SerializeField] private WeaponUpgradeManager weaponUpgradeManager;



        [Header("Feedbacks")]
        /// the feedback to play when the player levels up
        [Tooltip("the feedback to play when the player levels up")]
        public MMFeedbacks LevelUpFeedback;

        [SerializeField] List<UpgradeData> upgrades;

        List<UpgradeData> selectedUpgrades;

        [SerializeField] List<UpgradeData> aquiredUpgrades;

        [SerializeField] List<UpgradeData> upgradesAvaliableOnStart; // upgrades available for all characters on start

        private void Start()
        {
            LevelUpFeedback?.Initialization(this.gameObject);
            AddUpgradesIntoAvailableUpgrades(upgradesAvaliableOnStart);
        }

        internal void AddUpgradesIntoAvailableUpgrades(List<UpgradeData> upgradesToAdd)
        {
            if (upgrades == null) { return;}
            this.upgrades.AddRange(upgradesToAdd);
        }


        public void Upgrade(int selectedUpgradeID)
        {
            UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

            if (aquiredUpgrades == null) { aquiredUpgrades = new List<UpgradeData>(); }

            switch (upgradeData.upgradeType)
            {
                case UpgradeType.ItemUpgrade:
                    break;
                case UpgradeType.CharacterUpgrade:
                    characterUpgradeManager.Upgrade(upgradeData.characterUpgradeType);

                    AddUpgradesIntoAvailableUpgrades(upgradeData.nextUpgrades);
                    break;
                case UpgradeType.WeaponUpgrade:
                    weaponUpgradeManager.Upgrade(upgradeData);

                    AddUpgradesIntoAvailableUpgrades(upgradeData.nextUpgrades);
                    break;
                case UpgradeType.ItemUnlock:
                    // passiveItems.UnlockItem(upgradeData.item);
                    break;
                case UpgradeType.AbilityUnlock:
                    Debug.Log("Unlocking ability " + upgradeData.ability);
                    abilityManager.UnlockAbility(upgradeData.ability);
                    break;
                case UpgradeType.WeaponUnlock:
                    break;
                case UpgradeType.JutsuUnlock:
                    break;
            }

            aquiredUpgrades.Add(upgradeData);
            upgrades.Remove(upgradeData);

            Debug.Log("Upgrade " + selectedUpgradeID);
        }

        /// <summary>
		/// Catches TopDownEngineExperiencePointsEvents and acts on them, playing the corresponding sounds
		/// </summary>
		/// <param name="pointEvent">TopDownEngineExperiencePointEvent event.</param>
		public virtual void OnMMEvent(TopDownEngineExperiencePointEvent experiencePointEvent)
		{

            experiencePoints += experiencePointEvent.ExperiencePoints;
            levelProgressUI.UpdateProgressBar(experiencePoints, requiredExperiencePoints);
            CheckLevelUp(experiencePoints);
		}

        private void CheckLevelUp( int experiencePoints)
        {
            requiredExperiencePoints = playerLevel * 100;
            if (experiencePoints >= requiredExperiencePoints)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            if (selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
            selectedUpgrades.Clear();
            selectedUpgrades.AddRange(GetUpgrades(3));

            playerLevel += 1;
            experiencePoints -= requiredExperiencePoints;

            // Handle UI 
            levelProgressUI.UpdateLevelText(playerLevel);
            
            Debug.Log("upgrades left: " + upgrades.Count);

            if (upgrades.Count != 0) 
            { 
                upgradePanel.OpenPanel(selectedUpgrades); 
            }

            levelProgressUI.ResetProgress();
        }

        public List<UpgradeData> GetUpgrades(int count)
        {
            List<UpgradeData> upgradeList = new List<UpgradeData>();
            List<UpgradeData> availableUpgrades = new List<UpgradeData>(upgrades);

            if (count > availableUpgrades.Count)
            {
                count = availableUpgrades.Count;
            }

            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, availableUpgrades.Count); // Creates a random index between i and the end of the list
                UpgradeData temp = availableUpgrades[i]; // Assigns the value of the current index to a temp variable
                availableUpgrades[i] = availableUpgrades[randomIndex]; // Assigns the value of the random index to the current index
                availableUpgrades[randomIndex] = temp; // Assigns the value of the temp variable to the random index
            }

            upgradeList.AddRange(availableUpgrades.GetRange(0, count));

            return upgradeList;
        }

                /// <summary>
        /// OnEnable, we start listening to events.
        /// </summary>
        protected virtual void OnEnable()
        {
            this.MMEventStartListening<TopDownEngineExperiencePointEvent> ();
        }

        /// <summary>
        /// OnDisable, we stop listening to events.
        /// </summary>
        protected virtual void OnDisable()
        {
            this.MMEventStopListening<TopDownEngineExperiencePointEvent> ();
        }
    }
}