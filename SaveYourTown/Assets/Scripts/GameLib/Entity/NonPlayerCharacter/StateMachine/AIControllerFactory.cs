using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

namespace GameLib.Entity.NonPlayerCharacter.StateMachine
{
    public class AIControllerFactory : IAIControllerFactory
    {
        private static IAIControllerFactory entity;

        private AIControllerFactory()
        {

        }

        public static IAIControllerFactory getEntity()
        {
            if (entity == null)
            {
                entity = new AIControllerFactory();
            }

            return entity;
        }
        
        public AICharacterController getAICharacterController(AICharacter character, IBrainFactory factory)
        {
            AICharacterController controller = new AICharacterController();
            controller.init(character, factory);
            return controller;
        }
    }
}
