using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace MoreMountains.TopDownEngine
{
    public class CharacterUpgradeManager : MonoBehaviour
    {
        public Character character;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        }
        
    public void Upgrade(CharacterUpgradeType type)
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); // nned to refactor to now get character every time. Handling here for character death

            switch (type)
            {
                case CharacterUpgradeType.Health:
                    Health health = character.GetComponent<Health>();
                    health.MaximumHealth += 10;
                    health.CurrentHealth += 10;
                    break;
            }

        }
    }
}
