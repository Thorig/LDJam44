#define DEBUG

using GameLib.Entity;
using UnityEngine;

namespace GameLib.System.Gravity2D
{
    [CreateAssetMenu(fileName = "Data", menuName = "GameLib/Rayinformation", order = 1)]
    public class RayInformation : RayInformationAttributes
    {
        public float checkAttackRay(IGravityClient gameObject, Vector3 startPosRay, Vector3 endPosRay, string tag)
        {
            float result = -1.0f;
            RaycastHit2D attackRayResult = Physics2D.Raycast(startPosRay, endPosRay);
            if (attackRayResult.collider.tag.CompareTo(tag) == 0)
            {
                result = attackRayResult.distance;
            }

            RayHitboxes rayHitboxes = gameObject.getRayHitboxes();
            rayHitboxes.AttackRayResult = attackRayResult;
            gameObject.setRayHitboxes(rayHitboxes);

            return result;
        }

        public bool checkActiveRay(RaycastHit2D notMiddle, RaycastHit2D middle)
        {
            return (middle.distance >= notMiddle.distance);
        }

        public bool checkDistanceBetweenRays(RaycastHit2D notMiddle, RaycastHit2D middle)
        {
            return ((middle.distance - notMiddle.distance) >= minimalSpaceBetweenTileBelow);
        }
        
        public float checkRaysBelow(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore, bool doIgnoreSlope = true)
        {
            RaycastHit2D hitMiddleBelow;
            RaycastHit2D hitLeftBelow;
            RaycastHit2D hitRightBelow;

            float distanceBelow = checkRay(gameObject, posRayMiddleBelow,
                                      1000.0f, yAangle, zAngle, out hitMiddleBelow, Color.blue, layersToIgnore + (1 << slopeLayerNumber));

            float belowTolerance = ((hitMiddleBelow.collider.tag.CompareTo("Slope") == 0) ? this.belowTolerance : 1.0f);

            bool rightRayOnSlope = checkRightRayBelowForSlope(gameObject, yAangle, zAngle, out hitRightBelow, layersToIgnore);
            bool leftRayOnSlope = checkLeftRayBelowForSlope(gameObject, yAangle, zAngle, out hitLeftBelow, layersToIgnore);
            
            float leftDistanceBelow = checkRay(gameObject, posRayLeftBelow, 1000.0f, yAangle, 
                zAngle, out hitLeftBelow, Color.gray, layersToIgnore + ((doIgnoreSlope) ? 0 : (1 << slopeLayerNumber)));

            float rightDistanceBelow = checkRay(gameObject, posRayRightBelow, 1000.0f, yAangle, 
                zAngle, out hitRightBelow, Color.green, layersToIgnore + ((doIgnoreSlope) ? 0 : (1 << slopeLayerNumber)));

            if (!doIgnoreSlope ||  
                (doIgnoreSlope && distanceBelow >= (minimalSpaceBetweenTileBelow * belowTolerance)))
            {
                if (leftDistanceBelow < distanceBelow)
                {
                    distanceBelow = leftDistanceBelow;
                }
                if (distanceBelow > rightDistanceBelow)
                {
                    distanceBelow = rightDistanceBelow;
                }
            }

            RayHitboxes rayHitboxes = gameObject.getRayHitboxes();
            rayHitboxes.HitMiddleBelow = hitMiddleBelow;
            rayHitboxes.HitLeftBelow = hitLeftBelow;
            rayHitboxes.HitRightBelow = hitRightBelow;
            rayHitboxes.DistanceBelow = distanceBelow;
            gameObject.setRayHitboxes(rayHitboxes);

            return distanceBelow;
        }

        public bool checkRightRayBelowForSlope(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit2D hitRightBelow;

            checkRay(gameObject, posRayRightBelow, 1000.0f, yAangle, zAngle, out hitRightBelow, Color.cyan, layersToIgnore + (1 << slopeLayerNumber));

            return hitRightBelow.collider.tag.CompareTo("Slope") == 0;
        }

        public bool checkLeftRayBelowForSlope(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit2D hitLeftBelow;

            checkRay(gameObject, PosRayLeftBelow, 1000.0f, yAangle, zAngle, out hitLeftBelow, Color.gray, layersToIgnore + (1 << slopeLayerNumber));

            return hitLeftBelow.collider.tag.CompareTo("Slope") == 0;
        }

        public bool checkRightRayBelowForSlope(IGravityClient gameObject, float yAangle, float zAngle, out RaycastHit2D hitRightBelow, int layersToIgnore = 0)
        {
            checkRay(gameObject, posRayRightBelow, 1000.0f, yAangle, zAngle, out hitRightBelow, Color.cyan, layersToIgnore + (1 << slopeLayerNumber));

            return hitRightBelow.collider.tag.CompareTo("Slope") == 0;
        }

        public bool checkLeftRayBelowForSlope(IGravityClient gameObject, float yAangle, float zAngle, out RaycastHit2D hitLeftBelow, int layersToIgnore = 0)
        {
            checkRay(gameObject, PosRayLeftBelow, 1000.0f, yAangle, zAngle, out hitLeftBelow, Color.gray, layersToIgnore + (1 << slopeLayerNumber));

            return hitLeftBelow.collider.tag.CompareTo("Slope") == 0;
        }

        public float checkRaysTop(IGravityClient gameObject, float yAangle, float zAngle, int layersToIgnore = 0)
        {
            RaycastHit2D hitLeftTop;
            RaycastHit2D hitMiddleTop;
            RaycastHit2D hitRightTop;

            float distanceTop = checkRay(gameObject, posRayLeftTop,
                                    1000.0f, yAangle, zAngle, out hitLeftTop, Color.red, layersToIgnore);
            float tmpDistanceTop = checkRay(gameObject, posRayMiddleTop,
                                       1000.0f, yAangle, zAngle, out hitMiddleTop, Color.red, layersToIgnore);

            distanceTop = (distanceTop < tmpDistanceTop) ? distanceTop : tmpDistanceTop;

            tmpDistanceTop = checkRay(gameObject, posRayRightTop,
                1000.0f, yAangle, zAngle, out hitRightTop, Color.red, layersToIgnore);

            distanceTop = (distanceTop < tmpDistanceTop) ? distanceTop : tmpDistanceTop;

            RayHitboxes rayHitboxes = gameObject.getRayHitboxes();
            rayHitboxes.HitLeftTop = hitLeftTop;
            rayHitboxes.HitMiddleTop = hitMiddleTop;
            rayHitboxes.HitRightTop = hitRightTop;
            rayHitboxes.DistanceTop = distanceTop;
            gameObject.setRayHitboxes(rayHitboxes);

            return distanceTop;
        }

        public float checkRaysFront(IGravityClient gameObject, float yAangle, float zAngle, int layerMask = 0, bool ignoreSlope = false)
        {
            RaycastHit2D hitTopFront;
            RaycastHit2D hitMiddleFront;
            RaycastHit2D hitBelowFront;

            float distance = (gameObject.isFlipped()) ? -1000.0f : 1000.0f;

            float distanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayTopFront,
                                      distance, yAangle, zAngle, out hitTopFront, Color.green, layerMask + ((ignoreSlope)? 0 : (1 << slopeLayerNumber))) :
            checkRayFlipped(gameObject, posRayTopFront,
                                      distance, yAangle, zAngle, out hitTopFront, Color.green, layerMask + ((ignoreSlope) ? 0 : (1 << slopeLayerNumber)));

            float tmpDistanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayMiddleFront,
                                         distance, yAangle, zAngle, out hitMiddleFront, Color.green, layerMask + ((ignoreSlope) ? 0 : (1 << slopeLayerNumber))) :
                checkRayFlipped(gameObject, posRayMiddleFront,
                                         distance, yAangle, zAngle, out hitMiddleFront, Color.green, layerMask + ((ignoreSlope) ? 0 : (1 << slopeLayerNumber)));

            distanceFront = (distanceFront < tmpDistanceFront) ? distanceFront : tmpDistanceFront;

            RayHitboxes rayHitboxes = gameObject.getRayHitboxes();

            if (rayHitboxes.HitMiddleFront.collider != null &&
                rayHitboxes.HitMiddleFront.collider.tag.CompareTo("Slope") != 0)
            {
                tmpDistanceFront = (!gameObject.isFlipped()) ? checkRay(gameObject, posRayBelowFront,
                    distance, yAangle, zAngle, out hitBelowFront, Color.green, layerMask) :
                checkRayFlipped(gameObject, posRayBelowFront,
                    distance, yAangle, zAngle, out hitBelowFront, Color.green, layerMask);

                rayHitboxes.HitBelowFront = hitBelowFront;

                if (rayHitboxes.HitBelowFront.collider != null &&
                     rayHitboxes.HitBelowFront.collider.tag.CompareTo("Slope") != 0)
                {
                    distanceFront = (distanceFront < tmpDistanceFront) ? distanceFront : tmpDistanceFront;
                }
            }
            else
            {
                hitBelowFront = new RaycastHit2D();
                hitBelowFront.distance = -1.0f;
                rayHitboxes.HitBelowFront = hitBelowFront;
            }
            rayHitboxes.HitMiddleFront = hitMiddleFront;
            rayHitboxes.HitTopFront = hitTopFront;
            rayHitboxes.DistanceFront = distanceFront;
            gameObject.setRayHitboxes(rayHitboxes);

            return distanceFront;
        }

        public virtual float checkRayFlipped(IGravityClient gameObject, Vector3 positionRay, float distance, float yAngle, float zAngle, out RaycastHit2D hit, Color color, int layersToIgnore = 0)
        {
            float result = float.MaxValue;
            Vector3 startRay = new Vector2(positionRay.x, positionRay.y);
            Vector3 endRay = new Vector2(positionRay.x + distance, positionRay.y);
            Vector2 dir = Vector2.left;

            Vector3 eulerVector = new Vector3(0.0f, 0.0f, 0.0f);

            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            eulerVector.y = yAngle;
            eulerVector.z = zAngle;
            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            startRay += gameObject.getPosition();
            endRay += gameObject.getPosition();

            int layerMask = layersToIgnore;

            hit = Physics2D.Raycast(startRay, dir, 100000.0f, layerMask);

            if (hit.collider != null)
            {
                result = hit.distance;
              /*  Debug.DrawLine(startRay, hit.point, color);
            }
            else
            {
                Debug.DrawLine(startRay, endRay, color);
            */}
            
            return result;
        }

        public virtual float checkRay(IGravityClient gameObject, Vector3 positionRay, float distance, float yAngle, float zAngle, out RaycastHit2D hit, Color color, int layersToIgnore = 0)
        {
            float result = float.MaxValue;
            Vector3 startRay = new Vector2(positionRay.x, positionRay.y);
            Vector3 endRay = new Vector2(positionRay.x + distance, positionRay.y);
            Vector2 dir = Vector2.right;

            Vector3 eulerVector = new Vector3(0.0f, 0.0f, 0.0f);

            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            eulerVector.y = yAngle;
            eulerVector.z = zAngle;
            startRay = Quaternion.Euler(eulerVector) * (startRay);
            endRay = Quaternion.Euler(eulerVector) * (endRay);
            dir = Quaternion.Euler(eulerVector) * (dir);

            startRay += gameObject.getPosition();
            endRay += gameObject.getPosition();

            int layerMask = layersToIgnore;

            hit = Physics2D.Raycast(startRay, dir, 1000000.0f, layerMask);
            
            if (hit.collider != null)
            {
                result = hit.distance;
              Debug.DrawLine(startRay, hit.point, color);
            }
            
            else
            {
                Debug.DrawLine(startRay, endRay, color);
            }
            
            return result;
        }
    }
}