using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentSystem.Models.Objects
{
    public class Star : EnvironmentObject
    {
        private char image;
        private readonly Random rnd;
        private static int counter = 0;

        public Star(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            this.rnd = new Random();

            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Star;            
        }

        protected virtual char[,] GenerateImageProfile()
        {
            char[] poss = new char[] { '0', '@', 'O'};
            int starImage = rnd.Next(0, poss.Length);
            this.image = poss[starImage];

            return new char[,] { { poss[starImage]} };
        }

        public override void Update()
        {           
            if (counter == 10)
            {
                this.ImageProfile = GenerateImageProfile();
                counter = 0;
            }

            counter++;
        }

        public override void RespondToCollision(CollisionInfo collisionInfo)
        {
            var hitObjectGroup = collisionInfo.HitObject.CollisionGroup;
            if (hitObjectGroup == CollisionGroup.Explosion)
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
