using System;

namespace GameLib.System.Controller
{
    class NonContoller : IController
    {
        private KeysPressed keysPressed = new KeysPressed();

        public KeysPressed KeysPressed { get { return keysPressed; } set { keysPressed = value; } }

        public void reset()
        {
        }

        public KeysPressed updateInput()
        {
            return keysPressed;
        }
    }
}
