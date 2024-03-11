using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using ProceduralToolkit;


namespace MoreMountains.TopDownEngine
{
	public enum InzouSign
	{
		Default,
		Rin,
		Kai,
		Zen
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

		[SerializeField] private bool inzouReady = true;

		[Header("Inzou Sprites")]
		[SerializeField] private Sprite RinSprite;
		[SerializeField] private Sprite KaiSprite;
		[SerializeField] private Sprite ZenSprite;

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
		public MMFeedbacks RinFeedback;
		public MMFeedbacks KaiFeedback;
		public MMFeedbacks ZenFeedback;

        /// <summary>
		/// On init we initialize our cooldown and feedback
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();
			RinFeedback?.Initialization(this.gameObject);
			KaiFeedback?.Initialization(this.gameObject);
			ZenFeedback?.Initialization(this.gameObject);

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
			    || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
			{
				return;
			}

			if (_inputManager.MudaraModeButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
			{
				Debug.Log("Mudara mode button pressed");

				if (_inputManager.RinButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete()) // add conditional to check if sequence is comeplete here
				{
					Debug.Log("Rin button pressed");
					RinStart();
				}
				if (_inputManager.KaiButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete())
				{
					Debug.Log("Kai button pressed");
					KaiStart();
				}
				if (_inputManager.ZenButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !InzouSequenceComplete())
				{
					Debug.Log("Zen button pressed");
					ZenStart();
				}
			}
			
		}

        /// <summary>
		/// Starts Rin
		/// </summary>
		private void RinStart()
		{
			if (!inzouReady)
			{
				return;
			}

			AddInzouSignToSequence(InzouSign.Rin); // add inzou sign to sequence		

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			RinFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		/// <summary>
		/// Starts Kai
		/// </summary>
		private void KaiStart()
		{
			if (!inzouReady)
			{
				return;
			}

			AddInzouSignToSequence(InzouSign.Kai);

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			KaiFeedback?.PlayFeedbacks(this.transform.position);
			PlayAbilityStartFeedbacks();

			Debug.Log("Current inzou sequence: " + GetCurrentInzouSequence()[0] + GetCurrentInzouSequence()[1] + GetCurrentInzouSequence()[2]);
		}

		/// <summary>
		/// Starts Zen
		/// </summary>
		private void ZenStart()
		{
			if (!inzouReady)
			{
				return;
			}

			AddInzouSignToSequence(InzouSign.Zen);

			// _movement.ChangeState(CharacterStates.MovementStates.Dashing); // To do: add yin state
			ZenFeedback?.PlayFeedbacks(this.transform.position);
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

				StartCoroutine(InzouDelay()); // start delay coroutine

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
					case InzouSign.Rin:
						currentInzouImage.GetComponent<Image>().sprite = RinSprite;
						currentInzouImage.SetActive(true);
						break;
					case InzouSign.Kai:
						currentInzouImage.GetComponent<Image>().sprite = KaiSprite;
						currentInzouImage.SetActive(true);
						break;
					case InzouSign.Zen:
						currentInzouImage.GetComponent<Image>().sprite = ZenSprite;
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
				// To do: set fireball jutsu UI
			}
			else
			{
				Debug.Log("No jutsu activated");
				// To do: set no jutsu UI
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

			if (currentInzouSequence[0] == InzouSign.Rin && currentInzouSequence[1] == InzouSign.Kai && currentInzouSequence[2] == InzouSign.Zen)
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
				if (currentInzouSequence[0] == InzouSign.Rin && currentInzouSequence[1] == InzouSign.Rin && currentInzouSequence[2] == InzouSign.Rin)
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

		IEnumerator InzouDelay()
		{
			inzouReady = false;
			Debug.LogWarning("Starting inzou delay");
			yield return new WaitForSeconds(0.2f);
			Debug.LogWarning("Inzou delay complete");
			inzouReady = true;
		}
    }
}
