using UnityEngine;

namespace GameLib.System.Gravity2D
{
    [CreateAssetMenu(fileName = "Data", menuName = "GameLib/Rayinformation3d", order = 1)]
    public class RayInformation3D : RayInformationAttributes
    {
        public float checkAttackRay(IGravityClient gameObject, Vector3 startPosRay, Vector3 endPosRay, string tag)
        {
            float result = -1.0f;
            RaycastHit attackRayResult;
            Physics.Raycast(startPosRay, endPosRay, out attackRayResult);
            if (attackRayResult.collider.tag.CompareTo(tag) == 0)
            {
                result = attackRayResult.distance;
            }

            RayHitboxes3D rayHitboxes = gameObject.getRayHitboxes3D();
            rayHitboxes.AttackRayResult = attackRayResult;
            gameObject.setRayHitboxes3D(rayHitboxes);

            return result;
        }
        
        public bool checkActiveRay(RaycastHit notMiddle, RaycastHit middle)
        {
            return (middle.distance >= notMiddle.distance);
        }

        public bool checkDistanceBetweenRays(RaycastHit notMiddle, RaycastHit middle)
        {
            return ((middle.distance - notMiddle.distance) >= minimalSpaceBetweenTileBelow);
        }

        public float checkRaysBelow(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit hitMiddleBelow;
            RaycastHit hitLeftBelow;
            RaycastHit hitRightBelow;
     
            float distanceBelow = checkRay(gameObject, posRayMiddleBelow,
                                      1000.0f, yAangle, zAngle, out hitMiddleBelow, Color.blue, layersToIgnore);

            float leftDistanceBelow = checkRay(gameObject, posRayLeftBelow,
                                          1000.0f, yAangle, zAngle, out hitLeftBelow, Color.gray, layersToIgnore);

            float rightDistanceBelow = checkRay(gameObject, posRayRightBelow,
                                           1000.0f, yAangle, zAngle, out hitRightBelow, Color.blue, layersToIgnore);

            if (checkDistanceBetweenRays(hitLeftBelow, hitMiddleBelow))
            {
                distanceBelow = leftDistanceBelow;
            }
            if (leftDistanceBelow > rightDistanceBelow)
            {
                if (checkDistanceBetweenRays(hitRightBelow, hitMiddleBelow))
                {
                    distanceBelow = rightDistanceBelow;
                }
            }

            RayHitboxes3D rayHitboxes = gameObject.getRayHitboxes3D();
            rayHitboxes.HitMiddleBelow = hitMiddleBelow;
            rayHitboxes.HitLeftBelow = hitLeftBelow;
            rayHitboxes.HitRightBelow = hitRightBelow;
            rayHitboxes.DistanceBelow = distanceBelow;
            gameObject.setRayHitboxes3D(rayHitboxes);
            return distanceBelow;
        }

        public float checkRaysTop(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit hitLeftTop;
            RaycastHit hitMiddleTop;
            RaycastHit hitRightTop;

            float distanceTop = checkRay(gameObject, posRayLeftTop,
                                    1000.0f, yAangle, zAngle, out hitLeftTop, Color.red, layersToIgnore);
            float tmpDistanceTop = checkRay(gameObject, posRayMiddleTop,
                                       1000.0f, yAangle, zAngle, out hitMiddleTop, Color.red, layersToIgnore);

            distanceTop = (distanceTop < tmpDistanceTop) ? distanceTop : tmpDistanceTop;

            tmpDistanceTop = checkRay(gameObject, posRayRightTop,
                1000.0f, yAangle, zAngle, out hitRightTop, Color.red, layersToIgnore);

            distanceTop = (distanceTop < tmpDistanceTop) ? distanceTop : tmpDistanceTop;

            RayHitboxes3D rayHitboxes = gameObject.getRayHitboxes3D();
            rayHitboxes.HitLeftTop = hitLeftTop;
            rayHitboxes.HitMiddleTop = hitMiddleTop;
            rayHitboxes.HitRightTop = hitRightTop;
            rayHitboxes.DistanceTop = distanceTop;
            gameObject.setRayHitboxes3D(rayHitboxes);

            return distanceTop;
        }

        public float checkRaysFront(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit hitTopFront;
            RaycastHit hitMiddleFront;
            RaycastHit hitBelowFront;

            float distance = (gameObject.isFlipped()) ? -1000.0f : 1000.0f;

            float distanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayTopFront,
                                      distance, yAangle, zAngle, out hitTopFront, Color.green, layersToIgnore) :
            checkRayFlipped(gameObject, posRayTopFront,
                                      distance, yAangle, zAngle, out hitTopFront, Color.green, layersToIgnore);

            float tmpDistanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayMiddleFront,
                                         distance, yAangle, zAngle, out hitMiddleFront, Color.green, layersToIgnore) :
                checkRayFlipped(gameObject, posRayMiddleFront,
                                         distance, yAangle, zAngle, out hitMiddleFront, Color.green, layersToIgnore);

            distanceFront = (distanceFront < tmpDistanceFront) ? distanceFront : tmpDistanceFront;

            RayHitboxes3D rayHitboxes = gameObject.getRayHitboxes3D();

            if (rayHitboxes.HitMiddleFront.collider != null &&
                rayHitboxes.HitMiddleFront.collider.tag.CompareTo("Slope") != 0)
            {
                tmpDistanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayBelowFront,
                    distance, yAangle, zAngle, out hitBelowFront, Color.green, layersToIgnore) :
                checkRayFlipped(gameObject, posRayBelowFront,
                    distance, yAangle, zAngle, out hitBelowFront, Color.green, layersToIgnore);

                rayHitboxes.HitBelowFront = hitBelowFront;

                if (rayHitboxes.HitBelowFront.collider != null &&
                    rayHitboxes.HitBelowFront.collider.tag.CompareTo("Slope") != 0)
                {
                    distanceFront = (distanceFront < tmpDistanceFront) ? distanceFront : tmpDistanceFront;
                }
            }
            rayHitboxes.HitMiddleFront = hitMiddleFront;
            rayHitboxes.HitTopFront = hitTopFront;
            rayHitboxes.DistanceFront = distanceFront;
            gameObject.setRayHitboxes3D(rayHitboxes);

            return distanceFront;
        }

        //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html?_ga=2.69651992.960042701.1515280206-1316818202.1483563338
        public float checkRayFlipped(IGravityClient gameObject, Vector3 positionRay, float distance, 
            float yAngle, float zAngle, out RaycastHit hit, Color color, int layersToIgnore = 0)
        {
            float result = float.MaxValue;
            Vector3 startRay = new Vector2(positionRay.x, positionRay.y);
            Vector3 endRay = new Vector2(positionRay.x + distance, positionRay.y);
            Vector3 dir = Vector3.left;
            
            Vector3 eulerVector = new Vector3(0.0f, 0.0f, 0.0f);

            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            eulerVector.z = zAngle;
            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            startRay += gameObject.getPosition();
            endRay += gameObject.getPosition();

            int layerMask = ~(1 << gameObject.getLayer() | layersToIgnore);
            if (yAngle > 0.0f)
            {
                dir = Vector3.back;
            }
            if (yAngle < 0.0f)
            {
                dir = Vector3.forward;
            }
            if (Physics.Raycast(startRay, dir, out hit, 1000000.0f, layerMask))
            {
                result = hit.distance;
            }

            Debug.DrawRay(startRay, dir, color, 0.2f, false);

            return result;
        }

        public float checkRay(IGravityClient gameObject, Vector3 positionRay, float distance, 
            float yAngle, float zAngle, out RaycastHit hit, Color color, int layersToIgnore = 0)
        {
            float result = float.MaxValue;
            Vector3 startRay = new Vector2(positionRay.x, positionRay.y);
            Vector3 endRay = new Vector2(positionRay.x + distance, positionRay.y);
            Vector3 eulerVector = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 dir = Vector3.right;
            
            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            eulerVector.z = zAngle;
            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            startRay += gameObject.getPosition();
            endRay += gameObject.getPosition();

            int layerMask = ~(1 << gameObject.getLayer() | layersToIgnore);
            if (yAngle < 0.0f)
            {
                dir = Vector3.back;
            }
            if (yAngle > 0.0f)
            {
                dir = Vector3.forward;
            }
            if (Physics.Raycast(startRay, dir, out hit, 1000000.0f, layerMask))
            {
                result = hit.distance;
            }

            Debug.DrawRay(startRay, dir, color, 0.2f, false);

            return result;
        }
    }
}
