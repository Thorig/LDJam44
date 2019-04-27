namespace GameLib.Entity.NonPlayerCharacter.StateMachine.Logic
{
    public abstract class IBrain
    {
        protected float switchBrainCounter;

        public float SwitchBrainCounter { get { return switchBrainCounter;  } set { switchBrainCounter = value; } }

        abstract public void init(AICharacter character);
        abstract public void update(AICharacter character);
    }
}