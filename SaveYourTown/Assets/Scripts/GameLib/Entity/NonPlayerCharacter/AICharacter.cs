using GameLib.Entity.NonPlayerCharacter.StateMachine;
using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;
using GameLib.System.Controller;
using UnityEngine;

namespace GameLib.Entity.NonPlayerCharacter
{
    public class AICharacter : Player
    {
        [SerializeField]
        protected AICharacterController aiCharacterController;

        public AICharacterController AiCharacterController
        {
            get
            {
                return aiCharacterController;
            }

            set
            {
                aiCharacterController = value;
            }
        }

        [SerializeField]
        public Vector3 lastPosition;

        [SerializeField]
        protected float positionY;

        [SerializeField]
        protected float positionX;

        protected override void init()
        {
            base.init();
            IAIControllerFactory controllerFactory = AIControllerFactory.getEntity();
            aiCharacterController = controllerFactory.getAICharacterController(this, new BrainFactory());
        }

        public override void LateUpdate()
        {
            if (frameCounter % activeFrame == 0)
            {
                fixedUpdateBody();
            }

            base.LateUpdate();
        }

        protected virtual void fixedUpdateBody()
        { 
            keysPressed.reset();
            aiCharacterController.updateBrain(this);
            lastPosition = transform.position;
            positionY = transform.position.y;
            positionX = transform.position.x;
        }

        public void setKeysPressed(KeysPressed keysPressed)
        {
            this.keysPressed = keysPressed;
        }
    }
}
