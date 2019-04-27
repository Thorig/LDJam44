using GameLib.Entity.Animation;
using GameLib.Entity.Behaviour;
using GameLib.Level;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
    class Attack : Move
    {
        private bool doMovement = true;

        public override void animationMessage(int messageId, IEntity entity)
        {
            entity.getTransform().gameObject.GetComponent<Player>().AnimationAttributes.setCooldown();
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

                keysPressed.right = (doMovement && !entity.isFlipped());
                keysPressed.left = (doMovement && entity.isFlipped());

                Gravity gravity = entity.getGravity();

                moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());
            }
        }
    }
}
