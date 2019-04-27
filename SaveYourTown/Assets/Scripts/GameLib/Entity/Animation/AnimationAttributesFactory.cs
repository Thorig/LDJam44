using System;
using UnityEngine;

namespace GameLib.Entity.Animation
{
    public class AnimationAttributesFactory : IAnimationAttributesFactory
    {
        private bool showDebugMessage = false;

        protected virtual AnimationAttributes getAnimationAttributes()
        {
            return new AnimationAttributes();
        }

        public virtual AnimationAttributes getAnimationAttributes(Player player, bool showDebugMessage = true)
        {
            this.showDebugMessage = showDebugMessage;

            AnimationAttributes animationAttribute = getAnimationAttributes();

            setAnimator(animationAttribute, player);

            return animationAttribute;
        }

        public virtual AnimationAttributes getAnimationAttributes(Player player)
        {
            showDebugMessage = false;

            AnimationAttributes animationAttribute = getAnimationAttributes();

            setAnimator(animationAttribute, player);

            return animationAttribute;
        }

        protected virtual void setAnimator(AnimationAttributes animationAttribute, Player player)
        {
            try
            {
                if (player.GetComponent<Animator>() != null)
                {
                    animationAttribute.Animator = player.GetComponent<Animator>();
                }
                else if (player.transform.GetChild(0).GetComponent<Animator>() != null)
                {
                    animationAttribute.Animator = player.transform.GetChild(0).GetComponent<Animator>();
                }
            }
            catch (Exception ex)
            {
                if (showDebugMessage)
                {
                    Debug.Log("TODO: add an Animator to the player!!!");
                    Debug.Log(ex.Message);
                }
            }
        }
    }
}