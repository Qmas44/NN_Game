using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    public class PlayerStats : MonoBehaviour, MMEventListener<TopDownEngineExperiencePointEvent>
    {
        [Tooltip("The maximum health points of the player.")]
        [SerializeField] private int maxHealth = 100;

        [Tooltip("The current health points of the player.")]
        [SerializeField] private int currentHealth = 100;

        [Tooltip("The damage points the player deals.")]
        [SerializeField] private int damagePoints = 10;

        [Tooltip("The experience points of the player.")]
        [SerializeField] private int experiencePoints = 0;

        [Tooltip("The level of the player.")]
        [SerializeField] private int playerLevel = 1;

        [SerializeField] private LevelProgressUI levelProgressUI;

        /// <summary>
		/// Catches TopDownEngineExperiencePointsEvents and acts on them, playing the corresponding sounds
		/// </summary>
		/// <param name="pointEvent">TopDownEngineExperiencePointEvent event.</param>
		public virtual void OnMMEvent(TopDownEngineExperiencePointEvent experiencePointEvent)
		{
            experiencePoints += experiencePointEvent.ExperiencePoints;
            levelProgressUI.UpdateProgressBar(experiencePoints);
            CheckLevelUp(experiencePointEvent.ExperiencePoints);
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


        private void CheckLevelUp( int experiencePoints)
        {
            int requiredExperience = playerLevel * 100;
            if (experiencePoints >= requiredExperience)
            {
                playerLevel++;
                experiencePoints -= requiredExperience;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            levelProgressUI.UpdateLevelText(playerLevel);
            levelProgressUI.ResetProgress();
            maxHealth += 10;
            currentHealth = maxHealth;
            damagePoints += 5;
        }
    }
}