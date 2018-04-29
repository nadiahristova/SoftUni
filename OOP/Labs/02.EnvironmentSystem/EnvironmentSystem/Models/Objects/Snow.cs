using EnvironmentSystem.Models.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentSystem.Models.Objects
{
    public class Snow : EnvironmentObject
    {
        public Snow(Rectangle rect) : base(rect) 
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Snow;
        }

        public Snow(int x, int y, int width, int height) 
            : base(x, y, width, height)
        {
            this.ImageProfile = this.GenerateImageProfile();
            this.CollisionGroup = CollisionGroup.Snow;
        }

        public override IEnumerable<EnvironmentObject> ProduceObjects()
        {
            return new EnvironmentObject[0];
        }

        public override void RespondToCollision(CollisionInfo collisionInfo)
        {
        }

        public override void Update()
        {
        }

        protected virtual char[,] GenerateImageProfile()
        {
            return new char[,] { { '.' } };
        }

    }
}
