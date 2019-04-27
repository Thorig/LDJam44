using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using UnityEngine;

namespace GameLib.Entity.Behaviour
{
    public interface IEntity
    {
        KeysPressed getKeysPressed();
        Gravity getGravity();
        IGravityClient getGravityClient();
        bool getRotateHorizontalMovement();
        void moveEntity(Vector3 pos);

        Transform getTransform();
        
        void setState(AbstractState state);

        void flipped(bool flip);
        bool isFlipped();
        void setMoving(bool moving);
        void switchAnimation(int animationState);
        bool getJumpedReleased();
        float getMovementSpeed();

        void playAudio(int audioType);

        IBehaviourStateFactory getBehaviourStateFactory();
        void setBehaviourStateFactory(IBehaviourStateFactory behaviourStateFactory);

        Player getPlayerComponent();
    }
}