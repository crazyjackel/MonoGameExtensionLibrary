using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// Data for usage in the Gameobject
    /// </summary>
    public struct GameData
    {
        public Texture2D texture;
        public Rect bounds;
        public Collider hit;
    }
    /// <summary>
    /// Arguements for Drawing
    /// </summary>
    public struct DrawArgs
    {
        public SpriteBatch sb;
        public GameData game;
        public Color? c;
    }
    public class GameObject
    {
        public delegate void UpdateEvent(object sender, GameData e);
        public delegate void DrawEvent(object sender, DrawArgs e);

        public event UpdateEvent update;
        public event DrawEvent draw;

        /// <summary>
        /// The Data of the GameObject
        /// </summary>
        protected GameData data;

        /// <summary>
        /// Gets the Texture of the GameObject
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return data.texture;
            }
            set
            {
                data.texture = value;
            }
        }
        /// <summary>
        /// Gets the bounds of the Gameobject
        /// </summary>
        public Rect Bounds
        {
            get
            {
                return data.bounds;
            }
            set
            {
                data.bounds = value;
            }
        }

        /// <summary>
        /// Gets the Collider of the Gameobject
        /// </summary>
        public Collider Hit
        {
            get
            {
                return data.hit;
            }
            set
            {
                data.hit = value;
            }
        }

        /// <summary>
        /// Creates a Gameobject
        /// </summary>
        /// <param name="text">The Gameobjects texture use for display</param>
        /// <param name="bounds">The boundaries of text</param>
        /// <param name="hit">The Collider Component of the Gameobject, it is not required</param>
        public GameObject(Texture2D text, Rect bounds, Collider hit = null)
        {
            data = new GameData();
            this.data.texture = text;
            this.data.bounds = bounds;
            this.data.hit = hit;
            draw += DrawMain;
            update += UpdateMain;
        }

        /// <summary>
        /// Invokes the Draw Event
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            draw?.Invoke(this, new DrawArgs() { game = data, sb = sb});
        }

        /// <summary>
        /// Invokes the Draw Event w/ Color
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="c"></param>
        public void Draw(SpriteBatch sb, Color c)
        {
            draw?.Invoke(this, new DrawArgs() { game = data, sb = sb, c = c});
        }

        /// <summary>
        /// Invokes the Update Event
        /// </summary>
        public void Update()
        {
            update?.Invoke(this,data);
        }

        /// <summary>
        /// The First Method that will be called by Draw by Default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DrawMain(object sender, DrawArgs e)
        {
            e.sb.Draw(e.game.texture, (Rectangle)e.game.bounds, e.c ?? Color.White);
        }

        /// <summary>
        /// The First Method that will be called by Update by Default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UpdateMain(object sender, GameData e)
        {

        }

        /// <summary>
        /// This function should be used for moving the gameobject and its collider
        /// </susmmary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Goto(float x, float y)
        {
            data.bounds.X = x;
            data.bounds.Y = y;
            if (Hit != null)
            {
                if (Hit.Shape.GetCenterType() == CenterType.Center)
                {
                    Hit.Shape.SetX(x + Hit.Shape.getBounds().Width / 2);
                    Hit.Shape.SetY(y + Hit.Shape.getBounds().Height / 2);
                }
                else if (Hit.Shape.GetCenterType() == CenterType.UpperLeft)
                {
                    Hit.Shape.SetX(x);
                    Hit.Shape.SetY(y);
                }
            }
        }

        /// <summary>
        /// Moves coordinates relative to the player
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Translate(float x, float y)
        {
            Goto(data.bounds.X + x, data.bounds.Y + y);
        }
    }
}
