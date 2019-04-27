namespace GameLib.Entity.NonPlayerCharacter.StateMachine.Logic
{
    public class BrainFactory : IBrainFactory
    {
        public BrainFactory()
        {

        }
        
        public virtual IBrain getNoBrain(AICharacter character)
        {
            IBrain noBrain = new NPCNoBrain();
            noBrain.init(character);
            return noBrain;
        }

        public virtual IBrain getMoveBrain(AICharacter character)
        {
            IBrain moveBrain = new NPCMoveBrain();
            moveBrain.init(character);
            return moveBrain;
        }
    }
}