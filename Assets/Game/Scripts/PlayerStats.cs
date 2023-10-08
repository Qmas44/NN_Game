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

        [Tooltip("The required experience points of the player to level.")]
        [SerializeField] private int requiredExperiencePoints = 100;

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
            playerLevel += 1;
            experiencePoints -= requiredExperiencePoints;
            levelProgressUI.UpdateLevelText(playerLevel);
            levelProgressUI.ResetProgress();
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