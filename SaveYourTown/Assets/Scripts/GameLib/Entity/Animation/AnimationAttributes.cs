using System;
using UnityEngine;

namespace GameLib.Entity.Animation
{
    [Serializable]
    public class AnimationAttributes
    {
        public static int ANIMATION_IDLE = 0;
        public static int ANIMATION_WALK = 1;
        public static int ANIMATION_JUMPUP = 2;
        public static int ANIMATION_FALL = 3;
        public static int ANIMATION_HITTED = 4;
        public static int ANIMATION_ATTACK = 5;
        public static int ANIMATION_DEATH = 6;

        public static int ANIMATION_JUMP_KICK = 7;
        public static int ANIMATION_KICK_ATTACK = 8;
        public static int ANIMATION_PUNCH_002 = 9;
        public static int ANIMATION_PUNCH_003 = 10;
        public static int ANIMATION_KNOCKED_DOWN = 11;
        public static int ANIMATION_GET_UP = 12;

        public static int ANIMATION_WALK_BACKWARDS = 13;

        public static int ANIMATION_RUN = 14;

        public static int ANIMATION_MESSAGE_FINISHED = 0;
        public static int ANIMATION_MESSAGE_CAN_DO_DAMAGE = 1;
        public static int ANIMATION_MESSAGE_DONE_DO_DAMAGE = 2;
        public static int ANIMATION_MESSAGE_FIX_SHADOW = 3;

        [SerializeField]
        protected int lastAttackAnimation = -1;

        public int LastAttackAnimation
        {
            get { return lastAttackAnimation; }
            set { lastAttackAnimation = value; }
        }

        [SerializeField]
        protected float comboCoolDown = 0.3f;

        public float ComboCoolDown
        {
            get { return comboCoolDown; }
            set { comboCoolDown = value; }
        }

        [SerializeField]
        protected Animator animator;

        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }

        [SerializeField]
        protected int animationState = ANIMATION_IDLE;

        public int AnimationState
        {
            get { return animationState; }
            set { animationState = value; }
        }

        [SerializeField]
        protected float attackAnimationCooldown = 0.1f;

        public float AttackAnimationCooldown
        {
            get { return attackAnimationCooldown; }
            set { attackAnimationCooldown = value; }
        }

        public void switchAnimation(int animationState)
        {
            if (animator != null &&
                this.animationState != ANIMATION_DEATH &&
                this.animationState != animationState)
            {
                this.animationState = animationState;
                animator.SetInteger("State", animationState);
            }
        }

        public virtual void doCooldown(float time)
        {
            attackAnimationCooldown -= time;

            comboCoolDown -= time;

            if (comboCoolDown <= 0.0f)
            {
                lastAttackAnimation = -1;
            }
        }

        public virtual void setCooldown(float time = 0.1f)
        {
            attackAnimationCooldown = time;
            comboCoolDown = 0.750f;
        }
    }
}