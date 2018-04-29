using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentSystem.Models.Objects
{
    public class ExplodingFallingStar : FallingStar
    {
        private EnvironmentObject expObj;
        public int counter = 0;

        public ExplodingFallingStar(int x, int y, int width, int height, Point direction) : base(x, y, width, height, direction)
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Star;
        }

        public override void Update()
        {
            base.Update();

            if (counter == 8)
            {
                counter = 0;
                this.Exists = false;
                this.expObj = new Explostion(this.Bounds.TopLeft.X, this.Bounds.TopLeft.Y + 1, 5, 5);
            }
            else
                counter++;
        }

        public override void RespondToCollision(CollisionInfo collisionInfo)
        {
            var hitObjectGroup = collisionInfo.HitObject.CollisionGroup;
            if (hitObjectGroup == CollisionGroup.Ground || hitObjectGroup == CollisionGroup.Explosion)
            {
                this.Exists = false;
            }
        }

        public override IEnumerable<EnvironmentObject> ProduceObjects()
        {
            if (this.expObj != null)
            {
                return new EnvironmentObject[] { this.expObj };

            }
            return new EnvironmentObject[0];
        }
    }
}
