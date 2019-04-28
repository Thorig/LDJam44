using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
	public class Idle : IBrain
	{
		private GameObject player;

		public override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			player = GameObject.FindGameObjectWithTag("Player");
            //Initialize your attributes or other necessities below.
            character.State = character.Entity.getBehaviourStateFactory().getIdleState(character.Entity);
            ((NonPlayerCharacter.Dog)character).trigger.enabled = true;
        }

		public override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
            //Add your code below.
        }
	}
}
