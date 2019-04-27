using GameLib.Entity;

namespace GameLib.System.Gravity2D
{
    public class GravityFactory : IGravityFactory
    {
        public GravityFactory()
        {
        }

        public Gravity getGravity()
        {
            return new Gravity();
        }

        public Gravity getNoGravity()
        {
            return new NoGravity();
        }

        public IGravityClient getGravityClient(Actor actor)
        {
            return new GravityClient(actor);
        }

        public virtual IGravityClient getGravityClientPlayer(Player actor)
        {
            return new GravityClientPlayer(actor);
        }

        public RayHitboxes getRayHitboxes()
        {
            return new RayHitboxes();
        }
        
        public RayHitboxes3D getRayHitboxes3D()
        {
            return new RayHitboxes3D();
        }
    }
}