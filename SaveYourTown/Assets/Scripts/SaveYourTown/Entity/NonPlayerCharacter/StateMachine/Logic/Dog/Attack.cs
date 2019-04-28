using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;
using GameLib.System.Controller;
using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
	public class Attack : IBrain
	{
        private float maxAttackDistance = 0.5f;
        private Vector2 posTar;
        private Vector2 pos;
        private float attackCoolDown = 0.0f;

        public override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)
		{
            //Initialize your attributes or other necessities below.
            character.MovementSpeed = 5.0f;

        }

        public override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)
        {
            //Add your code below.
            //            Debug.Log("Dog is attacking");

            //1 if in position do the attack
            posTar = ((NonPlayerCharacter.Dog)character).target.transform.position;
            pos = ((NonPlayerCharacter.Dog)character).transform.position;

            KeysPressed keysPressed = character.getEntity().getKeysPressed();
            keysPressed.reset();

            attackCoolDown -= Time.deltaTime;

            if (attackCoolDown <= 0.0f && maxAttackDistance >= Vector2.Distance(posTar,
                pos))
            {
                attackCoolDown = Random.Range(2.0f, 4.0f);
                keysPressed.attack = true;
            }

            if ((pos.y) < (posTar.y - 0.1f))
            {
                keysPressed.up = true;
            }
            if (pos.y > (posTar.y + 0.1f))
            {
                keysPressed.down = true;
            }
            if (pos.x < (posTar.x - 0.1f))
            {
                keysPressed.right = true;
            }
            if (pos.x > (posTar.x + 0.1f))
            {
                keysPressed.left = true;
            }
            character.KeysPressed = keysPressed;
        }
    }
}
