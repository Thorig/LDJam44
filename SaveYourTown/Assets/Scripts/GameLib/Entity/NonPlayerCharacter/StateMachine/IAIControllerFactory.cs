using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

namespace GameLib.Entity.NonPlayerCharacter.StateMachine
{
    public interface IAIControllerFactory
    {
        AICharacterController getAICharacterController(AICharacter character, IBrainFactory factory);
    }
}