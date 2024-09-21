using CitizenFX.Core;

namespace NativeUI.Client.Menu
{
    public class UIHandler
    {
        private Draw draw = new Draw();

        public static class Colours
        {
            public static Color OptionText = new Color(255, 255, 255, 255);
            public static Color OptionRect = new Color(0, 0, 0, 225);
            public static Color ScrollerColor = new Color(255, 223, 0, 175);
            public static Color HoverColor = new Color(100, 100, 100, 190);
            public static Color ToggleOff = new Color(255, 0, 0, 255);
            public static Color ToggleOn = new Color(0, 255, 0, 255);
            public static Color FooterRect = new Color(0, 0, 0, 255);
            public static Color HeaderRect = new Color(0, 0, 0, 255);
        }

        private static readonly int MaxOption = 10;

        private static Vector2 MenuPos = new Vector2(0.15f, 0.125f);
        private static Vector2 OptionSize = new Vector2(0.17f, 0.025f);
        private static Vector2 OptionOffsets = new Vector2(0f, 0.010f);
        private static Vector2 HeaderSize = new Vector2(OptionSize.X, 0.045f);
        private static Vector2 HeaderTextOffset = new Vector2(OptionSize.X, 0.065f);
        private static Vector2 FooterSize = new Vector2(OptionSize.X, 0.025f);

        private static Vector2 TextOffset = new Vector2(0.08f, 0.0025f);
        private static Vector2 RightTextOffset = new Vector2(0.01f, 0.005f);
        private static Vector2 FooterTextOffset = new Vector2(0.08f, 0.0025f);

        private static float ScrollerY = MenuPos.Y + 0.125f;
        private static float ScrollerSpeed = 0.10f;
        private static float TextScale = 0.25f;


        // Main Option Method
        public bool SetOption(string leftText, string centerText, string rightText)
        {
            Controls.OptionCount++;

            int startOption = (Controls.CurrentOption - 1) / MaxOption * MaxOption + 1;
            int endOption = startOption + MaxOption - 1;

            if (Controls.OptionCount >= startOption && Controls.OptionCount <= endOption)
            {
                float yPos = (OptionSize.Y * (Controls.OptionCount - startOption + 1) + OptionOffsets.Y) + MenuPos.Y;

                float smoothScrollYPosition = yPos;
                float scrollSpeed = 0.10f;

                Vector2 smoothTextPos = new Vector2(MenuPos.X, smoothScrollYPosition);

                draw.DrawRect(Colours.OptionRect, new Vector2(MenuPos.X, yPos), OptionSize);

                bool isCurrentOption = Controls.CurrentOption == Controls.OptionCount;

                if (!string.IsNullOrEmpty(leftText))
                {
                    Vector2 leftTextPos = new Vector2(MenuPos.X - TextOffset.X, yPos - OptionSize.Y / 2.00f + TextOffset.Y);
                    draw.DrawText(leftText, Colours.OptionText, leftTextPos, new Vector2(TextScale, TextScale), false, false);
                }

                if (!string.IsNullOrEmpty(centerText))
                {
                    Vector2 centerTextPos = new Vector2(MenuPos.X, yPos - OptionSize.Y / 2.00f + TextOffset.Y);
                    draw.DrawText(centerText, Colours.OptionText, centerTextPos, new Vector2(TextScale, TextScale), true, false);
                }

                if (!string.IsNullOrEmpty(rightText))
                {
                    Vector2 rightTextPos = new Vector2(MenuPos.X + TextOffset.X + RightTextOffset.X, yPos - OptionSize.Y / 2.00f + TextOffset.Y);
                    draw.DrawText(rightText, Colours.OptionText, rightTextPos, new Vector2(TextScale, TextScale), false, true);
                }

                if (isCurrentOption)
                {
                    smoothScrollYPosition += (yPos - smoothScrollYPosition) * scrollSpeed;
                    draw.DrawRect(Colours.ScrollerColor, new Vector2(MenuPos.X, smoothScrollYPosition), OptionSize);
                }
            }

            return false;
        }

        public void Title()
        {
            string titleText = "Avi";

            Vector2 titlePos = new Vector2(MenuPos.X, MenuPos.Y);
            draw.DrawRect(Colours.HeaderRect, titlePos, HeaderSize);

            Vector2 textPos = new Vector2(MenuPos.X + (HeaderSize.X - HeaderTextOffset.X), MenuPos.Y + (HeaderSize.Y - HeaderTextOffset.Y));
            draw.DrawText(titleText, Colours.OptionText, textPos, new Vector2(0.65f, 0.65f), true, false);
        }

        public void End()
        {
            int currentPage = (Controls.CurrentOption - 1) / MaxOption + 1;
            int startOption = (currentPage - 1) * MaxOption + 1;
            int displayedOptions = Controls.OptionCount - startOption + 1;
            displayedOptions = (displayedOptions > MaxOption) ? MaxOption : displayedOptions;

            float targetYPosition = (OptionSize.Y * displayedOptions + OptionSize.Y + OptionOffsets.Y) + MenuPos.Y;

            float currentYPosition = targetYPosition;
            float speed = 0.1f;
            currentYPosition += (targetYPosition - currentYPosition) * speed;

            Vector2 endPos = new Vector2(MenuPos.X, currentYPosition);

            draw.DrawRect(Colours.FooterRect, endPos, FooterSize);

            int totalPages = (Controls.OptionCount + MaxOption - 1) / MaxOption;
            string footerText = $"Page {currentPage}/{totalPages}";

            Vector2 leftTextPos = new Vector2(MenuPos.X - FooterTextOffset.X, endPos.Y - OptionSize.Y / 2.00f + FooterTextOffset.Y);
            draw.DrawText("Avi's Native UI", Colours.OptionText, leftTextPos, new Vector2(TextScale, TextScale), false, false);

            Vector2 rightTextPos = new Vector2(MenuPos.X + FooterTextOffset.X, endPos.Y - OptionSize.Y / 2.00f + FooterTextOffset.Y);
            draw.DrawText(footerText, Colours.OptionText, rightTextPos, new Vector2(TextScale, TextScale), false, true);
        }
    }
}
