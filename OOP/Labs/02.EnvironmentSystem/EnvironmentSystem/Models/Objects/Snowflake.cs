namespace EnvironmentSystem.Models.Objects
{
    using DataStructures;
    using System.Collections.Generic;

    public class Snowflake : MovingObject
    {
        private EnvironmentObject snow;

        public Snowflake(int x, int y, int width, int height, Point direction)
            : base(x, y, width, height, direction)
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Snowflake;
        }

        protected virtual char[,] GenerateImageProfile()
        {
            //char[,] image = new char[this.Bounds.Height, this.Bounds.Width];
            //for (int row = 0; row < image.GetLength(0); row++)
            //{
            //    for (int col = 0; col < image.GetLength(1); col++)
            //    {
            //        image[row, col] = this.image;
            //    }
            //}
            return new char[,] { { '*' } };
        }

        public override void RespondToCollision(CollisionInfo collisionInfo)
        {
            var hitObjectGroup = collisionInfo.HitObject.CollisionGroup;
            if (hitObjectGroup == CollisionGroup.Ground ||
                hitObjectGroup == CollisionGroup.Snow)
            {
                this.snow = new Snow(this.Bounds.TopLeft.X, this.Bounds.TopLeft.Y - this.Bounds.Height,
                    this.Bounds.Width, this.Bounds.Height);

                this.Exists = false;                
            }
        }

        public override IEnumerable<EnvironmentObject> ProduceObjects()
        {
            if (this.snow != null)
            {
                return new EnvironmentObject[]{ this.snow };

            }
            return new EnvironmentObject[0];
        }
    }
}
