using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// A Float-based Rectangle
    /// </summary>
    public struct Rect : IShape2D<Rect>
    {
        /// <summary>
        /// Gets an empty rectangle with default values of all fields
        /// </summary>
        public static Rect emptyRectangle = new Rect();

        #region Public Fields

        public float X, Y, Width, Height;

        #endregion Public Fields


        #region Public Properties

        /// <summary>
        /// Returns an Empty Rectangle
        /// </summary>
        public static Rect Empty
        {
            get { return emptyRectangle; }
        }
        /// <summary>
        /// Returns the float representing the left bound's x position
        /// </summary>
        public float Left
        {
            get { return this.X; }
        }
        /// <summary>
        /// Returns the float representing the right bound's x position
        /// </summary>
        public float Right
        {
            get { return (this.X + this.Width); }
        }
        /// <summary>
        /// Returns the float reprsenting the top bound's y position
        /// </summary>
        public float Top
        {
            get { return this.Y; }
        }
        /// <summary>
        /// Returns the float representing the bottom bound's y position
        /// </summary>
        public float Bottom
        {
            get { return (this.Y + this.Height); }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.X + this.Width/2, this.Y + this.Height/2);
            }
        }

        #endregion Public Properties


        public Rect(float x, float y, float width, float height)
        {


            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static bool operator ==(Rect a, Rect b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        public static implicit operator Rect(Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Height, rectangle.Width);
        }

        public static explicit operator Rectangle(Rect r)
        {
            return new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }

        public bool Contains(float x, float y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        public bool Equals(Rect other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is Rect) ? this == ((Rect)obj) : false;
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y;
        }

        public bool Intersects(Rect other)
        {
            return !(other.Left > Right
                     || other.Right < Left
                     || other.Top > Bottom
                     || other.Bottom < Top
                    );
        }

        public bool Intersects(IShape2D other, bool doubledsided = true)
        {

            if (other is Rect rectangle)
            {
                return this.Intersects(rectangle);
            }
            else if (doubledsided)
            {
                return other.Intersects(this, false);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
        }

        public ShapeTypes getShape()
        {
            return ShapeTypes.Rect;
        }

        public void SetX(float x)
        {
            this.X = x;
        }

        public void SetY(float y)
        {
            this.Y = y;
        }

        public CenterType GetCenterType()
        {
            return CenterType.UpperLeft;
        }

        public Rect getBounds()
        {
            return this;
        }
    }
}
