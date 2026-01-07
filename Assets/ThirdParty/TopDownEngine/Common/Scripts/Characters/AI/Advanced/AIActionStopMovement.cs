using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Requires a CharacterMovement ability. Makes the character move up to the specified MinimumDistance in the direction of the target. 
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/AI/Actions/AIActionStopMovement")]
	//[RequireComponent(typeof(CharacterMovement))]
	public class AIActionStopMovement : AIAction
	{
		protected CharacterMovement _characterMovement;
		protected Vector2 _movementVector;

		/// <summary>
		/// On init we grab our CharacterMovement ability
		/// </summary>
		public override void Initialization()
		{
			if(!ShouldInitialize) return;
			base.Initialization();
			_characterMovement = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterMovement>();
		}

		/// <summary>
		/// On PerformAction we move
		/// </summary>
		public override void PerformAction()
		{
			_movementVector.x = 0f;
			_movementVector.y = 0f;
			_characterMovement.SetHorizontalMovement(0f);
			_characterMovement.SetVerticalMovement(0f);
			_characterMovement.SetMovement(_movementVector);
			Debug.Log("Setting AI movement to 0");
		}

		/// <summary>
		/// On exit state we stop our movement
		/// </summary>
		public override void OnExitState()
		{
			base.OnExitState();

			_characterMovement?.SetHorizontalMovement(0f);
			_characterMovement?.SetVerticalMovement(0f);
		}
	}
}