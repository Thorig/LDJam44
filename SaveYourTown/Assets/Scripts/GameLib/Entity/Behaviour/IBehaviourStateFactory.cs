namespace GameLib.Entity.Behaviour
{
    public interface IBehaviourStateFactory
    {
        IEntity getBehaviourState(Player player);
        IEntity getBehaviourStateForPlayer(Player player);
        AbstractState getIdleState(IEntity entity);
        AbstractState getMoveState(IEntity entity);
        AbstractState getJumpUpState(IEntity entity);
        AbstractState getFallState(IEntity entity);
        AbstractState getHitState(IEntity entity);
        AbstractState getDeathState(IEntity entity);
        AbstractState getAttackState(IEntity entity);
    }
}