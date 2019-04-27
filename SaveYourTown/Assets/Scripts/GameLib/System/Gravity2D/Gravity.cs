//#undef USE_3D_RAYS
//#define USE_3D_RAYS
using UnityEngine;
using System;
using GameLib.Level;

namespace GameLib.System.Gravity2D
{
    [Serializable]
    public class Gravity
    {
#region attributes and props
        //Game physics
        [SerializeField]
        protected float gravity = 70.0f;

        public float _Gravity { get { return gravity; } set { gravity = value; } }

        [SerializeField]
        protected float y_velocity_reset = -0.50f;

        public float Y_velocity_reset { get { return y_velocity_reset; } set { y_velocity_reset = value; } }

        [SerializeField]
        protected float y_velocity_jump_reset = 25.0f;

        public float Y_velocity_jump_reset { get { return y_velocity_jump_reset; } set { y_velocity_jump_reset = value; } }

        [SerializeField]
        protected float y_velocity = 0.0f;

        public float Y_velocity { get { return y_velocity; } set { y_velocity = value; } }

        [SerializeField]
        protected float yFallDistance = 0.0f;

        public float YFallDistance { get { return yFallDistance; } set { yFallDistance = value; } }

        [SerializeField]
        protected float multiplyYVelocityUp = -1.0f;

        public float MultiplyYVelocityUp { get { return multiplyYVelocityUp; } set { multiplyYVelocityUp = value; } }

        [SerializeField]
        protected float multiplyYVelocityDown = -1.0f;

        public float MultiplyYVelocityDown { get { return multiplyYVelocityDown; } set { multiplyYVelocityDown = value; } }


        [SerializeField]
        protected Vector3 distanceTravelled;

        public Vector3 DistanceTravelled { get { return distanceTravelled; } set { distanceTravelled = value; } }

        [SerializeField]
        protected bool slope = false;

        public bool Slope { get { return slope; } set { slope = value; } }

        [SerializeField]
        protected bool jumping;

        public bool Jumping { get { return jumping; } set { jumping = value; } }

        [SerializeField]
        protected bool reset;

        [SerializeField]
        protected int layermask;

        public bool Reset { get { return reset; } set { reset = value; } }
#endregion

        public virtual void init(float gravity = 70.0f, float y_velocity_jump_reset = 25.0f)
        {
            this.gravity = gravity;
            y_velocity_reset = 0.0f;
            this.y_velocity_jump_reset = y_velocity_jump_reset;
            setLayerMask();
        }

        protected virtual void setLayerMask()
        {
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile") | 1 << layersLookup.giveLayerNumber("Default"));
        }

        public virtual bool checkObjectHitsPlatform(float yModifier, IGravityClient object_)
        {
#if USE_3D_RAYS
            RayInformation3D rayInformation = object_.getRayInformation3D();
            RayHitboxes3D rayHitboxes = object_.getRayHitboxes3D();
#else
            RayInformation rayInformation = object_.getRayInformation();
            RayHitboxes rayHitboxes = object_.getRayHitboxes();
#endif
            rayInformation.checkRaysTop(object_, 0.0f, object_.getTransform().eulerAngles.z + 90.0f, layermask);
            bool objectHitTop =  (rayHitboxes.DistanceTop <
                rayInformation.MinimalSpaceBetweenTileTop) ? true :
                 (yModifier > (rayHitboxes.DistanceTop -
                    rayInformation.MinimalSpaceBetweenTileTop)) ? true : false;
            
            if (objectHitTop)
            {
                float correction = -(rayHitboxes.DistanceTop - 
                    rayInformation.MinimalSpaceBetweenTileTop);
                moveEntity(object_, correction);
                checkObjectHitsFloor(object_);
            }
            return objectHitTop;
        }

        public virtual bool checkObjectHitsFloor(IGravityClient object_)
        {
#if USE_3D_RAYS
            RayInformation3D rayInformation = object_.getRayInformation3D();
            RayHitboxes3D rayHitboxes = object_.getRayHitboxes3D();
#else
            RayInformation rayInformation = object_.getRayInformation();
            RayHitboxes rayHitboxes = object_.getRayHitboxes();
#endif
            rayInformation.checkRaysBelow(object_, 0.0f, 
                object_.getTransform().eulerAngles.z + 270.0f,
                layermask);
            float correction = 0.0f;
            float minimalSpaceBetweenTileBelow = rayInformation.MinimalSpaceBetweenTileBelow;

            bool objectOnFloor = (rayHitboxes.DistanceBelow <
                minimalSpaceBetweenTileBelow * rayInformation.BelowTolerance) ? true :
                 ((multiplyYVelocityDown * yFallDistance) > (rayHitboxes.DistanceBelow -
                    minimalSpaceBetweenTileBelow)) ? true : false;
  
            if (objectOnFloor)
            {
                correction = -(rayHitboxes.DistanceBelow -
                    rayInformation.MinimalSpaceBetweenTileBelow);
                moveEntity(object_, correction);
            }
            
            return objectOnFloor;
        }

        public virtual void _reset(IGravityClient object_)
        {
            y_velocity = (jumping) ? object_.getJumpForce() : y_velocity_reset;
        }

        public virtual bool isFalling()
        {
            return (yFallDistance < 0.0f);
        }

        public virtual bool checkForCollision(IGravityClient object_)
        {
            return ((yFallDistance > 0.0f) ? checkObjectHitsPlatform(yFallDistance, object_) : checkObjectHitsFloor(object_));
        }

        public virtual bool isNotFalling()
        {
            return (yFallDistance > 0.0f);
        }

        public virtual bool isStanding()
        {
            return (yFallDistance == 0.0f);
        }

        public virtual void moveEntity(IGravityClient object_, float moveDistance)
        {
            Vector3 newPosition = object_.getTransform().position;
            
            Vector3 distance = Quaternion.Euler(
                new Vector3(object_.getTransform().eulerAngles.x, 
                object_.getTransform().eulerAngles.y,
                object_.getTransform().eulerAngles.z)) * new Vector3(0.0f, moveDistance, 0.0f);
            distanceTravelled += distance;
            newPosition += distance;
            object_.getTransform().position = newPosition;
        }

        // Update is called once per frame
        public virtual void update(IGravityClient object_)
        {
            if (!reset)
            {
                y_velocity = y_velocity + gravity * Time.deltaTime * (isFalling()? multiplyYVelocityDown : multiplyYVelocityUp);
                yFallDistance = y_velocity * Time.deltaTime + 0.5f * gravity * (Time.deltaTime * Time.deltaTime);
                
                if (!checkForCollision(object_))
                {
                    moveEntity(object_, yFallDistance);
                }
                else
                {
                    if (isNotFalling())
                    {
                        y_velocity = 0.00f;
                    }
                    else
                    {
                        reset = true;
                        yFallDistance = 0.0f;
                        distanceTravelled.x = 0.0f;
                        distanceTravelled.y = 0.0f;
                        distanceTravelled.z = 0.0f;
                        y_velocity = 0.00f;
                    }
                }
            }
            else
            {
                _reset(object_);
            }
        }
    }
}
