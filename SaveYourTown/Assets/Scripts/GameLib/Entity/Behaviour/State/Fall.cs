using GameLib.Entity.Animation;
using GameLib.Level;
using GameLib.System.Audio;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using System;
using UnityEngine;

namespace GameLib.Entity.Behaviour.State
{
    public class Fall : Move
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void init(IEntity entity)
        {
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));
            switchAnimation(AnimationAttributes.ANIMATION_FALL, entity);
        }

        public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();
            
            moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());

            if (gravity.isStanding() || gravity.isNotFalling())
            {
                entity.playAudio(AudioAttributes.LANDING_SOUND);
                IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory(); 

                if (!keysPressed.left && !keysPressed.right)
                {
                    entity.setState(behaviourStateFactory.getIdleState(entity));
                }
                else
                {
                    entity.setState(behaviourStateFactory.getMoveState(entity));
                }
            }
        }
    }
}
