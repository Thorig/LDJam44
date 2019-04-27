namespace GameLib.System.Controller
{
    public class InputFactory : IInputFactory
    {
        private static IInputFactory instance;

        private InputFactory()
        {

        }

        public static IInputFactory getInstance()
        {
            if (instance == null)
            {
                instance = new InputFactory();
            }

            return instance;
        }

        public IController getKeyboardInput(KeyboardSetting keys)
        {
            return new KeyboardController(keys);
        }

        public IController getBuildingController(KeyboardSetting keys)
        {
            return new BuildingController(keys);
        }

        public IController getNonController()
        {
            return new NonContoller();
        }
    }
}