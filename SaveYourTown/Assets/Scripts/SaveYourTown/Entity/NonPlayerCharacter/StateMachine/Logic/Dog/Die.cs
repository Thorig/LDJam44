using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;
using GameLib.System.Controller;
using SaveYourTown.System;
using System;
using UnityEngine;

namespace SaveYourTown.Entity.NonPlayerCharacter.StateMachine.Logic.Dog
{
    class Die : IBrain
    {
        private float respawnTimer;
        private GameObject player;

        public override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //Initialize your attributes or other necessities below.
            respawnTimer = UnityEngine.Random.Range(2.20f, 3.00f);
        }

        public override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)
        {
            //Add your code below.

            KeysPressed keysPressed = character.getEntity().getKeysPressed();
            keysPressed.reset();
            character.KeysPressed = keysPressed;

            respawnTimer -= Time.deltaTime;
            if (respawnTimer < 0.0f)
            {
                Sun sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Sun>();
                sun.respawn(character.gameObject);
            }
        }
    }
}