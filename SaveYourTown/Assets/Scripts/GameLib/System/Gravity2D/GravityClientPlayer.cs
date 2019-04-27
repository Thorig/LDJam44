//#define USE_3D_RAYS
using GameLib.Entity;

namespace GameLib.System.Gravity2D
{
    public class GravityClientPlayer : GravityClient
    {
        protected Player player = null;

        protected bool hasFlippedRayInformation;

        public GravityClientPlayer(Player actor) : base(actor)
        {
            player = actor;
            hasFlippedRayInformation = (player.RayInformationFlippedX != null);
        }
#if USE_3D_RAYS
        public override RayInformation3D getRayInformation3D()
        {
            RayInformation3D returnValue = actor.RayInformation3D;
            if (hasFlippedRayInformation && player.SpriteRenderer.flipX)
            {
                returnValue = player.RayInformationFlippedX3D;
            }
            return returnValue;
        }
#else
        public override RayInformation getRayInformation()
        {
            RayInformation returnValue = actor.RayInformation;
            if (hasFlippedRayInformation && player.SpriteRenderer.flipX)
            {
                returnValue = player.RayInformationFlippedX;
            }
            return returnValue;
        }
#endif
    }
}