using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Add this ability to a character, and it'll be able to control time when pressing the TimeControl button
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Bullet Time")]
	public class CharacterBulletTime : CharacterAbility
	{
		public enum Modes { OneTime, Continuous }
        
		/// the chosen mode for this ability : one time will stop time for the specified duration on button press, even if you release it, while continuous will stop time while the button is pressed, until cooldown consumption duration expiration
		[Tooltip("the chosen mode for this ability : one time will stop time for the specified duration on button press, even if you release it, while continuous will stop time while the button is pressed, until cooldown consumption duration expiration")]
		public Modes Mode = Modes.Continuous;
		/// the radius to search our target in
		[Tooltip("the radius to search our target in")]
		public float Radius = 4f;
		/// the time scale to switch to when the time control button gets pressed
		[Tooltip("the time scale to switch to when the time control button gets pressed")]
		public float TimeScale = 0.5f;
		/// the duration for which to keep the timescale changed
		[Tooltip("the duration for which to keep the timescale changed")]
		[MMEnumCondition("Mode", (int)Modes.OneTime)]
		public float OneTimeDuration = 1f;
		/// whether or not the timescale should get lerped
		[Tooltip("whether or not the timescale should get lerped")]
		public bool LerpTimeScale = true;
		/// the speed at which to lerp the timescale
		[Tooltip("the speed at which to lerp the timescale")]
		public float LerpSpeed = 5f;
		/// the cooldown for this ability
		[Tooltip("the cooldown for this ability")]
		public MMCooldown Cooldown;

		/// the layers that will be damaged by this object
		[Tooltip("the layers that will be damaged by this object")]
		public LayerMask TargetLayerMask;

		protected bool _timeControlled = false;
		protected float _detectionTimer = 0f;
		protected bool _detectionActive = false;


		/// <summary>
		/// Watches for input press
		/// </summary>
		protected override void HandleInput()
		{
			base.HandleInput();
			if (!AbilityAuthorized)
			{
				return;
			}
			if (_inputManager.DashButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				TimeControlStart();
			}
			if (_inputManager.DashButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
			}
		}


		/// <summary>
		/// Starts the time scale modification
		/// </summary>
		public virtual void TimeControlStart()
		{
			
			// Start detection for 1 second
			StartDetectionForDuration(1f);
			Debug.Log("Starting detection collider");
			//MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, TimeScale, OneTimeDuration, LerpTimeScale, LerpSpeed, false);
		}

		/// <summary>
		/// Starts enemy detection for a specified duration
		/// </summary>
		/// <param name="duration">How long to detect enemies (in seconds)</param>
		public virtual void StartDetectionForDuration(float duration)
		{
			_detectionTimer = duration;
			_detectionActive = true;
		}

		/// <summary>
		/// Stops enemy detection
		/// </summary>
		public virtual void StopDetection()
		{
			_detectionActive = false;
			_detectionTimer = 0f;
		}

		/// <summary>
		/// Debug method to test overlap sphere manually
		/// </summary>
		[ContextMenu("Test Overlap Sphere")]
		public virtual void TestOverlapSphere()
		{
			Debug.Log("=== MANUAL OVERLAP SPHERE TEST ===");
			CheckForEnemyHitboxes();
			Debug.Log("=== END TEST ===");
		}

		private void CheckForEnemyHitboxes()
		{
			Debug.Log("Checking for hitboxes at position: " + transform.position + " with radius: " + Radius + " on layer: " + LayerMask.LayerToName(TargetLayerMask));

			// Check ALL colliders first (no layer mask)
			Collider[] allColliders = Physics.OverlapSphere(transform.position, Radius);
			Debug.Log("Found " + allColliders.Length + " total colliders in radius");

			// Check with layer mask
			Collider[] Colliders = Physics.OverlapSphere(transform.position, Radius, TargetLayerMask);
			Debug.Log("Found " + Colliders.Length + " colliders on target layer");

			foreach (Collider col in Colliders)
			{
				Debug.Log("Found collider: " + col.gameObject.name + " on layer: " + LayerMask.LayerToName(col.gameObject.layer));
			}

			if(Colliders != null && Colliders.Length > 0)
			{
				Debug.LogWarning("Found " + Colliders.Length + " enemies in range - DO SOMETHING");
			}
		}

		/// <summary>
		/// Updates the detection timer and checks for enemies while active
		/// </summary>
		protected virtual void Update()
		{
			if (_detectionActive)
			{
				_detectionTimer -= Time.deltaTime;

				// Check for enemies every frame while detection is active
				CheckForEnemyHitboxes();

				// Stop detection when timer runs out
				if (_detectionTimer <= 0f)
				{
					StopDetection();
					Debug.Log("Detection duration ended");
				}
			}
		}

		/// <summary>
		/// Draws gizmos for the detection circle
		/// </summary>
		protected virtual void OnDrawGizmosSelected()
		{
			// Draw detection sphere
        	Gizmos.color = Color.red;
        	Gizmos.DrawWireSphere(transform.position, Radius);

			// While detecting, draw filled sphere too
			if (_detectionActive)
			{
				Gizmos.color = new Color(1f, 0f, 0f, 0.3f); // Semi-transparent red
				Gizmos.DrawSphere(transform.position, Radius);
			}
		}


		
	}
}