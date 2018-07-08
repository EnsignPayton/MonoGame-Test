using Microsoft.Xna.Framework.Input;

namespace Breakanoid
{
    public class InputState
    {
        public InputState()
        {
            var keyboardState = Keyboard.GetState();
            CurrentKeyboardState = keyboardState;
            PreviousKeyboardState = keyboardState;
        }

        public KeyboardState CurrentKeyboardState { get; private set; }
        public KeyboardState PreviousKeyboardState { get; private set; }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool KeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool KeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        public bool KeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) &&
                   PreviousKeyboardState.IsKeyUp(key);
        }
    }
}
