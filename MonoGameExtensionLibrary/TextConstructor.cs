using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// Describes anything that is can be Scaled up or down and requires a field for the scale
    /// </summary>
    public interface IScaleable2D
    {
        void setScale(Vector2 scale);
        Vector2 getScale();
    }
    /// <summary>
    /// Creates Text Objects using with a spritefont, Text Objects are easier to draw and manipulate
    /// </summary>
    public class TextConstructor
    {
        private SpriteFont font;

        public TextConstructor(SpriteFont font)
        {
            this.font = font;
        }

        public Text CreateText(string text)
        {
            return new Text {
                font = font,
                textString = text,
                textColor = Color.White,
                Position = new Vector2(0, 0),
                effects = SpriteEffects.None,
                layerDepth = 0,
                Pivot = new Vector2(0, 0),
                rotation = 0.0f,
                scale = new Vector2(1,1)
            };
        }

        public Text CreateText()
        {
            return new Text
            {

            };
        }
    }
    /// <summary>
    /// A ValueType struct that is positionable, scaleable and can be drawn
    /// </summary>
    public struct Text : IPositionable2D, IScaleable2D
    {
        public Vector2 Position;
        public Vector2 scale;
        public Vector2 Pivot;
        public string textString;
        public Color textColor;
        public SpriteFont font;
        public float rotation;
        public SpriteEffects effects;
        public int layerDepth;
        
        public void DrawTextSimple(SpriteBatch batch)
        {
            batch.DrawString(font, textString, Position, textColor);
        }
        public void DrawTextComplex(SpriteBatch batch)
        {
            batch.DrawString(font, textString, Position, textColor, rotation, Pivot, scale, effects, layerDepth);
        }

        public Vector2 getScale()
        {
            return scale;
        }

        public float GetX()
        {
            return Position.X;
        }

        public float GetY()
        {
            return Position.Y;
        }

        public void setScale(Vector2 scale)
        {
            this.scale = scale;
        }

        public void SetX(float x)
        {
            Position.X = x;
        }

        public void SetY(float y)
        {
            Position.Y = y;
        }
    }
}
