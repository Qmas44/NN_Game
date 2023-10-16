using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// Add this ability to a 3D character and it'll be able activate yin buff
	///
	/// Animation parameters :
	/// Dashing : true if the character is currently dashing
	/// DashStarted : true when the dash starts
	/// DashingDirectionX : the x component of the dash direction, normalized
	/// DashingDirectionY : the y component of the dash direction, normalized
	/// DashingDirectionZ : the z component of the dash direction, normalized
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Yin")]
    public class CharacterYin : CharacterAbility
    {

        [Header("Cooldown")]
		/// this ability's cooldown
		[Tooltip("this ability's cooldown")]
		public MMCooldown Cooldown;

        [Header("Feedbacks")]
		/// the feedbacks to play when dashing
		[Tooltip("the feedbacks to play when activating yin")]
		public MMFeedbacks YinFeedback;

        /// <summary>
		/// On init we initialize our cooldown and feedback
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();
			Cooldown.Initialization();
			YinFeedback?.Initialization(this.gameObject);
		}

        /// <summary>
		/// Watches for input and starts a dash if needed
		/// </summary>
		protected override void HandleInput()
		{
			base.HandleInput();
			if (!AbilityAuthorized
			    || (!Cooldown.Ready())
			    || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
			{
				return;
			}
			if (_inputManager.DashButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				DashStart();
			}
		}

        /// <summary>
		/// Starts a dash
		/// </summary>
		public virtual void DashStart()
		{
			if (!Cooldown.Ready())
			{
				return;
			}
			Cooldown.Start();

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			_controller.FreeMovement = false;
			_controller3D.DetachFromMovingPlatform();
			YinFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();
		}

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
