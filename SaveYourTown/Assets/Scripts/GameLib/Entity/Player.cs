//#define DEBUG
//#define USE_3D_RAYS

using GameLib.Entity.Animation;
using GameLib.Entity.Behaviour;
using GameLib.System.Audio;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using UnityEngine;

namespace GameLib.Entity
{
    public class Player : Actor
    {
        [SerializeField]
        protected RayInformation3D rayInformationFlippedX3D;

        public RayInformation3D RayInformationFlippedX3D
        {
            get { return rayInformationFlippedX3D; }
            set { rayInformationFlippedX3D = value; }
        }

        [SerializeField]
        protected RayInformation rayInformationFlippedX;

        public RayInformation RayInformationFlippedX
        {
            get { return rayInformationFlippedX; }
            set { rayInformationFlippedX = value; }
        }

        [SerializeField]
        protected Collider2D collider2DFlippedX;

        public Collider2D Collider2DFlippedX
        {
            get { return collider2DFlippedX; }
            set { collider2DFlippedX = value; }
        }

        protected IEntity entity;

        public IEntity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        protected IController controller;

        [SerializeField]
        protected KeyboardSetting keys;

        [SerializeField]
        protected KeysPressed keysPressed;

        public KeysPressed KeysPressed
        {
            get { return keysPressed; }
            set { keysPressed = value; }
        }

        protected bool jumpPressed;

        protected AbstractState state;

        public AbstractState State
        {
            get { return state; }
            set { state = value; }
        }

        [SerializeField]
        protected float movementSpeed;

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        protected bool isMoving = false;

        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = value; }
        }

        protected float stepSpeed;

        [SerializeField]
        protected AnimationAttributes animationAttributes;

        public AnimationAttributes AnimationAttributes
        {
            get { return animationAttributes; }
            set { animationAttributes = value; }
        }

        [SerializeField]
        protected bool jumpedReleased = true;

        public bool JumpedReleased
        {
            get { return jumpedReleased; }
            set { jumpedReleased = value; }
        }

        [SerializeField]
        protected AudioAttributes audioAttributes;

        public AudioAttributes AudioAttributes
        {
            get { return audioAttributes; }
            set { audioAttributes = value; }
        }

        protected bool hittedOnTheRight = false;
        protected bool moveRightOnHit = false;
        protected bool hittedEnemey = false;

        protected float healthPoints = 1000.0f;

        protected int balance = 3;

        [SerializeField]
        protected int activeFrame = 1;
        protected int frameCounter = 1;

        protected override void init()
        {
            if (collider2DFlippedX != null && collider2D == null)
            {
                throw new UnityException("Only collider2DFlippedX is referenced instead of only collider2D.");
            }

            base.init();

            IGravityFactory gravityFactory = new GravityFactory();
            gravityClient = gravityFactory.getGravityClientPlayer(this);

            setEntity();

            controller = InputFactory.getInstance().getKeyboardInput(keys);

            gravity._reset(gravityClient);

            IAnimationAttributesFactory animationAttribute = new AnimationAttributesFactory();

            animationAttributes = animationAttribute.getAnimationAttributes(this);
        }

        protected virtual void setEntity()
        {
            setEntity(new BehaviourStateFactory());
        }

        protected void setEntity(IBehaviourStateFactory behaviourStateFactory)
        {
            entity = behaviourStateFactory.getBehaviourStateForPlayer(this);
            entity.setBehaviourStateFactory(behaviourStateFactory);
            state = behaviourStateFactory.getIdleState(entity);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            keys = null;
        }

        public virtual void Update()
        {
            keysPressed = controller.updateInput();
        }

        public override void LateUpdate()
        {
            if (frameCounter % activeFrame == 0)
            {
                
#if DEBUG
                {
#if USE_3D_RAYS
                    gravityClient.getRayInformation3D().checkRaysTop(gravityClient, 0.0f, transform.eulerAngles.z + 90.0f);
                    gravityClient.getRayInformation3D().checkRaysBelow(gravityClient, 0.0f, transform.eulerAngles.z + 270.0f);
                    gravityClient.getRayInformation3D().checkRaysFront(gravityClient, 0.0f, transform.eulerAngles.z + 0.0f);
#else
                    gravityClient.getRayInformation().checkRaysTop(gravityClient, 0.0f, transform.eulerAngles.z + 90.0f, layermask);
                    gravityClient.getRayInformation().checkRaysBelow(gravityClient, 0.0f, transform.eulerAngles.z + 270.0f, layermask, true);
                    gravityClient.getRayInformation().checkRaysFront(gravityClient, 0.0f, transform.eulerAngles.z + 0.0f, layermask);
#endif
                }

#endif
                if (!keysPressed.jump)
                {
                    jumpedReleased = true;
                }
                
                state.update(entity);

                base.LateUpdate();

                animationAttributes.AttackAnimationCooldown -= Time.fixedDeltaTime;
            }
            frameCounter++;
        }

        public virtual void recievedDamage(float damage)
        {
            state = entity.getBehaviourStateFactory().getHitState(entity);
        }

        public virtual void switchAnimation(int animationState)
        {
            animationAttributes.switchAnimation(animationState);

            if (animationState == AnimationAttributes.ANIMATION_JUMPUP)
            {
                jumpedReleased = false;
            }
        }

        public virtual void animationDone(int messageId)
        {
            state.animationMessage(messageId, entity);
        }

        public virtual void animationEffect(int messageId)
        {
            throw new UnityException("Methode animationEffect is not implemented!");
        }

        public IEntity getEntity()
        {
            return entity;
        }

        public void playAudio(int audioId)
        {
            AudioAttributes.playAudio(audioId);
        }
    }
}