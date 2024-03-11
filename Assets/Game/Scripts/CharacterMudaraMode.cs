using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;
using ProceduralToolkit;
using Unity.VisualScripting;


namespace MoreMountains.TopDownEngine
{

	[AddComponentMenu("TopDown Engine/Character/Abilities/Character Mudara Mode")]
    public class CharacterMudaraMode : CharacterAbility
    {

		[SerializeField] private GameObject MudaraModeUI;

        /// <summary>
		/// On init we initialize our cooldown and feedback
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();

			MudaraModeUI = GameObject.FindGameObjectWithTag("MudaraPanel").transform.GetChild(0).gameObject;
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
			if (_inputManager.MudaraModeButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				ToggleDisplayUI();
			}

			if (_inputManager.MudaraModeButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
			{
				Debug.Log("Mudara mode button pressed");


				_characterMovement.MovementSpeed = 0;
			}

			if (_inputManager.MudaraModeButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
			{
				Debug.Log("Mudara mode button released");

				ToggleDisplayUI();

				_characterMovement.ResetSpeed();
			}	
		}

		public void ToggleDisplayUI()
		{
			if (MudaraModeUI.activeSelf)
			{
				MudaraModeUI.SetActive(false);
			}
			else
			{
				MudaraModeUI.SetActive(true);
			}
		}
		
        // Update is called once per frame
        void Update()
        {

        }
	}
}
