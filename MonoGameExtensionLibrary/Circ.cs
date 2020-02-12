using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameExtensionLibrary
{


    public struct Circ : IShape2D<Circ>
    {
        public static Circ EmptyCircle = new Circ();

        public float X;
        public float Y;
        private float radius;

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (value >= 0)
                {
                    radius = value;
                }
                else
                {
                    throw new ArgumentException("Radius cannot be negative, use absolute value instead");
                }
            }
        }



        #region Constructors
        public Circ(int x, int y, float radius)
        {
            X = x;
            Y = y;
            if (radius >= 0)
            {
                this.radius = radius;
            }
            else
            {
                throw new ArgumentException("Radius cannot be negative, use absolute value instead");
            }
        }
        #endregion

        #region Public Static
        public static bool operator ==(Circ a, Circ b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.radius == b.radius));
        }
        public static bool operator !=(Circ a, Circ b)
        {
            return !(a == b);
        }

        public static float Distance(Circ a, Circ b)
        {
            return (float)Math.Sqrt((double)DistanceSquared(a, b));
        }

        public static float DistanceSquared(Circ a, Circ b)
        {
            return SummedSquares(a.X - b.X, a.Y - b.Y);
        }

        public static bool Intersects(Circ a, Circ b)
        {
            return (a.radius + b.radius) * (a.radius + b.radius) > DistanceSquared(a, b);
        }

        /// <summary>
        /// Returns whether a Circle Intersects a Rectangle, radius for operations will be rounded up.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Intersects(Circ a, Rect b)
        {
            //Convert Circle into Rectangular bounds form
            Rect fromCircle = new Rect(a.X - a.radius, a.Y - a.radius, 2 * a.radius, 2 * a.radius);

            //If Rectangles dont intersect, then they dont the circle doesnt intersect
            if (b.Intersects(fromCircle))
            {
                //where is the circle relative to the rectangle
                bool isAbove = fromCircle.Y < b.Y;
                bool isLeft = fromCircle.X < b.X;

                //Compare the radius to the nearest corner of the rectangle, if the distance is bigger than the radius, they intersect
                float DistanceToNearestCornerSquared = SummedSquares(a.X - ((isLeft)?b.Left:b.Right), a.Y - ((isAbove)?b.Top:b.Bottom));
                if(DistanceToNearestCornerSquared > (a.radius * a.radius))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Private Static
        private static float SummedSquares(float a, float b)
        {
            return a * a + b * b;
        }
        #endregion

        #region Public
        public float Distance(Circ other)
        {
            return Distance(this, other);
        }

        public float DistanceSquared(Circ other)
        {
            return DistanceSquared(this, other);
        }

        public bool Intersects(Circ other)
        {
            return Intersects(this, other);
        }

        public bool Contains(float x, float y)
        {
            return SummedSquares(this.X - x, this.Y - y) < radius * radius;
        }

        public bool Equals(Circ other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return (obj is Circ) ? this == ((Circ)obj) : false;
        }

        public override int GetHashCode()
        {
            return (this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.radius.GetHashCode());
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y;
        }
        #endregion

        public bool Intersects(IShape2D other, bool doubledsided = true)
        {

            if (other is Circ circle)
            {
                return Intersects(this, circle);
            }
            else if (other is Rect rectangle)
            {
                return Intersects(this, rectangle);
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

        public ShapeTypes getShape()
        {
            return ShapeTypes.Circ;
        }

        public void SetX(float x)
        {
            this.X = x;
        }

        public void SetY(float y)
        {
            this.Y = y;
        }

        public Rect getBounds()
        {
            return new Rect(X - radius, Y - radius, 2 * Radius, 2 * radius);
        }

        public CenterType GetCenterType()
        {
            return CenterType.Center;
        }
    }
}
