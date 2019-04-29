using GameLib.Entity.NonPlayerCharacter;
using SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog;
using GameLib.Entity.NonPlayerCharacter.StateMachine;
using SaveYourTown.Entity.Behaviour;
using UnityEngine;
using SaveYourTown.System;

namespace SaveYourTown.Entity.NonPlayerCharacter
{
	public class Dog : AICharacter
	{
        public Player target;
        public int vitality;
        public int power;
        public Collider2D trigger;

        [SerializeField]
        private Sun _sun;

        public bool doDamage;

        // Use this for initialization
        protected override void init()
		{
			base.init();
			IAIControllerFactory controllerFactory = AIControllerFactory.getEntity();
			aiCharacterController = controllerFactory.getAICharacterController(this, new BrainFactory());
            //Initialize your attributes or other necessities below.
            aiCharacterController.switchBrain(((BrainFactory)aiCharacterController.Factory).getIdleBrain(this));
            doDamage = false;
            trigger.enabled = true;
        }

        protected override void setEntity()
        {
            setEntity(new BehaviourStateFactory());
        }

        protected override void fixedUpdateBody()
		{
			base.fixedUpdateBody();
			//Add your code below.

		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (target == null && collision.tag.CompareTo("Player") == 0)
            {
                trigger.enabled = false;
                target = collision.gameObject.GetComponent<Player>();
                aiCharacterController.switchBrain(((BrainFactory)aiCharacterController.Factory).getAttackBrain(this));
            }
        }

        private void checkDamage(Collision2D collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.doDamage)
            {
                player.doDamage = false;
                vitality -= player.power + player.extraPower;
                _sun.showBlood(transform.position);
                if(vitality <= 0)
                {
                    target = null;
                    vitality = 20;
                    aiCharacterController.switchBrain(((BrainFactory)aiCharacterController.Factory).getDieBrain(this));
                    State = Entity.getBehaviourStateFactory().getDeathState(Entity);
                    _sun.addBloodAmount(2);
                }
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

        public void walk()
        {
            aiCharacterController.switchBrain(((BrainFactory)aiCharacterController.Factory).getMoveBrain(this));
        }

        public override void animationEffect(int messageId)
        {
            if (state is Behaviour.State.Attack)
            {
                ((Behaviour.State.Attack)state).effect(messageId);
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

        public void reset()
        {
            target = null;
            vitality = 20;
            aiCharacterController.switchBrain(((BrainFactory)aiCharacterController.Factory).getIdleBrain(this));
        }
    }
}
