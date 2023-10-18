using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;


namespace MoreMountains.TopDownEngine
{

    /// <summary>
    /// Add this ability to a 3D character and it'll be able to execute inzou signs
    /// This jutsu is a fireball ninjutsu
	///
	/// Animation parameters :
	/// Dashing : true if the character is currently dashing
	/// DashStarted : true when the dash starts
	/// DashingDirectionX : the x component of the dash direction, normalized
	/// DashingDirectionY : the y component of the dash direction, normalized
	/// DashingDirectionZ : the z component of the dash direction, normalized
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Fireball Ninjutsu")]
    public class CharacterFireballNinjutsu : CharacterHandleWeapon
    {
        public CharacterInzou inzou;
        [Header("Cooldown")]
		/// this ability's cooldown
		[Tooltip("this ability's cooldown")]
		public MMCooldown Cooldown;

		[SerializeField] private float activeDuration = 5f;

        public MMFeedbacks FireballJutsuFeedback;

        /// <summary>
		/// On init we initialize our cooldown and feedback
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();
			Cooldown.Initialization();
            FireballJutsuFeedback?.Initialization(this.gameObject);
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

			// if fireball jutsu sequence isnt complete, dont do anything
            if (_inputManager.NinjutsuButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && inzou.FireballJutsuSequence())
			{
				Debug.Log("Executing Jutsu pressed");
				StartCoroutine(ExecuteNinjutsuCoroutine());
			}
		}

        /// <summary>
		/// Executes ninjutsu
		/// </summary>
		private void ExecuteNinjutsu()
		{
			// Add back if you want cooldown
			// if (!Cooldown.Ready())
			// {
			// 	return;
			// }
			// Cooldown.Start();

			ShootStart();

			FireballJutsuFeedback?.PlayFeedbacks(transform.position);

			PlayAbilityStartFeedbacks();

			inzou.ResetInzouSequence();
		}

		/// <summary>
		/// Starts the ninjutsu coroutine
		/// </summary>
		IEnumerator ExecuteNinjutsuCoroutine()
		{
			inzou.PermitAbility(false);
			ChangeWeapon(Jutsu, Jutsu.WeaponName, false);
			ExecuteNinjutsu();

			yield return new WaitForSeconds(activeDuration);

			inzou.PermitAbility(true);

			ChangeWeapon(InitialWeapon, InitialWeapon.name, false);
			Debug.LogWarning("permitting inzou");

		}

		public virtual void SetActiveDuration(float newDuration)
		{
			activeDuration = newDuration;
		}

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
