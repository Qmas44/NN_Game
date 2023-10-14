using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MoreMountains.TopDownEngine
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private Image upgradeImage;

        public void Set(UpgradeData upgradeData)
        {
            upgradeImage.sprite = upgradeData.upgradeSprite;
        }

        internal void Clean()
        {
            upgradeImage.sprite = null;
        }

    }
}