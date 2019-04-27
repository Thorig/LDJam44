namespace GameLib.System.Controller
{
    public class KeyboardController : IController
    {
        protected KeyboardSetting keys;
        protected KeysPressed keysPressed;

        public KeyboardController(KeyboardSetting keys)
        {
            this.keys = keys;
        }

        public void reset()
        {
            keysPressed.actionButtonFour = false;
            keysPressed.actionButtonOne = false;
            keysPressed.actionButtonThree = false;
            keysPressed.actionButtonTwo = false;
            keysPressed.attack = false;
            keysPressed.down = false;
            keysPressed.jump = false;
            keysPressed.right = false;
            keysPressed.up = false;
            keysPressed.left = false;
        }

        public virtual KeysPressed updateInput()
        {
            if (UnityEngine.Input.GetKeyDown(keys.upKey))
            {
                keysPressed.up = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.downKey))
            {
                keysPressed.down = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.jumpKey))
            {
                keysPressed.jump = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.attackKey))
            {
                keysPressed.attack = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.leftKey))
            {
                keysPressed.left = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.rightKey))
            {
                keysPressed.right = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.actionKeyOne))
            {
                keysPressed.actionButtonOne = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.actionKeyTwo))
            {
                keysPressed.actionButtonTwo = true;
            }
            if (UnityEngine.Input.GetKeyDown(keys.actionKeyThree))
            {
                keysPressed.actionButtonThree = true;
            }


            if (UnityEngine.Input.GetKeyUp(keys.jumpKey))
            {
                keysPressed.jump = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.attackKey))
            {
                keysPressed.attack = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.leftKey))
            {
                keysPressed.left = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.rightKey))
            {
                keysPressed.right = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.upKey))
            {
                keysPressed.up = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.downKey))
            {
                keysPressed.down = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.actionKeyOne))
            {
                keysPressed.actionButtonOne = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.actionKeyTwo))
            {
                keysPressed.actionButtonTwo = false;
            }
            if (UnityEngine.Input.GetKeyUp(keys.actionKeyThree))
            {
                keysPressed.actionButtonThree = false;
            }
            
            return keysPressed;
        }
    }
}