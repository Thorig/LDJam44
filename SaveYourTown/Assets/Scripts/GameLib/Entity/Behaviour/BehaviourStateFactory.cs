using GameLib.Entity.Behaviour.State;

namespace GameLib.Entity.Behaviour
{
    public class BehaviourStateFactory : IBehaviourStateFactory
    {
        protected AbstractState entityIdle = null;
        protected AbstractState entityMove = null;
        protected AbstractState entityJumpUp = null;
        protected AbstractState entityFall = null;
        protected AbstractState entityHit = null;
        protected AbstractState entityDeath = null;
        protected AbstractState entityAttack = null;

        public IEntity getBehaviourState(Player player)
        {
            return new Entity(player);
        }

        public virtual IEntity getBehaviourStateForPlayer(Player player)
        {
            return new EntityPlayer(player, (player.RayInformationFlippedX != null));
        }

        public virtual AbstractState getIdleState(IEntity entity)
        {
            if (entityIdle == null)
            {
                entityIdle = new Idle();
            }

            entityIdle.init(entity);
            return entityIdle;
        }

        public virtual AbstractState getMoveState(IEntity entity)
        {
            if (entityMove == null)
            {
                entityMove = new Move();
            }

            entityMove.init(entity);
            return entityMove;
        }

        public virtual AbstractState getJumpUpState(IEntity entity)
        {
            if (entityJumpUp == null)
            {
                entityJumpUp = new JumpUp();
            }

            entityJumpUp.init(entity);
            return entityJumpUp;
        }

        public virtual AbstractState getFallState(IEntity entity)
        {
            if (entityFall == null)
            {
                entityFall = new Fall();
            }

            entityFall.init(entity);
            return entityFall;
        }

        public virtual AbstractState getHitState(IEntity entity)
        {
            if (entityHit == null)
            {
                entityHit = new Hit();
            }

            entityHit.init(entity);
            return entityHit;
        }

        public virtual AbstractState getDeathState(IEntity entity)
        {
            if (entityDeath == null)
            {
                entityDeath = new Death();
            }

            entityDeath.init(entity);
            return entityDeath;
        }

        public virtual AbstractState getAttackState(IEntity entity)
        {
            if (entityAttack == null)
            {
                entityAttack = new Attack();
            }

            entityAttack.init(entity);
            return entityAttack;
        }
    }
}