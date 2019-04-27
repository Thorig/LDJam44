using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

namespace GameLib.Entity.NonPlayerCharacter.StateMachine
{
    public class AICharacterController
    {
        protected IBrain currentBrain;
        protected IBrain oldBrain;
        private IBrainFactory factory;

        public IBrainFactory Factory
        {
            get
            {
                return factory;
            }

            set
            {
                factory = value;
            }
        }

        public virtual void init(AICharacter character, IBrainFactory factory)
        {
            Factory = factory;
            currentBrain = factory.getMoveBrain(character);
        }

        public void updateBrain(AICharacter character)
        {
            currentBrain.update(character);
        }

        public void switchBrain(IBrain brain)
        {
            oldBrain = currentBrain;
            currentBrain = brain;
        }

        public void switchOldBrainBack(IBrain brain)
        {
            currentBrain = oldBrain;
        }
    }
}