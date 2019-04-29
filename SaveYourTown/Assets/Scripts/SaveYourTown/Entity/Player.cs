using GameLib.System.Gravity2D;
using SaveYourTown.Entity.Behaviour;
using SaveYourTown.Entity.NonPlayerCharacter;
using SaveYourTown.System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SaveYourTown.Entity
{
	public class Player : GameLib.Entity.Player
    {
        public Image vitalityBar;

        public GameObject button;
        public GameObject backGroundShop;
        public GameObject shopCursor;
        public List<GameObject> notForSale;

        public int extraVitality = 0;
        public float totalVitality = 100;
        public float currentVitality = 100;
        public bool doDamage;
        public bool isDefending = false;

        [SerializeField]
        private Sun _sun;

        public int power;

        public int extraPower = 0;

        private bool isNearShop = false;

        public bool IsNearShop
        {
            get
            {
                return isNearShop;
            }
            set
            {
                isNearShop = value;
                button.SetActive(isNearShop);
            }
        }


        [SerializeField]
        private GameObject startScreen;

        public override void setGravity()
        {
            IGravityFactory gravityFactory = new GravityFactory();

            gravity = gravityFactory.getNoGravity();
            gravity.init(gravitySetting);
        }

        protected override void setEntity()
        {
            setEntity(new BehaviourStateFactory());
        }

        public override void animationEffect(int messageId)
        {
            if(state is Behaviour.State.Attack)
            {
                ((Behaviour.State.Attack)state).effect(messageId);
            }
        }

        private void checkDamage(Collision2D collision)
        {
            Dog dog = collision.gameObject.GetComponent<Dog>();
            if (dog != null && dog.doDamage && !isDefending)
            {
                dog.doDamage = false;
                currentVitality -= dog.power;

                _sun.showBlood(transform.position);

                if (currentVitality <= 0)
                {
                    currentVitality = 0;
                    State = Entity.getBehaviourStateFactory().getDeathState(Entity);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                updateVitalityBar();
            }
        }

        private void updateVitalityBar()
        {
            Vector3 scale = new Vector3();
            scale.x = (currentVitality) / (totalVitality + extraVitality);
            scale.y = 1;
            scale.z = 1;
            vitalityBar.transform.localScale = scale;
        }

        public void resetVitality()
        {
            currentVitality = totalVitality + extraVitality;
            updateVitalityBar();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            checkDamage(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            checkDamage(collision);
        }



        public override void LateUpdate()
		{
			base.LateUpdate();

            if(startScreen.activeInHierarchy && (keysPressed.down || keysPressed.up || keysPressed.left || keysPressed.right))
            {
                startScreen.SetActive(false);
            }
        }

        public void damage()
        {
            doDamage = true;
        }

        public void noDamage()
        {
            doDamage = false;
        }
    }
}
