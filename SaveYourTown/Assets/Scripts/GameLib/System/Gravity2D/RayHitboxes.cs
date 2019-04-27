using UnityEngine;

namespace GameLib.System.Gravity2D
{
    public class RayHitboxes : RayHitBoxAttributes
    {
        protected RaycastHit2D hitTopFront;

        public RaycastHit2D HitTopFront { get { return hitTopFront; } set { hitTopFront = value; } }

        protected RaycastHit2D hitMiddleFront;

        public RaycastHit2D HitMiddleFront { get { return hitMiddleFront; } set { hitMiddleFront = value; } }

        protected RaycastHit2D hitBelowFront;

        public RaycastHit2D HitBelowFront { get { return hitBelowFront; } set { hitBelowFront = value; } }


        protected RaycastHit2D hitLeftTop;

        public RaycastHit2D HitLeftTop { get { return hitLeftTop; } set { hitLeftTop = value; } }

        protected RaycastHit2D hitMiddleTop;

        public RaycastHit2D HitMiddleTop { get { return hitMiddleTop; } set { hitMiddleTop = value; } }

        protected RaycastHit2D hitRightTop;

        public RaycastHit2D HitRightTop { get { return hitRightTop; } set { hitRightTop = value; } }


        protected RaycastHit2D hitLeftBelow;

        public RaycastHit2D HitLeftBelow { get { return hitLeftBelow; } set { hitLeftBelow = value; } }

        protected RaycastHit2D hitMiddleBelow;

        public RaycastHit2D HitMiddleBelow { get { return hitMiddleBelow; } set { hitMiddleBelow = value; } }

        protected RaycastHit2D hitRightBelow;

        public RaycastHit2D HitRightBelow { get { return hitRightBelow; } set { hitRightBelow = value; } }


        protected RaycastHit2D attackRayResult;

        public RaycastHit2D AttackRayResult { get { return attackRayResult; } set { attackRayResult = value; } }
    }
}