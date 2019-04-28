using GameLib.Entity.Behaviour;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using SaveYourTown.Entity.Animation;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
    class Idle : GameLib.Entity.Behaviour.State.Idle
    {
        public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();

            Player player = entity.getTransform().gameObject.GetComponent<Player>();

            IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();

            if (gravity.isStanding() && (keysPressed.down || keysPressed.up || keysPressed.left || keysPressed.right))
            {
                entity.setState(behaviourStateFactory.getMoveState(entity));
            }

            if (player != null && player.IsNearShop)
            {
                if (keysPressed.actionButtonOne)
                {
                    entity.setState(((BehaviourStateFactory)behaviourStateFactory).getShopState(entity));
                }
            }
            else
            {
                if (keysPressed.attack)
                {

                    entity.setState(((BehaviourStateFactory)behaviourStateFactory).getAttackState(entity, Move.getDirection(keysPressed)));
                }
                else if (keysPressed.actionButtonOne)
                {
                    entity.setState(((BehaviourStateFactory)behaviourStateFactory).getDefendState(entity));
                }
                else
                {
                    base.update(entity);
                }
            }
        }
    }
}
