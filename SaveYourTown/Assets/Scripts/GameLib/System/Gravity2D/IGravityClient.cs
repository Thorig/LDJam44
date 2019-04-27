using UnityEngine;

namespace GameLib.System.Gravity2D
{
    public interface IGravityClient
    {
        RayInformation getRayInformation();
        RayInformation3D getRayInformation3D();

        float getJumpForce();
        int getLayer();
        void moveActor(Vector3 positionNormalGravity);
        bool isFlipped();
        Transform getTransform();
        Vector3 getPosition();

        RayHitboxes3D getRayHitboxes3D();
        void setRayHitboxes3D(RayHitboxes3D rayHitboxes);
        RayHitboxes getRayHitboxes();
        void setRayHitboxes(RayHitboxes rayHitboxes);
    }
}