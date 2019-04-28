using GameLib.Entity.Behaviour;
using SaveYourTown.Entity.Behaviour.State;

namespace SaveYourTown.Entity.Behaviour
{
	public class BehaviourStateFactory : GameLib.Entity.Behaviour.BehaviourStateFactory
	{
        private AbstractState entityDefend = null;
        private AbstractState entityShopping = null;

        public virtual AbstractState getShopState(IEntity entity)
        {
            if (entityShopping == null)
            {
                entityShopping = new Shopping();
            }
            entityShopping.init(entity);
            return entityShopping;
        }

        public virtual AbstractState getDefendState(IEntity entity)
        {
            if (entityDefend == null)
            {
                entityDefend = new Defend();
            }
            entityDefend.init(entity);
            return entityDefend;
        }

        public override AbstractState getMoveState(IEntity entity)
		{
			if (entityMove == null)
			{
				entityMove = new Move();
			}
			entityMove.init(entity);
			return entityMove;
		}

        public override AbstractState getIdleState(IEntity entity)
        {
            if (entityIdle == null)
            {
                entityIdle = new Idle();
            }
            entityIdle.init(entity);
            return entityIdle;
        }

        public override AbstractState getAttackState(IEntity entity)
        {
            return getAttackState(entity, -1);
        }

        public virtual AbstractState getAttackState(IEntity entity, int directionOfAttack)
        {
            if (entityAttack == null)
            {
                entityAttack = new Attack();
            }
            ((Attack)entityAttack).init(entity, directionOfAttack);
            return entityAttack;
        }
    }
}
