using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;

using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
	public class Attack : IBrain
	{
		private GameObject player;

		public override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			player = GameObject.FindGameObjectWithTag("Player");
			//Initialize your attributes or other necessities below.

		}

		public override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			//Add your code below.

		}
	}
}