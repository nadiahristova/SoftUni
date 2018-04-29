using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentSystem.Models.Objects
{
    public class Explostion : EnvironmentObject
    {
        private static int counter = 0;

        public Explostion(int x, int y, int width, int height) 
            : base(x, y, width, height)
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Explosion;
        }

        public override IEnumerable<EnvironmentObject> ProduceObjects()
        {
            return new EnvironmentObject[0];
        }

        public override void RespondToCollision(CollisionInfo collisionInfo)
        {
            var hitObjectGroup = collisionInfo.HitObject.CollisionGroup;
            if (hitObjectGroup == CollisionGroup.Star)
            {
                var hitObj = collisionInfo.HitObject;
                //(hitObj as EnvironmentObject).Exists = false;               
            }
        }

        public override void Update()
        {
            if (counter < 2)
                counter++;
            else
                this.Exists = false;
            
        }

        protected char[,] GenerateImageProfile()
        {
            char[,] image = new char[this.Bounds.Height, this.Bounds.Width];
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    image[row, col] = '*';
                }
            }

            image[2, 2] = ' ';
            return image;
        }
    }
}
