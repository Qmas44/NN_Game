using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
	public enum InzouSign
	{
		Default,
		Ten,
		Chi,
		Jin
	}
    /// <summary>
    /// Add this ability to a 3D character and it'll be able to activate inzou signs
	///
	/// Animation parameters :
	/// Dashing : true if the character is currently dashing
	/// DashStarted : true when the dash starts
	/// DashingDirectionX : the x component of the dash direction, normalized
	/// DashingDirectionY : the y component of the dash direction, normalized
	/// DashingDirectionZ : the z component of the dash direction, normalized
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Inzou")]
    public class CharacterInzou : CharacterAbility
    {

		private const int MAX_SEQUENCE_LENGTH = 3;
		private InzouSign[] currentInzouSequence = new InzouSign[MAX_SEQUENCE_LENGTH];
		private int sequenceIndex = 0;

		[SerializeField] private float InzouSequenceTimeOutDuration = 5f;

        [Header("Cooldown")]
		/// this ability's cooldown
		[Tooltip("this ability's cooldown")]
		public MMCooldown Cooldown;

        [Header("Feedbacks")]
		/// the feedbacks to play when activating inzou signs
		[Tooltip("the feedbacks to play when activating inzou signs")]
		public MMFeedbacks TenFeedback;
		public MMFeedbacks ChiFeedback;
		public MMFeedbacks JinFeedback;

        /// <summary>
		/// On init we initialize our cooldown and feedback
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();
			Cooldown.Initialization();
			TenFeedback?.Initialization(this.gameObject);
			ChiFeedback?.Initialization(this.gameObject);
			JinFeedback?.Initialization(this.gameObject);
			
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
			if (_inputManager.TenButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete()) // add conditional to check if sequence is comeplete here
			{
				Debug.Log("Ten button pressed");
				TenStart();
			}
			if (_inputManager.ChiButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete())
			{
				Debug.Log("Chi button pressed");
				ChiStart();
			}
			if (_inputManager.JinButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete())
			{
				Debug.Log("Jin button pressed");
				JinStart();
			}
		}

        /// <summary>
		/// Starts Ten
		/// </summary>
		private void TenStart()
		{
			if (!Cooldown.Ready())
			{
				return;
			}
			Cooldown.Start();

			AddInzouSignToSequence(InzouSign.Ten);

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			TenFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

			CheckInzouSequenceComplete();

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		/// <summary>
		/// Starts Chi
		/// </summary>
		private void ChiStart()
		{
			if (!Cooldown.Ready())
			{
				return;
			}
			Cooldown.Start();

			AddInzouSignToSequence(InzouSign.Chi);

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			ChiFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

			CheckInzouSequenceComplete();

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		/// <summary>
		/// Starts Jin
		/// </summary>
		private void JinStart()
		{
			if (!Cooldown.Ready())
			{
				return;
			}
			Cooldown.Start();

			AddInzouSignToSequence(InzouSign.Jin);

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			JinFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

			CheckInzouSequenceComplete();

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		public void AddInzouSignToSequence(InzouSign sign)
		{
			if (sequenceIndex < MAX_SEQUENCE_LENGTH)
			{
				currentInzouSequence[sequenceIndex] = sign;
				sequenceIndex++;
			}
			else
			{
				Debug.Log("Inzou sequence is full");
			}
			// To do: add inzou sign to sequence


		}

		public InzouSign[] GetCurrentInzouSequence()
		{
			// To do: return current inzou sequence
			return currentInzouSequence;
		}

		public void CheckInzouSequenceComplete()
		{
			if(InzouSequenceComplete())
			{
				Debug.Log("Inzou sequence complete");
				// StartCoroutine(InzouSequenceTimeOut());  // Need better way to timeout inzou sequence
			}
		}

		public bool InzouSequenceComplete()
		{
			// To do: check if inzou sequence is complete
			if (sequenceIndex == MAX_SEQUENCE_LENGTH)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void ResetInzouSequence()
		{
			// To do: clear inzou sequence
			sequenceIndex = 0;
			currentInzouSequence = new InzouSign[MAX_SEQUENCE_LENGTH];

			Debug.LogWarning("Inzou sequence reset");
			Debug.LogWarning("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		public bool FireballJutsuSequence()
		{
			// To do: check if inzou sequence is complete
			if (sequenceIndex == MAX_SEQUENCE_LENGTH)
			{
				if (currentInzouSequence[0] == InzouSign.Ten && currentInzouSequence[1] == InzouSign.Ten && currentInzouSequence[2] == InzouSign.Ten)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{ 
				return false;
			}
		}

        // Update is called once per frame
        void Update()
        {

        }


		/// <summary>
		/// Starts the inzou sequence timeout
		/// </summary>
		IEnumerator InzouSequenceTimeOut()
		{
			yield return new WaitForSeconds(InzouSequenceTimeOutDuration);
			ResetInzouSequence();
			Debug.Log("Inzou sequence timed out");
		}
    }
}
