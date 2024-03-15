using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// This decision returns true if the Character got hit this frame, or after the specified number of hits has been reached.
	/// </summary>
	[AddComponentMenu("TopDown Engine/Character/AI/Decisions/AIDecisionAttack")]
	//[RequireComponent(typeof(Health))]
	public class AIDecisionAttacking : AIDecision
	{
		/// <summary>
		/// On init we grab our Health component
		/// </summary>
		public override void Initialization()
		{
		}

		/// <summary>
		/// On Decide we check whether we've been hit
		/// </summary>
		/// <returns></returns>
		public override bool Decide()
		{
			return IsAttacking(); // Need to wait untill attack is complete
		}

		/// <summary>
		/// Checks whether we've been hit enough times
		/// </summary>
		/// <returns></returns>
		protected virtual bool IsAttacking()
		{
			if (CharacterStates.MovementStates.Attacking == _brain.gameObject.GetComponentInParent<Character>().MovementState.CurrentState) // Need to find a different way to check if the AI is attacking
				{
					Debug.Log("AI is Attacking");
					return true;
				}
			return false;
			
		}
	}
}