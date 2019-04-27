using GameLib.Entity.Animation;

namespace GameLib.Entity.Behaviour.State
{
    public class Death : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
            //TODO: restart game
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_DEATH, entity);
        }

        public override void update(IEntity entity)
        {

        }
    }
}