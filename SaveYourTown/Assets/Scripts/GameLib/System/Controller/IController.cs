namespace GameLib.System.Controller
{
    public interface IController
    {
        KeysPressed updateInput();
        void reset();
    }
}