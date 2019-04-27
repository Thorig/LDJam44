//#undef USE_3D_RAYS
//#define USE_3D_RAYS
using GameLib.Entity.Animation;
using GameLib.Level;
using GameLib.System.Audio;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using System;
using UnityEngine;

namespace GameLib.Entity.Behaviour.State
{
    public class Move : AbstractState
    {
        protected bool ignoreslopes;

        public override void animationMessage(int messageId, IEntity entity)
        {
          //  throw new NotImplementedException();
        }

        public override void init(IEntity entity)
        {
            ignoreslopes = true;            
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));
            entity.setMoving(true);
            switchAnimation(AnimationAttributes.ANIMATION_WALK, entity);
        }

        public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();
            IGravityClient gravityClient = entity.getGravityClient();

#if USE_3D_RAYS
            gravityClient.getRayInformation3D().checkRaysFront(gravityClient, 0.0f, entity.getTransform().eulerAngles.z + 0.0f, entity.getGravityClient().getLayerToIgnore());
#else
            gravityClient.getRayInformation().checkRaysFront(gravityClient, 0.0f, entity.getTransform().eulerAngles.z + 0.0f, layermask);
#endif
            if (keysPressed.jump && (gravity.isStanding() ||
#if USE_3D_RAYS
                (gravityClient.getRayHitboxes().DistanceBelow <= (gravityClient.getRayInformation3D().MinimalSpaceBetweenTileBelow))) &&
#else
                 (gravityClient.getRayHitboxes().DistanceBelow <= (gravityClient.getRayInformation().MinimalSpaceBetweenTileBelow))) &&
#endif
                 entity.getJumpedReleased())
            {
                IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
                entity.setState(behaviourStateFactory.getJumpUpState(entity));
                moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());
            }
            else
            {
                moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());

                if (!keysPressed.left && !keysPressed.right)
                {
                    goIdle(entity);
                }
                else if (gravity.isFalling())
                {
                    fallState(entity, gravityClient);
                }
            }
        }

        protected virtual void fallState(IEntity entity, IGravityClient gravityClient)
        {
            if (
#if USE_3D_RAYS
                        (gravityClient.getRayHitboxes3D().HitLeftBelow.collider != null && gravityClient.getRayHitboxes3D().HitLeftBelow.collider.tag.CompareTo("Slope") != 0 &&
                       gravityClient.getRayHitboxes3D().HitMiddleBelow.collider != null && gravityClient.getRayHitboxes3D().HitMiddleBelow.collider.tag.CompareTo("Slope") != 0) ||
                            (gravityClient.getRayHitboxes3D().HitLeftBelow.distance > (2.0f * gravityClient.getRayInformation3D().MinimalSpaceBetweenTileBelow))
#else
                        (gravityClient.getRayHitboxes().HitLeftBelow.collider != null && gravityClient.getRayHitboxes().HitLeftBelow.collider.tag.CompareTo("Slope") != 0 &&
                      gravityClient.getRayHitboxes().HitMiddleBelow.collider != null && gravityClient.getRayHitboxes().HitMiddleBelow.collider.tag.CompareTo("Slope") != 0) ||
                           (gravityClient.getRayHitboxes().HitLeftBelow.distance > (2.0f * gravityClient.getRayInformation().MinimalSpaceBetweenTileBelow))
#endif
                        )
            {
                IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
                entity.setState(behaviourStateFactory.getFallState(entity));
            }
        }

        public virtual bool moveEntity(IEntity entity, KeysPressed keysPressed, Gravity gravity, float movementSpeed)
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
            if ((gravityClient.isFlipped() && keysPressed.right && !keysPressed.left) ||
                (!gravityClient.isFlipped() && keysPressed.left && !keysPressed.right))
            {
                if (gravityClient.isFlipped() && keysPressed.right)
                {
                    entity.flipped(false);
                }
                else
                {
                    entity.flipped(true);
                }
                rayInformation.checkRaysFront(gravityClient, 0.0f, angles.z + 0.0f, layermask, false);
            }
            bool isWalking = !entity.getGravity().isFalling() && !entity.getGravity().Jumping;
            float frontDistance = (rayHitboxes.HitMiddleFront.collider != null && rayHitboxes.HitMiddleFront.collider.CompareTag("Slope")) ? rayHitboxes.HitTopFront.distance : rayHitboxes.HitMiddleFront.distance;

            frontDistance = (isWalking && (rayHitboxes.HitMiddleBelow.collider.CompareTag("Slope") || rayHitboxes.HitBelowFront.distance < 0)) ? frontDistance : rayHitboxes.DistanceFront;

            if ( frontDistance > rayInformation.MinimalSpaceBetweenTileFront)
            {
                if (keysPressed.right && !keysPressed.left)
                {
                    moving = true;

                    if (entity.getRotateHorizontalMovement())
                    {
                        Vector3 pos = new Vector3();

                        if (angles.z > 90.0f && angles.z < 270.0f)
                        {
                            pos.x = ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : movementSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            pos.x = ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : movementSpeed * Time.fixedDeltaTime;
                        }

                        entity.moveEntity(pos);
                    }
                    else
                    {
                        Vector3 pos = entity.getTransform().position;

                        if (angles.z > 90.0f && angles.z < 270.0f)
                        {
                            pos.x += ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : movementSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            pos.x += ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : movementSpeed * Time.fixedDeltaTime;
                        }
                        entity.getTransform().position = pos;
                    }

                    gravity.Reset = false;
                }
                else if (keysPressed.left && !keysPressed.right)
                {
                    moving = true;

                    if (entity.getRotateHorizontalMovement())
                    {
                        Vector3 pos = new Vector3();
                        if (angles.z > 90.0f && angles.z < 270.0f)
                        {
                            pos.x = ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                -(frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : -movementSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            pos.x = ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                -(frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : -movementSpeed * Time.fixedDeltaTime;
                        }
                        entity.moveEntity(pos);
                    }
                    else
                    {
                        Vector3 pos = entity.getTransform().position;
                        if (angles.z > 90.0f && angles.z < 270.0f)
                        {
                            pos.x += ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                -(frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : -movementSpeed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            pos.x -= ((frontDistance - rayInformation.MinimalSpaceBetweenTileFront) < movementSpeed * Time.fixedDeltaTime) ?
                                (frontDistance - rayInformation.MinimalSpaceBetweenTileFront) : movementSpeed * Time.fixedDeltaTime;
                        }
                        entity.getTransform().position = pos;
                    }

                    gravity.Reset = false;
                }
            }
            entity.setMoving(moving);
            return moving;
        }

        public virtual void goIdle(IEntity entity)
        {
            IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
            Gravity gravity = entity.getGravity();
            if (!gravity.isStanding())
            {
                entity.setState(behaviourStateFactory.getFallState(entity));
            }
            else
            {
                entity.setState(behaviourStateFactory.getIdleState(entity));
            }
        }
    }
}