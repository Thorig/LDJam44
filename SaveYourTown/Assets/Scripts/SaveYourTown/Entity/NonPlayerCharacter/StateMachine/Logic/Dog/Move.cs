using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;
using GameLib.System.Controller;
using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
	public class Move : IBrain
	{
		private GameObject player;
        private Vector2 newPosition;
        private float maxTimeForWalking;
        private bool isWaiting = false;

		public override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
			player = GameObject.FindGameObjectWithTag("Player");
            //Initialize your attributes or other necessities below.

            if (Random.value >= 0.501f)
            {
                newPosition.x = player.transform.position.x + Random.Range(-20.0f, 20.0f);
            }
            else
            {
                newPosition.y = player.transform.position.y + Random.Range(-20.0f, 20.0f);
            }

            maxTimeForWalking = Random.Range(0.20f, 0.70f);
            character.MovementSpeed = 2.0f;
            isWaiting = false;
        }

        public override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
            //Add your code below.

            KeysPressed keysPressed = character.getEntity().getKeysPressed();
            keysPressed.reset();

            if (isWaiting)
            {
                if (character.transform.position.x > player.transform.position.x)
                {
                    character.SpriteRenderer.flipX = true;
                }
                if (character.transform.position.x < player.transform.position.x)
                {
                    character.SpriteRenderer.flipX = false;
                }
            }
            else
            {
                if (newPosition.x > 0.0f)
                {
                    keysPressed.right = true;
                }
                if (newPosition.x < 0.0f)
                {
                    keysPressed.left = true;
                }
                if (newPosition.y > 0.0f)
                {
                    keysPressed.up = true;
                }
                if (newPosition.y < 0.0f)
                {
                    keysPressed.down = true;
                }
            }
            character.KeysPressed = keysPressed;

            maxTimeForWalking -= Time.deltaTime;
            if(!isWaiting && maxTimeForWalking < 0.0f)
            {
                maxTimeForWalking = Random.Range(0.50f, 2.0f);
                isWaiting = true;
            }
            else if(isWaiting && maxTimeForWalking < 0.0f)
            {
                character.AiCharacterController.switchBrain(((BrainFactory)character.AiCharacterController.Factory).getAttackBrain(character));
            }
        }
	}
}
