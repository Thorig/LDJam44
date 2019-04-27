using UnityEngine;

namespace GameLib.Level
{
    public class Tile : MonoBehaviour
    {
        public virtual float getAngle()
        {
            return transform.localRotation.eulerAngles.z;
        }
    }
}