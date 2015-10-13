namespace SousTitres
{
    using Domain.Command;
    using Microsoft.Practices.Unity;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Windows;

    public partial class App : Application
    {
        private IUnityContainer _container;
        
        private void InitializeContainer()
        {
            _container = new UnityContainer();
            _container.RegisterTypes(
                AllClasses.FromAssembliesInBasePath(),
                WithMappings.FromMatchingInterface,
                WithName.Default);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeContainer();

            //TODO: refactor this ugly if with strategy
            if (e.Args.Any())
            {
                try
                {
                    var command = _container.Resolve<DownloadSubtitlesCommand>();
                    command.Execute(new DownloadSubtitlesCommandArgs
                            {
                                FilePath = e.Args.First(),
                                Language = ConfigurationManager.AppSettings["Language"],
                                UserAgent = ConfigurationManager.AppSettings["UserAgent"]
                            });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                new MainWindow().ShowDialog();
            }
            Shutdown();
        }
    }
}
