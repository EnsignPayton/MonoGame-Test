using System;
using System.Reflection;
using Autofac;

namespace Breakanoid
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new MainGame())
            {
                var assembly = Assembly.GetExecutingAssembly();
                var builder = new ContainerBuilder();

                builder.RegisterAssemblyTypes(assembly);
                builder.RegisterInstance(game).AsSelf().SingleInstance();
                builder.RegisterType<InputState>().AsSelf().SingleInstance();

                game.Container = builder.Build();
                game.Run();
            }
        }
    }
}
