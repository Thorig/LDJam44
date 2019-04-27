using GameLib.Entity;

namespace GameLib.System.Gravity2D
{
    public interface IGravityFactory
    {
        Gravity getGravity();
        Gravity getNoGravity();
        IGravityClient getGravityClient(Actor actor);
        IGravityClient getGravityClientPlayer(Player actor);
        RayHitboxes getRayHitboxes();
        RayHitboxes3D getRayHitboxes3D();
    }
}