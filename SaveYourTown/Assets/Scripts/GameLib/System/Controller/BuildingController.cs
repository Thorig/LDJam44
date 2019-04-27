using UnityEngine;

namespace GameLib.System.Controller
{
    public class BuildingController : KeyboardController
    {
        public BuildingController(KeyboardSetting keys)
            : base(keys)
        {
        }

        public override KeysPressed updateInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                keysPressed.attack = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                keysPressed.attack = false;
            }

            return keysPressed;
        }
    }
}