using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

public class Draw
{
    public void DrawText(string text, Color color, Vector2 position, Vector2 size, bool center, bool rightAlign)
    {
        if (center)
        {
            API.SetTextCentre(true);
        }
        if (rightAlign)
        {
            API.SetTextRightJustify(true);
            API.SetTextWrap(0.0f, position.X);
        }

        API.SetTextColour(color.R, color.G, color.B, color.A);
        API.SetTextFont(0);
        API.SetTextScale(size.X, size.Y);
        API.SetTextDropshadow(1, 0, 0, 0, 0);
        API.SetTextEdge(1, 0, 0, 0, 0);
        API.SetTextOutline();
        API.BeginTextCommandDisplayText("STRING");
        API.AddTextComponentSubstringPlayerName(text);
        API.EndTextCommandDisplayText(position.X, position.Y);
    }

    public void DrawRect(Color color, Vector2 position, Vector2 size)
    {
        API.DrawRect(position.X, position.Y, size.X, size.Y, color.R, color.G, color.B, color.A);
    }

    public void DrawSprite(string textureDict, string textureName, Vector2 position, Vector2 size, float rotation, Color color)
    {
        if (!API.HasStreamedTextureDictLoaded(textureDict))
        {
            API.RequestStreamedTextureDict(textureDict, false);
        }
        else
        {
            API.DrawSprite(textureDict, textureName, position.X, position.Y, size.X, size.Y, rotation, color.R, color.G, color.B, color.A);
        }
    }

    public void DrawLine(Vector2 start, Vector2 end, Color color)
    {
        API.DrawLine(start.X, start.Y, 0, end.X, end.Y, 0, color.R, color.G, color.B, color.A);
    }

    public void DrawCircle(Vector2 center, float radius, Color color, int segments = 50)
    {
        float angleStep = 360.0f / segments;
        Vector2 previousPoint = center + new Vector2(radius, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * (float)(Math.PI / 180);
            Vector2 newPoint = center + new Vector2((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);

            DrawLine(previousPoint, newPoint, color);
            previousPoint = newPoint;
        }
    }
}
