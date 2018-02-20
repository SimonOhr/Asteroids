using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids_2_Return_of_the_Asteroids
{
    static class KeyMouseReader
    {
        public static KeyboardState KeyState { get; set; } = Keyboard.GetState();
        public static KeyboardState OldKeyState { get; set; } = Keyboard.GetState();
        public static MouseState MouseState { get; set; } = Mouse.GetState();
        public static MouseState OldMouseState { get; set; } = Mouse.GetState();
        public static Vector2 MousePosition { get; set; }
        public static Vector2 CursorViewToWorldPosition { get; private set; }
        public static Camera Camera { get; set; }

        public static void Update(Camera cam)
        {
            Camera = cam;
            OldKeyState = KeyState;
            KeyState = Keyboard.GetState();

            OldMouseState = MouseState;
            MouseState = Mouse.GetState();

            MousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            CursorViewToWorldPosition = Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(Camera.GetTransform()));
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyState.IsKeyDown(key) && OldKeyState.IsKeyUp(key);
        }
        public static bool KeyHeld(Keys key)
        {
            return KeyState.IsKeyDown(key) && OldKeyState.IsKeyDown(key);
        }
        public static bool KeyReleased(Keys key)
        {
            return KeyState.IsKeyUp(key) && OldKeyState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return MouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released;
        }
        public static bool RightClick()
        {
            return MouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released;
        }

        public static bool ScrollUp()
        {
            return MouseState.ScrollWheelValue > OldMouseState.ScrollWheelValue;
        }

        public static bool ScrollDown()
        {
            return MouseState.ScrollWheelValue < OldMouseState.ScrollWheelValue;
        }

        public static Camera PassCameraInformation() //ref?
        {
            return  Camera;
        }
    }
}
