namespace GameLib.Entity.NonPlayerCharacter.StateMachine.Logic
{
    public interface IBrainFactory
    {
        IBrain getNoBrain(AICharacter character);

        IBrain getMoveBrain(AICharacter character);
    }
}