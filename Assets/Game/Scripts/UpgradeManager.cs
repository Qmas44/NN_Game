using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace MoreMountains.TopDownEngine
{
    public class UpgradeManager : MMMonoBehaviour
    {
        public Character _character;

        public MMStateMachine<CharacterStates.CharacterConditions> ConditionState;

        [SerializeField] LevelUpManager levelUpManager;

        [SerializeField] InputManager inputManager;

        [SerializeField] private GameObject panel;

        [SerializeField] List<UpgradeButton> upgradeButtons;

        private void Start()
        {
        }

        private void Update()
        {

            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return;
            }

            _character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); // need to refactor here to get player prefab not in update   
        }

        public void OpenPanel(List<UpgradeData> upgradeDatas)
        {
            CleanPanel(); // clean panel before open
            panel.SetActive(true); // open panel


            _character.FindAbility<CharacterPause>().PermitAbility(false); // disable pause button

            // Set upgrade buttons
            for (int i = 0; i < upgradeDatas.Count; i++)
            {
                upgradeButtons[i].gameObject.SetActive(true); // show upgrade buttons
                upgradeButtons[i].Set(upgradeDatas[i]);
            }


            // Pause game
            CharacterPause characterPause = _character.FindAbility<CharacterPause>();
            MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0f, 0f, false, 0f, true);
            characterPause.PauseCharacter();
        }

        public void CleanPanel()
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                upgradeButtons[i].Clean();
            }
        }
        public void ClosePanel()
        {
            HideButtons(); // hide upgrade buttons

            _character.FindAbility<CharacterPause>().PermitAbility(true); // enable pause button

            // we trigger a Pause event for the GameManager and other classes that could be listening to it too
            panel.SetActive(false);
            CharacterPause characterPause = _character.FindAbility<CharacterPause>();
            characterPause.UnPauseCharacter();
            MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 1f, 0f, false, 0f, true);

        }

        private void HideButtons()
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                upgradeButtons[i].gameObject.SetActive(false); // hide upgrade buttons
            }
        }

        public void Upgrade(int pressedButtonID)
        {
           levelUpManager.Upgrade(pressedButtonID);
           ClosePanel();
        }

        public void UpgradeHealth()
        {
            Health health = _character.GetComponent<Health>();
            health.MaximumHealth += 10;
            health.CurrentHealth += 10;
        }
    }
}
