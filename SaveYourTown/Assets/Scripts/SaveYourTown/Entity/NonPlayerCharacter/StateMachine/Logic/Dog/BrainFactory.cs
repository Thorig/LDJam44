using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
	public class BrainFactory : GameLib.Entity.NonPlayerCharacter.StateMachine.Logic.BrainFactory
    {
        public virtual IBrain getDieBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)
        {
            IBrain newBrain = new Die();
            newBrain.init(character);
            return newBrain;
        }

        public virtual IBrain getIdleBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			IBrain newBrain = new Idle();
			newBrain.init(character); 
			return newBrain; 
		}

		public override IBrain getMoveBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			IBrain noBrain = new Move();
			noBrain.init(character); 
			return noBrain; 
		}

		public virtual IBrain getAttackBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			IBrain newBrain = new Attack();
			newBrain.init(character); 
			return newBrain; 
		}

	}
}
