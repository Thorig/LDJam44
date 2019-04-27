using GameLib.Entity.Behaviour;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
	public class Move : GameLib.Entity.Behaviour.State.Move
	{
		public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();
            IGravityClient gravityClient = entity.getGravityClient();
            bool moving = false;

            moving = moveEntityUp(entity, keysPressed, gravity, entity.getMovementSpeed());
            if(!moving)
            {
                moving = moveEntityDown(entity, keysPressed, gravity, entity.getMovementSpeed());
            }
            if(moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed()))
            {
                moving = true;
            }

            entity.setMoving(moving);

            if (!keysPressed.left && !keysPressed.right && !keysPressed.up && !keysPressed.down)
            {
                goIdle(entity);
            }
        }

        public bool moveEntityDown(IEntity entity, KeysPressed keysPressed, Gravity gravity, float movementSpeed)
        {
            IGravityClient gravityClient = entity.getGravityClient();
            bool moving = false;
#if USE_3D_RAYS
            RayHitboxes3D rayHitboxes = gravityClient.getRayHitboxes3D();
#else
            RayHitboxes rayHitboxes = gravityClient.getRayHitboxes();
#endif
            Vector3 angles = new Vector3(entity.getTransform().eulerAngles.x,
                                 entity.getTransform().eulerAngles.y,
                                 entity.getTransform().eulerAngles.z);
#if USE_3D_RAYS
            RayInformation3D rayInformation = gravityClient.getRayInformation3D();
#else
            RayInformation rayInformation = gravityClient.getRayInformation();
#endif
            if (!keysPressed.up && keysPressed.down)
            {
                rayInformation.checkRaysBelow(gravityClient, 0.0f, angles.z + 270.0f, layermask);
            }
            bool isWalking = !entity.getGravity().isFalling() && !entity.getGravity().Jumping;
            float frontDistance = rayHitboxes.DistanceBelow;

            if (frontDistance > rayInformation.MinimalSpaceBetweenTileBelow)
            {
                if (!keysPressed.up && keysPressed.down)
                {
                    moving = true;

                    if (!entity.getRotateHorizontalMovement())
                    {
                        Vector3 pos = entity.getTransform().position;

                        if (angles.z > 90.0f && angles.z < 270.0f)
                        {
                            pos.y -= ((frontDistance - rayInformation.MinimalSpaceBetweenTileBelow) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileBelow) : movementSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            pos.y -= ((frontDistance - rayInformation.MinimalSpaceBetweenTileBelow) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileBelow) : movementSpeed * Time.fixedDeltaTime;
                        }
                        entity.getTransform().position = pos;
                    }

                    gravity.Reset = false;
                }
            }
            return moving;
        }

        public bool moveEntityUp(IEntity entity, KeysPressed keysPressed, Gravity gravity, float movementSpeed)
        {
            IGravityClient gravityClient = entity.getGravityClient();
            bool moving = false;
#if USE_3D_RAYS
            RayHitboxes3D rayHitboxes = gravityClient.getRayHitboxes3D();
#else
            RayHitboxes rayHitboxes = gravityClient.getRayHitboxes();
#endif
            Vector3 angles = new Vector3(entity.getTransform().eulerAngles.x,
                                 entity.getTransform().eulerAngles.y,
                                 entity.getTransform().eulerAngles.z);
#if USE_3D_RAYS
            RayInformation3D rayInformation = gravityClient.getRayInformation3D();
#else
            RayInformation rayInformation = gravityClient.getRayInformation();
#endif
            if (keysPressed.up && !keysPressed.down)
            {
                rayInformation.checkRaysTop(gravityClient, 0.0f, angles.z + 90.0f, layermask);
            }
            bool isWalking = !entity.getGravity().isFalling() && !entity.getGravity().Jumping;

            float frontDistance = rayHitboxes.DistanceTop;
            
            if (frontDistance > rayInformation.MinimalSpaceBetweenTileTop)
            {
                if (keysPressed.up && !keysPressed.down)
                {
                    moving = true;

                    Vector3 pos = entity.getTransform().position;

                    pos.y += ((frontDistance - rayInformation.MinimalSpaceBetweenTileTop) < movementSpeed * Time.fixedDeltaTime) ?
                        (frontDistance - rayInformation.MinimalSpaceBetweenTileTop) : movementSpeed * Time.fixedDeltaTime;

                    entity.getTransform().position = pos;

                    gravity.Reset = false;
                }
            }
            return moving;
        }
    }
}
