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
        public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
        public static MouseState mouseState, oldMouseState = Mouse.GetState();
        public static Vector2 mousePosition;
        public static Vector2 cursorViewToWorldPosition;
        public static Camera camera;

        public static void Update(Camera cam)
        {
            camera = cam;
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            cursorViewToWorldPosition = Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(camera.GetTransform()));
        }

        public static bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
        public static bool KeyHeld(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyDown(key);
        }
        public static bool KeyReleased(Keys key)
        {
            return keyState.IsKeyUp(key) && oldKeyState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
        }
        public static bool RightClick()
        {
            return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
        }

        public static bool ScrollUp()
        {
            return mouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue;
        }

        public static bool ScrollDown()
        {
            return mouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue;
        }

        public static ref Camera PassCameraInformation()
        {
            return ref camera;
        }
    }
}
