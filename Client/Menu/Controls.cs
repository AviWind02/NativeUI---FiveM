using CitizenFX.Core.Native;
using CitizenFX.Core;

using System.Threading.Tasks;

namespace NativeUI.Client.Menu
{
    public class Controls
    {
        public static bool CursorVisible = false;
        public static bool LeftPressed = false;
        public static bool RightPressed = false;
        public static bool SelectPressed = false;
        public static bool UsingInMenuKeyboard = false;
        public static bool IsMenuOpen = true;

        public static int OptionCount = 0;
        public static int CurrentOption = 1;
        public static int KeyPressDelay = 150;

        public static long KeyPressDelayTickCount = API.GetGameTimer();

        public static bool IsKeyPressed(int virtualKey)
        {
            return API.IsControlJustPressed(0, virtualKey);
        }

        public static bool IsOpenMenuPressed() { return IsKeyPressed((int)Control.VehicleHorn); }
        public static bool IsMouseButtonPressed() { return IsKeyPressed((int)Control.CursorAccept); }
        public static bool IsFavoriteKeyPressed() { return IsKeyPressed((int)Control.CharacterWheel); }
        public static bool IsScrollTopMenuPressed() { return IsKeyPressed((int)Control.ScriptRT); }

        public static bool IsMovingForward() { return IsKeyPressed((int)Control.MoveUpOnly); }
        public static bool IsMovingBackward() { return IsKeyPressed((int)Control.MoveDownOnly); }
        public static bool IsMovingRight() { return IsKeyPressed((int)Control.MoveRightOnly); }
        public static bool IsMovingLeft() { return IsKeyPressed((int)Control.MoveLeftOnly); }

        public static bool IsLeftShiftPressed() { return IsKeyPressed((int)Control.Sprint); }
        public static bool IsLeftCtrlPressed() { return IsKeyPressed((int)Control.Duck); }

        public static bool IsMouseWheelScrolledUp() { return API.IsControlJustPressed(0, (int)Control.CursorScrollUp); }
        public static bool IsMouseWheelScrolledDown() { return API.IsControlJustPressed(0, (int)Control.CursorScrollDown); }

        public static bool IsUpArrowPressed()
        {
            return IsKeyPressed((int)Control.FrontendUp) || IsKeyPressed((int)Control.MoveUpOnly);
        }

        public static bool IsDownArrowPressed()
        {
            return IsKeyPressed((int)Control.FrontendDown) || IsKeyPressed((int)Control.MoveDownOnly);
        }

        public static bool IsRightArrowPressed()
        {
            return IsKeyPressed((int)Control.FrontendRight) || IsKeyPressed((int)Control.MoveRightOnly);
        }

        public static bool IsLeftArrowPressed()
        {
            return IsKeyPressed((int)Control.FrontendLeft) || IsKeyPressed((int)Control.MoveLeftOnly);
        }

        public static bool IsSelectKeyPressed()
        {
            return IsKeyPressed((int)Control.FrontendAccept);
        }

        public static bool IsBackKeyPressed()
        {
            return IsKeyPressed((int)Control.FrontendCancel);
        }

        // Input handling for capturing character input
        public static char GetPressedKeyChar()
        {
            for (int key = 8; key <= 190; ++key)
            {
                if (API.IsControlJustPressed(0, key))
                {
                    return (char)key; // Simple representation for characters
                }
            }
            return '\0'; // No key pressed
        }

        // Gets user input and handles keyboard interactions
        public static void GetUserInput(string option, ref string userInput, int maxLength, ref bool enterPressed)
        {
            if (CurrentOption == OptionCount)
            {
                UsingInMenuKeyboard = true;
                long currentTick = API.GetGameTimer();
                if (currentTick - KeyPressDelayTickCount > KeyPressDelay)
                {
                    if (IsSelectKeyPressed())
                    {
                        enterPressed = true;
                        KeyPressDelayTickCount = currentTick;
                    }
                    else if (IsBackKeyPressed() && userInput.Length > 0)
                    {
                        userInput = userInput.Substring(0, userInput.Length - 1); // Backspace handling
                        KeyPressDelayTickCount = currentTick;
                    }
                    else
                    {
                        char key = GetPressedKeyChar();
                        if (key != '\0' && userInput.Length < maxLength - 1)
                        {
                            enterPressed = false;
                            userInput += key;
                            KeyPressDelayTickCount = currentTick;
                        }
                    }
                }
            }
            else
                UsingInMenuKeyboard = false;
        }

        public static void DisableControls()
        {
            if (IsMenuOpen)
            {
                if (UsingInMenuKeyboard)
                {
                    API.DisableAllControlActions(0);
                }

                if (CursorVisible)
                {
                    API.DisableControlAction(0, (int)Control.LookLeftRight, true);
                    API.DisableControlAction(0, (int)Control.LookUpDown, true);
                }

                API.DisableControlAction(0, (int)Control.Phone, true);
                API.DisableControlAction(0, (int)Control.CharacterWheel, true);
                API.DisableControlAction(0, (int)Control.VehicleHeadlight, true);
            }
        }

        public static void ControlTick()
        {
            long currentTick = API.GetGameTimer();

            LeftPressed = false;
            RightPressed = false;
            SelectPressed = false;

            if (currentTick - KeyPressDelayTickCount > KeyPressDelay)
            {
                if (IsOpenMenuPressed())
                {
                    IsMenuOpen = !IsMenuOpen;
                    KeyPressDelayTickCount = currentTick;
                }

                if (IsMenuOpen)
                {
                    if (IsUpArrowPressed())
                    {
                        CurrentOption = CurrentOption > 1 ? CurrentOption - 1 : OptionCount;
                        KeyPressDelayTickCount = currentTick;
                    }
                    else if (IsDownArrowPressed())
                    {
                        CurrentOption = CurrentOption < OptionCount ? CurrentOption + 1 : 1;
                        KeyPressDelayTickCount = currentTick;
                    }
                    else if (IsSelectKeyPressed())
                    {
                        SelectPressed = true;
                        KeyPressDelayTickCount = currentTick;
                    }
                    else if (IsBackKeyPressed())
                    {
                        KeyPressDelayTickCount = currentTick;
                    }
                }
            }
            OptionCount = 0;
        }
    }
}
