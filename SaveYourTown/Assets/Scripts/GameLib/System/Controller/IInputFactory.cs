namespace GameLib.System.Controller
{
    public interface IInputFactory
    {
        IController getKeyboardInput(KeyboardSetting keys);

        IController getBuildingController(KeyboardSetting keys);

        IController getNonController();
    }
}