using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameExtensionLibrary
{
    /// <summary>
    /// Des
    /// </summary>
    public struct Collision
    {
        public Collider Caller;
        public Collider otherCollider;
    }
    public class Collider
    {
        private IShape2D shape;

        public IShape2D Shape
        {
            get
            {
                return shape;
            }
        }

        private List<Collider> IntersectingColliders;

        public delegate void CollisionEvent(object sender, Collision e);

        public event CollisionEvent OnCollisionEnter;
        public event CollisionEvent OnCollisionStay;
        public event CollisionEvent OnCollisionExit;

        public Collider(IShape2D colliderShape)
        {
            shape = colliderShape;
            OnCollisionEnter += AddNewIntersectingCollider;
            OnCollisionExit += RemoveIntersectingCollider;
            IntersectingColliders = new List<Collider>();
        }


        public void FullCheck(Collider[] other)
        {
            for (int i = 0; i < other.Length; i++)
            {
                CheckForNewIntersection(other[i]);
            }

            CheckIfStillIntersecting();
        }

        public void CheckForNewIntersection(Collider other)
        {
            if (!IntersectingColliders.Contains(other) && shape.Intersects(other.shape))
            {
                OnCollisionEnter?.Invoke(this, new Collision() { Caller = this, otherCollider = other });
            }
        }

        private void AddNewIntersectingCollider(object sender, Collision e)
        {
            IntersectingColliders.Add(e.otherCollider);
        }

        private void RemoveIntersectingCollider(object sender, Collision e)
        {
            IntersectingColliders.Remove(e.otherCollider);
        }

        public void CheckIfStillIntersecting()
        {
            foreach (Collider item in IntersectingColliders)
            {
                if (shape.Intersects(item.shape))
                {
                    OnCollisionStay?.Invoke(this, new Collision() { Caller = this, otherCollider = item });
                }
                else
                {
                    OnCollisionExit?.Invoke(this, new Collision() { Caller = this, otherCollider = item });
                }
            }
        }
    }
}
