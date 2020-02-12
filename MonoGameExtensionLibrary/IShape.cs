using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameExtensionLibrary
{
    public enum ShapeTypes
    {
        Rect, Circ
    }

    public enum CenterType
    {
        UpperLeft, Center
    }

    public interface IPositionable2D
    {
        float GetX();
        float GetY();
        void SetX(float x);
        void SetY(float y);
    }

    
    public interface IBoundable2D: IPositionable2D
    {
        Rect getBounds();
    }

    public interface IShape2D : IBoundable2D
    {
   
        CenterType GetCenterType();
        ShapeTypes getShape();
        bool Contains(float x, float y);
        bool Intersects(IShape2D other, bool doublesided = true);
    }
    public interface IShape2D<Shape> : IShape2D, IEquatable<Shape> where Shape : IShape2D
    {
        bool Intersects(Shape other);
    }
}
