using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineQuestions.App.Startup))]
namespace OnlineQuestions.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
