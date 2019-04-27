namespace GameLib.System.Gravity2D
{
    public class RayHitBoxAttributes
    {
        protected float distanceFront;

        public float DistanceFront { get { return distanceFront; } set { distanceFront = value; } }

        protected float distanceTop;

        public float DistanceTop { get { return distanceTop; } set { distanceTop = value; } }

        protected float distanceBelow;

        public float DistanceBelow { get { return distanceBelow; } set { distanceBelow = value; } }
    }
}
