using System;

namespace GameLib.Entity.Behaviour
{
    public class EntityPlayer : Entity
    {
        protected bool hasFlippedRayInformation;

        public bool HasFlippedRayInformation
        {
            get
            {
                return hasFlippedRayInformation;
            }
            set
            {
                hasFlippedRayInformation = value;
            }
        }

        public EntityPlayer(Player entity) : base(entity)
        {
            throw new NotSupportedException();
        }

        public EntityPlayer(Player entity, bool hasFlippedRayInformation) : base(entity)
        {
            this.hasFlippedRayInformation = hasFlippedRayInformation;
        }

        public override void flipped(bool flip)
        {
            base.flipped(flip);

            if(hasFlippedRayInformation && player.Collider2DFlippedX != null)
            {
                if (flip && !player.Collider2DFlippedX.enabled)
                {
                    player.Collider2DFlippedX.enabled = true;
                    player.GetComponent<UnityEngine.Collider2D>().enabled = false;
                }
                if (!flip && !player.GetComponent<UnityEngine.Collider2D>().enabled)
                {
                    player.Collider2DFlippedX.enabled = false;
                    player.GetComponent<UnityEngine.Collider2D>().enabled = true;
                }
            }
        }
    }
}