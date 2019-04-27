//#define DEBUG

using UnityEngine;

namespace GameLib.System.Gravity2D
{
    public class NoGravity : Gravity
    {
        // Update is called once per frame
        public override void update(IGravityClient object_)
        {
#if (DEBUG)
           // Debug.Log("Update method is empty!");
#endif
        }

        public override bool isNotFalling()
        {
            return true;
        }

        public override bool isStanding()
        {
            return true;
        }

        public override bool isFalling()
        {
            return false;
        }
    }
}