using GameLib.Entity.Animation;
using GameLib.Entity.Behaviour;
using GameLib.Level;
using GameLib.System.Controller;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
    public class Defend : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(Animation.AnimationAttributes.ANIMATION_DEFEND, entity);
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));
        }

        public override void update(IEntity entity)
        {
            KeysPressed keysPressed = entity.getKeysPressed();

            if (!keysPressed.actionButtonOne)
            {
                IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
                entity.setState(((BehaviourStateFactory)behaviourStateFactory).getIdleState(entity));
            }
        }
    }
}