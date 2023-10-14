using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace MoreMountains.TopDownEngine
{
    public class AbilityManager : MonoBehaviour
    {
        public Character character;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); // need to refactor here to get player prefab not in update
        }

        public void UnlockAbility(CharacterAbility ability)
        {
            if(ability == null)
            {
                return;
            }
            else
            {
                Debug.Log("Unlocking ability: " + ability.GetType().Name);
                character.FindAbilityByString(ability.GetType().Name).PermitAbility(true);
            }
        }
    }
}
