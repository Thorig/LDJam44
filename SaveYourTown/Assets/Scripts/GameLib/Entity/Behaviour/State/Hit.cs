using GameLib.Entity.Animation;
using GameLib.System.Audio;

namespace GameLib.Entity.Behaviour.State
{
    public class Hit : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
          
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_HITTED, entity);
            entity.playAudio(AudioAttributes.HITTED_SOUND);
        }

        public override void update(IEntity entity)
        {
            
        }
    }
}