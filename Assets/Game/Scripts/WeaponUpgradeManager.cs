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

        public void Upgrade(Weapon weapon)
        {
            if (weapon == null) { return; }      

            Weapon currentWeapon = player.GetComponent<CharacterHandleWeapon>().GetCurrentWeapon();

            if (currentWeapon == null) { return; }

            if (currentWeapon.name == weapon.name)
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

                    // Upgrade kunai damage caused
                    kunaiProjectile.SetDamageCaused(kunaiProjectile.GetMinDamageCaused(), kunaiProjectile.GetMaxDamageCaused() + 5);
                    
                    // Upgrade kunai health
                    kunaiHealth.CurrentHealth += 1;
                    kunaiHealth.InitialHealth += 1;

                    // Upgrade kunai Speed
                    currentWeapon.SetTimeBetweenUses(currentWeapon.TimeBetweenUses - 0.1f);

                    // Update kunai pooler
                    kunaiPooler.ClearPool();
                    kunaiPooler.FillObjectPool();
                }
            }
        }
    }
}
