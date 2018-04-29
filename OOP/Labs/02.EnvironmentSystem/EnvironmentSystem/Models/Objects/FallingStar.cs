using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentSystem.Models.Objects
{
    public class FallingStar : MovingObject
    {
        public FallingStar(int x, int y, int width, int height, Point direction)
            : base(x, y, width, height, direction)
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Star;
        }

        protected virtual char[,] GenerateImageProfile()
        {
            int col;

            if (this.Direction.X == -2)
                col = 3;
            else col = 0;

            char[,] image = new char[this.Bounds.Height, this.Bounds.Width];
            for (int row = 0; row < image.GetLength(0); row++)
            {
                char ch;
                if (row == image.GetLength(0) - 1)
                    ch = '0';
                else ch = '*';
                image[row, Math.Abs(col)] = ch;
                col--;
            }

            return image;
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
            return new EnvironmentObject[0];
        }
    }
}
