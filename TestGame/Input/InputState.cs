using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestGame.Input
{
    /// <summary>
    /// Handles input state for each frame.
    /// </summary>
    public class InputState
    {
        #region Constructor

        public InputState()
        {
            CurrentKeyboardState = Keyboard.GetState();
            PreviousKeyboardState = Keyboard.GetState();

            CurrentMouseState = Mouse.GetState();
            PreviousMouseState = Mouse.GetState();

            var gamePadState1 = GamePad.GetState(PlayerIndex.One);
            var gamePadState2 = GamePad.GetState(PlayerIndex.Two);
            var gamePadState3 = GamePad.GetState(PlayerIndex.Three);
            var gamePadState4 = GamePad.GetState(PlayerIndex.Four);

            CurrentGamePadStates = new[]
            {
                gamePadState1,
                gamePadState2,
                gamePadState3,
                gamePadState4
            };

            PreviousGamePadStates = new[]
            {
                gamePadState1,
                gamePadState2,
                gamePadState3,
                gamePadState4
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// The keyboard state this frame.
        /// </summary>
        public KeyboardState CurrentKeyboardState { get; private set; }

        /// <summary>
        /// The keyboard state last frame.
        /// </summary>
        public KeyboardState PreviousKeyboardState { get; private set; }

        /// <summary>
        /// The mouse state this frame.
        /// </summary>
        public MouseState CurrentMouseState { get; private set; }

        /// <summary>
        /// The mouse state last frame.
        /// </summary>
        public MouseState PreviousMouseState { get; private set; }

        /// <summary>
        /// The gamepad states this frame.
        /// </summary>
        public GamePadState[] CurrentGamePadStates { get; }

        /// <summary>
        /// The gamepad states last frame.
        /// </summary>
        public GamePadState[] PreviousGamePadStates { get; }

        #endregion


        /// <summary>
        /// Update the input state for the current frame.
        /// Call this at the start of every <see cref="Game.Update"/>.
        /// </summary>
        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;

            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            for (int i = 0; i < 4; i++)
            {
                PreviousGamePadStates[i] = CurrentGamePadStates[i];
                CurrentGamePadStates[i] = GamePad.GetState(i);
            }
        }

        #region Single State Queries

        /// <summary>
        /// Gets whether given key is currently being pressed.
        /// </summary>
        /// <param name="key">The key to query</param>
        /// <returns>If key is pressed</returns>
        public bool KeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets whether given key is currently not being pressed.
        /// </summary>
        /// <param name="key">The key to query</param>
        /// <returns>If key is not pressed</returns>
        public bool KeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets whether the given key was pressed this frame.
        /// </summary>
        /// <param name="key">The key to query</param>
        /// <returns>If key was pressed this frame</returns>
        public bool KeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Gets whether the given mouse button is currently being pressed.
        /// </summary>
        /// <param name="button">The button to query</param>
        /// <returns>If button is pressed</returns>
        public bool MouseDown(MouseButton button)
        {
            return MouseButtonToState(button) == ButtonState.Pressed;
        }

        /// <summary>
        /// Gets whether the given mouse button is currently not being pressed.
        /// </summary>
        /// <param name="button">The button to query</param>
        /// <returns>If button is not pressed</returns>
        public bool MouseUp(MouseButton button)
        {
            return MouseButtonToState(button) == ButtonState.Released;
        }

        /// <summary>
        /// Gets whether the given mouse button was pressed this frame.
        /// </summary>
        /// <param name="button">The button to query</param>
        /// <returns>If button was pressed this frame</returns>
        public bool MousePressed(MouseButton button)
        {
            return MouseButtonToState(button) == ButtonState.Pressed &&
                   MouseButtonToState(button, false) == ButtonState.Released;
        }

        /// <summary>
        /// Gets whether the given gamepad button is currently being pressed.
        /// </summary>
        /// <param name="index">GamePad index</param>
        /// <param name="button">The button to query</param>
        /// <returns>If button is pressed</returns>
        public bool GamePadButtonDown(int index, Buttons button)
        {
            return CurrentGamePadStates[index].IsButtonDown(button);
        }

        /// <summary>
        /// Gets whether the given gamepad button is currently not being pressed.
        /// </summary>
        /// <param name="index">GamePad index</param>
        /// <param name="button">The button to query</param>
        /// <returns>If button is not pressed</returns>
        public bool GamePadButtonUp(int index, Buttons button)
        {
            return CurrentGamePadStates[index].IsButtonUp(button);
        }

        /// <summary>
        /// Gets whether the given gamepad button was pressed this frame.
        /// </summary>
        /// <param name="index">GamePad index</param>
        /// <param name="button">The button to query</param>
        /// <returns>If button was pressed this frame</returns>
        public bool GamePadButtonPressed(int index, Buttons button)
        {
            return CurrentGamePadStates[index].IsButtonDown(button) &&
                   PreviousGamePadStates[index].IsButtonUp(button);
        }

        #endregion

        #region Multiple State Queries

        /// <summary>
        /// Gets whether the given keys are currently being pressed.
        /// </summary>
        /// <param name="all">Return true for all keys vs any key</param>
        /// <param name="keys">The keys to query</param>
        /// <returns>If keys are pressed</returns>
        public bool KeysDown(bool all, params Keys[] keys)
        {
            bool result = all;

            foreach (var key in keys)
            {
                if (all)
                    result &= KeyDown(key);
                else
                    result |= KeyDown(key);
            }

            return result;
        }

        /// <summary>
        /// Gets whether the given buttons are being pressed this frame.
        /// </summary>
        /// <param name="all">Return true for all buttons vs any button</param>
        /// <param name="buttons">The buttons to query</param>
        /// <returns>If buttons are pressed</returns>
        public bool MultiMousePressed(bool all, params MouseButton[] buttons)
        {
            bool result = all;

            foreach (var button in buttons)
            {
                if (all)
                    result &= MousePressed(button);
                else
                    result |= MousePressed(button);
            }

            return result;
        }

        #endregion

        private ButtonState MouseButtonToState(MouseButton button, bool useCurrent = true)
        {
            ButtonState buttonState;
            switch (button)
            {
                case MouseButton.LeftButton:
                    buttonState = useCurrent ? CurrentMouseState.LeftButton : PreviousMouseState.LeftButton;
                    break;
                case MouseButton.MiddleButton:
                    buttonState = useCurrent ? CurrentMouseState.MiddleButton : PreviousMouseState.MiddleButton;
                    break;
                case MouseButton.RightButton:
                    buttonState = useCurrent ? CurrentMouseState.RightButton : PreviousMouseState.RightButton;
                    break;
                case MouseButton.XButton1:
                    buttonState = useCurrent ? CurrentMouseState.XButton1 : PreviousMouseState.XButton1;
                    break;
                case MouseButton.XButton2:
                    buttonState = useCurrent ? CurrentMouseState.XButton2 : PreviousMouseState.XButton2;
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(button), (int) button, typeof(MouseButton));
            }

            return buttonState;
        }
    }
}
