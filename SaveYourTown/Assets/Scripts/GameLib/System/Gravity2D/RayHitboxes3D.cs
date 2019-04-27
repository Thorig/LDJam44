using UnityEngine;

namespace GameLib.System.Gravity2D
{
    public class RayHitboxes3D : RayHitBoxAttributes
    {
        protected RaycastHit hitTopFront;

        public RaycastHit HitTopFront { get { return hitTopFront; } set { hitTopFront = value; } }

        protected RaycastHit hitMiddleFront;

        public RaycastHit HitMiddleFront { get { return hitMiddleFront; } set { hitMiddleFront = value; } }

        protected RaycastHit hitBelowFront;

        public RaycastHit HitBelowFront { get { return hitBelowFront; } set { hitBelowFront = value; } }


        protected RaycastHit hitLeftTop;

        public RaycastHit HitLeftTop { get { return hitLeftTop; } set { hitLeftTop = value; } }

        protected RaycastHit hitMiddleTop;

        public RaycastHit HitMiddleTop { get { return hitMiddleTop; } set { hitMiddleTop = value; } }

        protected RaycastHit hitRightTop;

        public RaycastHit HitRightTop { get { return hitRightTop; } set { hitRightTop = value; } }


        protected RaycastHit hitLeftBelow;

        public RaycastHit HitLeftBelow { get { return hitLeftBelow; } set { hitLeftBelow = value; } }

        protected RaycastHit hitMiddleBelow;

        public RaycastHit HitMiddleBelow { get { return hitMiddleBelow; } set { hitMiddleBelow = value; } }

        protected RaycastHit hitRightBelow;

        public RaycastHit HitRightBelow { get { return hitRightBelow; } set { hitRightBelow = value; } }


        protected RaycastHit attackRayResult;

        public RaycastHit AttackRayResult { get { return attackRayResult; } set { attackRayResult = value; } }
    }
}
