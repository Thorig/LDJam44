using GameLib.Entity.Animation;
using GameLib.Entity.Behaviour;
using GameLib.Level;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using SaveYourTown.Entity.NonPlayerCharacter;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
    class Attack : Move
    {
        private bool doMovement = true;
        private int direction;

        public override void animationMessage(int messageId, IEntity entity)
        {
            if (entity.getTransform().gameObject.GetComponent<Player>() != null)
            {
                entity.getTransform().gameObject.GetComponent<Player>().AnimationAttributes.setCooldown();
            }
            else if (entity.getTransform().gameObject.GetComponent<Dog>() != null)
            {
                entity.getTransform().gameObject.GetComponent<Dog>().AnimationAttributes.setCooldown();
                entity.getTransform().gameObject.GetComponent<Dog>().walk();
            }
            entity.setState(entity.getBehaviourStateFactory().getIdleState(entity));
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_ATTACK, entity);
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));
            doMovement = true;
        }

        public virtual void init(IEntity entity, int direction)
        {
            init(entity);
            this.direction = direction;
        }

        public void effect(int parameter)
        {
            if (parameter == 0)
            {
                doMovement = false;
            }
            if (parameter == 1)
            {
                doMovement = true;
            }
        }

        public override void update(IEntity entity)
        {
            if (doMovement)
            {
                KeysPressed keysPressed = entity.getKeysPressed();

                if (direction == -1)
                {
                    keysPressed.right = (doMovement && !entity.isFlipped());
                    keysPressed.left = (doMovement && entity.isFlipped());
                }
                /*else
                {
                    keysPressed.right = false;
                    keysPressed.left = false;
                }
                /*
                 up = 0
       up-left = 7     up-right = 1
     left = 6               right = 2
        down-left = 5 down-right = 3
                down = 4
                 
                if(direction >= 1 && direction <= 3)
                {
                    keysPressed.right = true;
                }
                if (direction >= 5 && direction <= 7)
                {
                    keysPressed.left = true;
                }

                if ((direction >= 0 && direction <= 1) || direction == 7)
                {
                    keysPressed.up = true;
                }
                if (direction >= 3 && direction <= 5)
                {
                    keysPressed.down = true;
                }
                */
                Gravity gravity = entity.getGravity();

                moveEntityUp(entity, keysPressed, gravity, entity.getMovementSpeed());
                moveEntityDown(entity, keysPressed, gravity, entity.getMovementSpeed());
                moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());
            }
        }
    }
}
