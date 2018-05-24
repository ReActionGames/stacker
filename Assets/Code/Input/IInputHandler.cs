using Stacker.Enums;

namespace Stacker
{
    public interface IInputHandler
    {

        bool IsInput();

        InputTriggerType HandleInput();
    }
}