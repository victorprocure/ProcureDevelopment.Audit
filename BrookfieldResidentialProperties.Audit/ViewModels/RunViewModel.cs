//-----------------------------------------------------------------------
// <copyright file="RunViewModel.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Wpf.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Commands;
    using Ookii.Dialogs.Wpf;
    using Services;
    using Services.Tasks;

    /// <summary>
    ///     Handles user interaction with running the audit
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Wpf.ViewModels.ViewModelBase" />
    internal class RunViewModel : ViewModelBase
    {
        /// <summary>
        ///     The audit that will be run
        /// </summary>
        private BrookfieldAudit audit;

        /// <summary>
        ///     The run tasks command
        /// </summary>
        private ICommand runTasks;

        /// <summary>
        ///     The select save folder command
        /// </summary>
        private ICommand selectSaveFolder;

        /// <summary>
        ///     The tasks to run
        /// </summary>
        private ObservableCollection<IAuditTask> tasks;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RunViewModel" /> class.
        /// </summary>
        /// <param name="audit">The audit.</param>
        public RunViewModel(BrookfieldAudit audit)
        {
            this.ViewTitle = "Run Audit";
            this.audit = audit;

            foreach (var task in this.audit.Tasks)
            {
                task.ProgressUpdated += this.TaskUpdated;
            }
        }

        /// <summary>
        ///     Gets the run tasks.
        /// </summary>
        /// <value>The run tasks.</value>
        public ICommand RunTasks
        {
            get
            {
                return this.runTasks ?? (this.runTasks = new CommandHandler(this.audit.RunAll, (object x) => { return this.SaveFolder.Length > 0; }));
            }
        }

        /// <summary>
        ///     Gets or sets the save folder.
        /// </summary>
        /// <value>The save folder.</value>
        public string SaveFolder
        {
            get
            {
                return this.audit.SavePath ?? string.Empty;
            }

            set
            {
                this.audit.SavePath = value;
                this.NotifyPropertyChanged(this, nameof(this.SaveFolder));
            }
        }

        /// <summary>
        ///     Gets the select save folder.
        /// </summary>
        /// <value>The select save folder.</value>
        public ICommand SelectSaveFolder
        {
            get
            {
                return this.selectSaveFolder ?? (this.selectSaveFolder = new CommandHandler(this.ChooseSaveFolderLocation));
            }
        }

        /// <summary>
        ///     Gets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public ObservableCollection<IAuditTask> Tasks
        {
            get
            {
                return this.tasks ?? (this.tasks = new ObservableCollection<IAuditTask>(this.audit.Tasks));
            }
        }

        /// <summary>
        ///     Chooses the save folder location.
        /// </summary>
        private void ChooseSaveFolderLocation()
        {
            var folderDialog = new VistaFolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.UseDescriptionForTitle = true;
            folderDialog.Description = "Select the Save Folder Location";

            folderDialog.ShowDialog();

            this.audit.SavePath = folderDialog.SelectedPath;

            this.NotifyPropertyChanged(this, nameof(this.SaveFolder));
        }

        /// <summary>
        ///     Tasks the updated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     The progress of the task.</param>
        private void TaskUpdated(object sender, int e)
        {
            this.NotifyPropertyChanged(this, nameof(this.Tasks));
        }
    }
}