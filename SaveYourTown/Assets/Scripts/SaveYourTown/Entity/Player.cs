using GameLib.System.Gravity2D;
using SaveYourTown.Entity.Behaviour;
using SaveYourTown.Entity.NonPlayerCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace SaveYourTown.Entity
{
	public class Player : GameLib.Entity.Player
    {
        public Image vitalityBar;

        public GameObject button;
        public GameObject backGroundShop;
        public GameObject shopCursor;
        public float totalVitality = 100;
        public float currentVitality = 100;
        public bool doDamage;
        public bool isDefending = false;

        public int power;

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

                if (currentVitality <= 0)
                {
                    currentVitality = 0;
                    State = Entity.getBehaviourStateFactory().getDeathState(Entity);
                }

                Vector3 scale = new Vector3();
                scale.x = (currentVitality) / totalVitality;
                scale.y = 1;
                scale.z = 1;
                vitalityBar.transform.localScale = scale;
            }
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
