//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Wpf.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Commands;
    using Services;

    /// <summary>
    ///     View model for the main application, handles the sub views and view model switching
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Wpf.ViewModels.ViewModelBase" />
    internal class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///     The past view models
        /// </summary>
        private readonly List<ViewModelBase> pastViewModels;

        /// <summary>
        ///     The audit
        /// </summary>
        private BrookfieldAudit audit;

        /// <summary>
        ///     The current view model
        /// </summary>
        private ViewModelBase currentViewModel;

        /// <summary>
        ///     The navigation BuilderMT button command
        /// </summary>
        private ICommand navigationBuilderMtButtonCommand;

        /// <summary>
        ///     The navigation configuration button command
        /// </summary>
        private ICommand navigationConfigurationButtonCommand;

        /// <summary>
        ///     The navigation run button command
        /// </summary>
        private ICommand navigationRunButtonCommand;

        /// <summary>
        ///     The navigation Sage 300 button command
        /// </summary>
        private ICommand navigationSageButtonCommand;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        public MainViewModel()
        {
            this.ViewTitle = "Procure Development Audit";

            this.pastViewModels = new List<ViewModelBase>();
            this.audit = new BrookfieldAudit();

            this.ChangeView(new BuilderMtViewModel(this.audit));
        }

        /// <summary>
        ///     Gets the current view model.
        /// </summary>
        /// <value>The current view model.</value>
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
        }

        /// <summary>
        ///     Gets the navigation BuilderMT button command.
        /// </summary>
        /// <value>The navigation BuilderMT button command.</value>
        public ICommand NavigationBuilderMtButtonCommand
        {
            get
            {
                return this.navigationBuilderMtButtonCommand ?? (this.navigationBuilderMtButtonCommand = new CommandHandler(() => this.ChangeView(new BuilderMtViewModel(this.audit))));
            }
        }

        /// <summary>
        ///     Gets the navigation configuration button command.
        /// </summary>
        /// <value>The navigation configuration button command.</value>
        public ICommand NavigationConfigurationButtonCommand
        {
            get
            {
                return this.navigationConfigurationButtonCommand ?? (this.navigationConfigurationButtonCommand = new CommandHandler(() => this.ChangeView(new ConfigurationViewModel(this.audit))));
            }
        }

        /// <summary>
        ///     Gets the navigation run button command.
        /// </summary>
        /// <value>The navigation run button command.</value>
        public ICommand NavigationRunButtonCommand
        {
            get
            {
                return this.navigationRunButtonCommand ?? (this.navigationRunButtonCommand = new CommandHandler(() => this.ChangeView(new RunViewModel(this.audit)), (object x) => { return this.audit.CanExecute; }));
            }
        }

        /// <summary>
        ///     Gets the navigation sage button command.
        /// </summary>
        /// <value>The navigation sage button command.</value>
        public ICommand NavigationSageButtonCommand
        {
            get
            {
                return this.navigationSageButtonCommand ?? (this.navigationSageButtonCommand = new CommandHandler(() => this.ChangeView(new SageViewModel(this.audit))));
            }
        }

        /// <summary>
        ///     Changes the view.
        /// </summary>
        /// <param name="view">The view.</param>
        private void ChangeView(ViewModelBase view)
        {
            var viewType = view.GetType();

            var pastViewModel = this.pastViewModels.FirstOrDefault(v => v.GetType() == viewType);
            if (pastViewModel == null)
            {
                this.pastViewModels.Add(view);
                pastViewModel = view;
            }

            this.currentViewModel = pastViewModel;

            this.NotifyPropertyChanged(this, nameof(this.CurrentViewModel));
        }
    }
}