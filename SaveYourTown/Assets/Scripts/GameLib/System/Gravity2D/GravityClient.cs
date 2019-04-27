//#undef USE_3D_RAYS
//#define USE_3D_RAYS

using UnityEngine;
using GameLib.Entity;

namespace GameLib.System.Gravity2D
{
    public class GravityClient : IGravityClient
    {
        protected Actor actor;

        public GravityClient(Actor actor)
        {
            this.actor = actor;
        }

        public virtual RayInformation getRayInformation()
        {
#if USE_3D_RAYS
            return null;
#else
            return actor.RayInformation;
#endif
        }

        public virtual RayInformation3D getRayInformation3D()
        {
#if USE_3D_RAYS
            return actor.RayInformation3D;
#else
            return null;
#endif
        }

        public float getJumpForce()
        {
            return actor.JumpForce;
        }

        public int getLayer()
        {
            return actor.gameObject.layer;
        }

        public Transform getTransform()
        {
            return actor.transform;
        }

        public virtual Vector3 getPosition()
        {
            return actor.transform.position;
        }

        public bool isFlipped()
        {
            return actor.SpriteRenderer.flipX;
        }

        public void moveActor(Vector3 positionNormalGravity)
        {
            actor.moveActor(positionNormalGravity);
        }
        public RayHitboxes3D getRayHitboxes3D()
        {
#if USE_3D_RAYS
            return actor.RayHitboxes3D;
#else
            return null;
#endif
        }

        public void setRayHitboxes3D(RayHitboxes3D rayHitboxes)
        {
#if USE_3D_RAYS
            actor.RayHitboxes3D = rayHitboxes;
#endif
        }

        public RayHitboxes getRayHitboxes()
        {
#if USE_3D_RAYS
            return null;
#else
            return actor.RayHitboxes;
#endif
        }

        public void setRayHitboxes(RayHitboxes rayHitboxes)
        {
#if USE_3D_RAYS
#else
            actor.RayHitboxes = rayHitboxes;
#endif
        }
    }
}