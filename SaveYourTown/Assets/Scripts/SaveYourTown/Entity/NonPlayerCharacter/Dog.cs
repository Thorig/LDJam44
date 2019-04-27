using GameLib.Entity.NonPlayerCharacter;
using SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog;
using GameLib.Entity.NonPlayerCharacter.StateMachine;
using SaveYourTown.Entity.Behaviour;

namespace SaveYourTown.Entity.NonPlayerCharacter
{
	public class Dog : AICharacter
	{
		// Use this for initialization
		protected override void init()
		{
			base.init();
			IAIControllerFactory controllerFactory = AIControllerFactory.getEntity();
			aiCharacterController = controllerFactory.getAICharacterController(this, new BrainFactory());
			//Initialize your attributes or other necessities below.

		}

        protected override void setEntity()
        {
            setEntity(new BehaviourStateFactory());
        }

        protected override void fixedUpdateBody()
		{
			//base.fixedUpdateBody();
			//Add your code below.

		}

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                entity.setState(entity.getBehaviourStateFactory().getAttackState(entity));
            }
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                entity.setState(entity.getBehaviourStateFactory().getIdleState(entity));
            }
        }

        public override void animationEffect(int messageId)
        {
            if (state is Behaviour.State.Attack)
            {
                ((Behaviour.State.Attack)state).effect(messageId);
            }
        }
    }
}
