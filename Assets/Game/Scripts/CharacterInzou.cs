using UnityEngine;
using UnityEngine.UI;
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
		[SerializeField] private bool isTimeoutRunning = false;
		[SerializeField] private float InzouSequenceTimeOutDuration = 3f;

		[Header("Inzou Sprites")]
		[SerializeField] private Sprite TenSprite;
		[SerializeField] private Sprite ChiSprite;
		[SerializeField] private Sprite JinSprite;

        [Header("Cooldown")]
		/// this ability's cooldown
		[Tooltip("this ability's cooldown")]
		public MMCooldown Cooldown;

		[Header("InzouUI")]
		/// this is the inzou UI
		[Tooltip("this is the inzou UI")]
		private GameObject InzouBar;

		[Header("InzouUI")]
		/// this is the inzou UI
		[Tooltip("this is the inzou UI")]
		[SerializeField] private GameObject firstInzou;
		[SerializeField] private GameObject secondInzou;
		[SerializeField] private GameObject thirdInzou;

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

			InzouBar = GameObject.FindGameObjectWithTag("InzouBar");
			firstInzou = InzouBar.transform.GetChild(0).gameObject;
			secondInzou = InzouBar.transform.GetChild(1).gameObject;
			thirdInzou = InzouBar.transform.GetChild(2).gameObject;
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

			AddInzouSignToSequence(InzouSign.Ten); // add inzou sign to sequence

			

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			TenFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

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

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		public void AddInzouSignToSequence(InzouSign sign)
		{
			if (sequenceIndex < MAX_SEQUENCE_LENGTH)
			{
				currentInzouSequence[sequenceIndex] = sign;

				SetInzouUI(sign);

				sequenceIndex++;

				Timeout(); // start timeout coroutine

				CheckJutsu(); // check if jutsu is selected
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

		private void SetInzouUI(InzouSign sign)
		{
			// To do: set inzou UI
			GameObject currentInzouImage = InzouBar.transform.GetChild(sequenceIndex).gameObject;

			switch (sign)
				{
					case InzouSign.Ten:
						currentInzouImage.GetComponent<Image>().sprite = TenSprite;
						currentInzouImage.SetActive(true);
						break;
					case InzouSign.Chi:
						currentInzouImage.GetComponent<Image>().sprite = ChiSprite;
						currentInzouImage.SetActive(true);
						break;
					case InzouSign.Jin:
						currentInzouImage.GetComponent<Image>().sprite = JinSprite;
						currentInzouImage.SetActive(true);
						break;
					default:
						break;
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

		public void CheckJutsu()
		{
			if (WindwallJutsuSequence())
			{
				Debug.Log("Set Windwall Jutsu UI");
			}
			else if (FireballJutsuSequence())
			{
				Debug.Log("Set Fireball Jutsu UI");
			}
			else
			{
				Debug.Log("No jutsu activated");
			}
		}

		private void Timeout()
		{
			if (isTimeoutRunning)
				{
					Debug.LogWarning("Stopping inzou sequence timeout");
					StopCoroutine("InzouSequenceTimeOutCoroutine");
				}
				StartCoroutine("InzouSequenceTimeOutCoroutine");
		}

		public void ResetInzouSequence()
		{
			sequenceIndex = 0; // reset sequence index
			currentInzouSequence = new InzouSign[MAX_SEQUENCE_LENGTH]; // reset inzou sequence

			StopCoroutine("InzouSequenceTimeOutCoroutine"); // stop timeout coroutine
			isTimeoutRunning = false; // reset timeout bool

			// reset inzou UI
			firstInzou.SetActive(false);
			secondInzou.SetActive(false);
			thirdInzou.SetActive(false);

			Debug.LogWarning("Inzou sequence reset");
			Debug.LogWarning("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		public bool FireballJutsuSequence()
		{

			if (currentInzouSequence[0] == InzouSign.Ten && sequenceIndex == 1 || currentInzouSequence[0] == InzouSign.Chi && sequenceIndex == 1 || currentInzouSequence[0] == InzouSign.Jin && sequenceIndex == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool WindwallJutsuSequence()
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
		IEnumerator InzouSequenceTimeOutCoroutine()
		{
			Debug.LogWarning("Starting inzou sequence timeout");
			isTimeoutRunning = true;
			yield return new WaitForSeconds(InzouSequenceTimeOutDuration);
			isTimeoutRunning = false;
			Debug.LogWarning("Inzou sequence timed out");
			ResetInzouSequence();

		}
    }
}
