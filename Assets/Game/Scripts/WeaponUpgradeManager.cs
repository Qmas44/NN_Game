using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Unity.VisualScripting;


namespace MoreMountains.TopDownEngine
{
    public class WeaponUpgradeManager : MonoBehaviour
    {

        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Upgrade(UpgradeData upgradeData)
        {
            if (upgradeData.weapon == null) { return; }      

            Weapon currentWeapon = player.GetComponent<CharacterHandleWeapon>().GetCurrentWeapon();

            if (currentWeapon == null) { return; }

            if (currentWeapon.name == upgradeData.weapon.name)
            {
                Debug.Log("weapon matches ");
                if (currentWeapon.name == "Kunai")
                {
                    // Get the kunai weapon object
                    GameObject kunaiWeaponObject = currentWeapon.gameObject;
                    MMSimpleObjectPooler kunaiPooler = kunaiWeaponObject.GetComponent<MMSimpleObjectPooler>();
                    GameObject kunaiPoolObject = kunaiWeaponObject.GetComponent<MMSimpleObjectPooler>().GameObjectToPool;
                    Projectile kunaiProjectile = kunaiPoolObject.GetComponent<Projectile>();
                    Health kunaiHealth = kunaiPoolObject.GetComponent<Health>();

                    switch (upgradeData.upgradeLevel)
                    {
                        case UpgradeLevel.Level1:
                            Debug.Log("Level 1");
                            // Upgrade kunai damage caused
                            kunaiProjectile.SetDamageCaused(kunaiProjectile.GetMinDamageCaused(), kunaiProjectile.GetMaxDamageCaused() + upgradeData.weaponDamageCaused);
                            // Upgrade kunai Speed
                            currentWeapon.SetTimeBetweenUses(currentWeapon.TimeBetweenUses - upgradeData.weaponSpeed);
                            break;
                        case UpgradeLevel.Level2:
                            Debug.Log("Level 2");
                            // Upgrade kunai damage caused
                            kunaiProjectile.SetDamageCaused(kunaiProjectile.GetMinDamageCaused() + upgradeData.weaponDamageCaused, kunaiProjectile.GetMaxDamageCaused() + upgradeData.weaponDamageCaused);

                            // Upgrade kunai health
                            kunaiHealth.CurrentHealth += upgradeData.weaponHealth;
                            kunaiHealth.InitialHealth += upgradeData.weaponHealth;

                            // Upgrade kunai Speed
                            currentWeapon.SetTimeBetweenUses(currentWeapon.TimeBetweenUses - upgradeData.weaponSpeed);
                            break;
                        case UpgradeLevel.Level3:
                            Debug.Log("Level 3");
                            // Upgrade kunai damage caused
                            kunaiProjectile.SetDamageCaused(kunaiProjectile.GetMinDamageCaused() + upgradeData.weaponDamageCaused, kunaiProjectile.GetMaxDamageCaused() + upgradeData.weaponDamageCaused);
                            // Upgrade kunai Speed
                            currentWeapon.SetTimeBetweenUses(currentWeapon.TimeBetweenUses - upgradeData.weaponSpeed);
                            break;
                        case UpgradeLevel.Level4:
                            Debug.Log("Level 4");
                            break;
                        case UpgradeLevel.Level5:
                            Debug.Log("Level 5");
                            break;
                    }
                
                    // Update kunai pooler
                    kunaiPooler.ClearPool();
                    kunaiPooler.FillObjectPool();
                }
            }
        }
    }
}
