using GameLib.Entity.Animation;
using GameLib.Level;
using UnityEngine;

namespace GameLib.Entity.Behaviour.State
{
    public class Attack : AbstractState
    {
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
        }

        public override void update(IEntity entity)
        {
            
        }
    }
}