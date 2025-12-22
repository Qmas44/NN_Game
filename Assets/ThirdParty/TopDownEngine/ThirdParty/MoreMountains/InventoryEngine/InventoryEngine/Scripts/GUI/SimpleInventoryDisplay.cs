using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MoreMountains.InventoryEngine
{	
	[SelectionBase]
	/// <summary>
	/// A component that handles the visual representation of an Inventory, allowing the user to interact with it
	/// </summary>
	public class SimpleInventoryDisplay : MonoBehaviour
	{
		[Header("Binding")]
		/// the name of the inventory to display
		/// <summary>
		/// Grabs the target inventory based on its name
		/// </summary>
		/// <value>The target inventory.</value>
		public Inventory TargetInventory;

		public int AmmoCount;
		public string AmmoItemID = "BaseCharacterTestGunAmmo";

		void Update()
		{
			AmmoCount = TargetInventory.GetQuantity(AmmoItemID);
		}
		
	}
}