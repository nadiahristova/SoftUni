namespace EnvironmentSystem.Core.Generator
{
    using System;
    using System.Collections.Generic;

    using EnvironmentSystem.Models;
    using EnvironmentSystem.Models.Objects;
    using EnvironmentSystem.Interfaces;

    public class ObjectGenerator : IObjectGenerator<EnvironmentObject>
    {
        private const int ObjectsCount = 10;
        private const int StaticStarCount = 20;
        private const int FallingStarCount = 2;
        private const int ExplodingStarCount = 10;

        private readonly int worldWidth;
        private readonly int worldHeight;
        private readonly Random randomGenerator;

        public ObjectGenerator(int worldWidth, int worldHeight)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
            this.randomGenerator = new Random();
        }

        /// <summary>
        /// Adds objects only once to the passed collection. Should be used once.
        /// </summary>
        /// <param name="objects"></param>
        public IEnumerable<EnvironmentObject> SeedInitialObjects()
        {
            var generatedObjects = new List<EnvironmentObject>();
            for (int i = 0; i < StaticStarCount; i++)
            {
                int x = this.randomGenerator.Next(0, this.worldWidth);
                int y = this.randomGenerator.Next(0, 20);

                var envObject = new Star(x, y, 1, 1);
                generatedObjects.Add(envObject);
            }


            for (int i = 0; i < FallingStarCount; i++)
            {

                int x = this.randomGenerator.Next(0, this.worldWidth);
                int y = this.randomGenerator.Next(0, 20);

                int[] dirPoss = { 1, -2 };
                int dirIndex = this.randomGenerator.Next(0, 2);

                var envObject = new FallingStar(x, y, 4, 4, new Point(dirPoss[dirIndex], 1));

                generatedObjects.Add(envObject);
            }

            for (int i = 0; i < ExplodingStarCount; i++)
            {

                int x = this.randomGenerator.Next(0, this.worldWidth);
                int y = this.randomGenerator.Next(0, 20);

                int[] dirPoss = { 1, -2 };
                int dirIndex = this.randomGenerator.Next(0, 2);

                var envObject = new ExplodingFallingStar(x, y, 4, 4, new Point(dirPoss[dirIndex], 1));

                generatedObjects.Add(envObject);
            }

            generatedObjects.Add(new Ground(0, 25, 50, 2, '#'));

            return generatedObjects;
        }

        /// <summary>
        /// Adds the next portion of objects to the passed collection. Can be used many times.
        /// </summary>
        /// <param name="objects"></param>
        public IEnumerable<EnvironmentObject> GenerateNextObjects()
        {
            var generatedObjects = new List<EnvironmentObject>();

            //for (int i = 0; i < ObjectsCount; i++)
            //{
            //    int generateFlag = this.randomGenerator.Next(0, 5);

            //    if (generateFlag == 1)
            //    {
            //        int x = this.randomGenerator.Next(0, this.worldWidth);
            //        var envObject = new Snowflake(x, 1, 1, 1, new Point(0, 1));

            //        generatedObjects.Add(envObject);
            //    }
            //}

            return generatedObjects;
        }
    }
}
