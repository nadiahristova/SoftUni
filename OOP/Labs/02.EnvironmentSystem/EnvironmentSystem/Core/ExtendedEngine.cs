namespace EnvironmentSystem.Core
{
    using Interfaces;
    using Models.Objects;

    public class ExtendedEngine : Engine
    {
        protected readonly IController controller;

        public ExtendedEngine(int worldWidth, int worldHeight, 
            IObjectGenerator<EnvironmentObject> objectGenerator, ICollisionHandler collisionHandler, 
            IRenderer renderer, IController controller) 
            : base(worldWidth, worldHeight, objectGenerator, collisionHandler, renderer)
        {
            this.controller = controller;
        }

        protected override void Insert(EnvironmentObject obj)
        {
            if (obj != null)
            {
                base.Insert(obj);
            }            
        }

        protected override void ExecuteEnvironmentLoop()
        {  
            base.ExecuteEnvironmentLoop();

            controller.ProcessInput();
        }
    }
}
