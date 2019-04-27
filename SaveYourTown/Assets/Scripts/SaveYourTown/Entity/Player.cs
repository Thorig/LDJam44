using GameLib.System.Gravity2D;
using SaveYourTown.Entity.Behaviour;
using UnityEngine;

namespace SaveYourTown.Entity
{
	public class Player : GameLib.Entity.Player
	{
        public GameObject button;
        public GameObject backGroundShop;
        public GameObject shopCursor;
        public float totalVitality = 100;
        public float currentVitality = 100;

        private bool isNearShop = false;

        public bool IsNearShop
        {
            get
            {
                return isNearShop;
            }
            set
            {
                isNearShop = value;
                button.SetActive(isNearShop);
            }
        }

        public override void setGravity()
        {
            IGravityFactory gravityFactory = new GravityFactory();

            gravity = gravityFactory.getNoGravity();
            gravity.init(gravitySetting);
        }

        protected override void setEntity()
        {
            setEntity(new BehaviourStateFactory());
        }

        public override void LateUpdate()
		{
			base.LateUpdate();
		}
	}
}
