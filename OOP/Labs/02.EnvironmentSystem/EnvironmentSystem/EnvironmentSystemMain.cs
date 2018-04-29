namespace EnvironmentSystem
{
    using Core;
    using Core.Generator;
    using System;

    public class EnvironmentSystemMain
    {
        private const int WorldWidth = 50;
        private const int WorldHeight = 30;

        static void Main()
        {
            var objectGenerator = new ObjectGenerator(WorldWidth, WorldHeight);
            var consoleRenderer = new ConsoleRenderer(WorldWidth, WorldHeight);
            var collisionHandler = new CollisionHandler(WorldWidth, WorldHeight);
            var controller = new Controller();
            controller.Pause += Controller_Pause;

            var engine = new ExtendedEngine(WorldWidth, 
                WorldHeight, 
                objectGenerator, 
                collisionHandler, 
                consoleRenderer,
                controller);

            engine.Run();
        }

        private static void Controller_Pause(object sender, System.EventArgs e)
        {
            Console.ReadKey();
        }
    }
}
