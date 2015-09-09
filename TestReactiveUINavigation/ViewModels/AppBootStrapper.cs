using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReactiveUI;
using Splat;
using TestReactiveUINavigation.Views;

namespace TestReactiveUINavigation.ViewModels
{
    public class AppBootStrapper : ReactiveObject, IScreen
    {
        public AppBootStrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState testRouter = null)
        {
            Router = testRouter ?? new RoutingState();
            dependencyResolver = dependencyResolver ?? Locator.CurrentMutable;

            // Bind 
            RegisterParts(dependencyResolver);

            // TODO: This is a good place to set up any other app 
            // startup tasks, like setting the logging level
            LogHost.Default.Level = LogLevel.Debug;

            // Navigate to the opening page of the application
            Router.Navigate.Execute(new SimpleViewModel(this));
        }

        public RoutingState Router { get; }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(new MyLogManager(), typeof(ILogManager));
            dependencyResolver.RegisterConstant(this, typeof (IScreen));

            dependencyResolver.Register(() => new SimpleView(), typeof (IViewFor<SimpleViewModel>));
            dependencyResolver.Register(() => new ComplexView(), typeof (IViewFor<ComplexViewModel>));
            dependencyResolver.Register(() => new MiniView(), typeof (IViewFor<MiniViewModel>));
        }

        private class MyLogManager : ILogManager
        {
            private readonly IDictionary<Type, IFullLogger> _loggers = new Dictionary<Type, IFullLogger>();

            public IFullLogger GetLogger(Type type)
            {
                IFullLogger logger;
                if (!_loggers.TryGetValue(type, out logger))
                {
                    logger = new WrappingFullLogger(new MyLogger(), type);
                    _loggers[type] = logger;
                }
                return logger;
            }
        }

        // Log to immediate window
        private class MyLogger : ILogger  
        {
            public void Write(string message, LogLevel logLevel)
            {
                Debug.WriteLine($"[{logLevel}] {message}");
            }

            public LogLevel Level { get; set; }
        }
    }
}