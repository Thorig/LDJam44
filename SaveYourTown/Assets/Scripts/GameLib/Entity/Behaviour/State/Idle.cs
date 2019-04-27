using GameLib.Entity.Animation;
using GameLib.Level;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using System;
using UnityEngine;

namespace GameLib.Entity.Behaviour.State
{
    public class Idle : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
      //      throw new NotImplementedException();
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_IDLE, entity);
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));
        }

        public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();

            IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();

            if (keysPressed.jump && gravity.isStanding() && entity.getJumpedReleased())
            {
                entity.setState(behaviourStateFactory.getJumpUpState(entity));
            }
            else if (gravity.isStanding() && ((keysPressed.left && !keysPressed.right) || (!keysPressed.left && keysPressed.right)))
            {              
                entity.setState(behaviourStateFactory.getMoveState(entity));
            }
            else if (!gravity.isStanding())
            {
                entity.setState(behaviourStateFactory.getFallState(entity));
            }
        }
    }
}