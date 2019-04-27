namespace GameLib.System.Controller
{
    public struct KeysPressed
    {
        public bool jump;
        public bool left;
        public bool right;
        public bool attack;
        public bool up;
        public bool down;
        public bool actionButtonOne;
        public bool actionButtonTwo;
        public bool actionButtonThree;
        public bool actionButtonFour;

        public void reset()
        {
            jump = false;
            left = false;
            right = false;
            attack = false;
            up = false;
            down = false;
            actionButtonOne = false;
            actionButtonTwo = false;
            actionButtonThree = false;
            actionButtonFour = false;
        }
    }
}