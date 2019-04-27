using UnityEngine;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;

namespace GameLib.Entity.Behaviour
{
    public class Entity : IEntity
    {
        protected Player player;
        protected IBehaviourStateFactory behaviourStateFactory;

        public Entity(Player entity)
        {
            player = entity;
        }

        public virtual void flipped(bool flip)
        {
            player.SpriteRenderer.flipX = flip;
        }

        public virtual bool isFlipped()
        {
            return player.SpriteRenderer.flipX;
        }

        public Gravity getGravity()
        {
            return player.Gravity;
        }

        public IGravityClient getGravityClient()
        {
            return player.GravityClient;
        }

        public bool getJumpedReleased()
        {
            return player.JumpedReleased;
        }

        public KeysPressed getKeysPressed()
        {
            return player.KeysPressed;
        }

        public float getMovementSpeed()
        {
            return player.MovementSpeed;
        }

        public bool getRotateHorizontalMovement()
        {
            return player.RotateHorizontalMovement;
        }

        public Transform getTransform()
        {
            return player.transform;
        }

        public void moveEntity(Vector3 pos)
        {
            player.moveActor(pos);
        }

        public void playAudio(int audioType)
        {
            if (player.SpriteRenderer.isVisible)
            {
                player.AudioAttributes.stopAudio();
                player.AudioAttributes.playAudio(audioType);
            }
        }

        public void setState(AbstractState state)
        {
            player.State = state;
        }

        public void setMoving(bool moving)
        {
            player.IsMoving = moving;
        }

        public void switchAnimation(int animationState)
        {
            player.switchAnimation(animationState);
        }

        public IBehaviourStateFactory getBehaviourStateFactory()
        {
            return behaviourStateFactory;
        }

        public void setBehaviourStateFactory(IBehaviourStateFactory behaviourStateFactory)
        {
            this.behaviourStateFactory = behaviourStateFactory;
        }

        public Player getPlayerComponent()
        {
            return player;
        }
    }
}