﻿//-----------------------------------------------------------------------
// <copyright file="SageViewModel.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------

namespace ProcureDevelopment.Audit.Wpf.ViewModels
{
    using System.Windows.Input;
    using Commands;
    using Ookii.Dialogs.Wpf;
    using Services;
    using Services.Tasks;

    /// <summary>
    ///     Handles user interaction while setting up Sage task
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Wpf.ViewModels.TracksServerCredentials" />
    internal class SageViewModel : TracksServerCredentials
    {
        /// <summary>
        ///     The audit
        /// </summary>
        private Audit audit;

        /// <summary>
        ///     The show data folder
        /// </summary>
        private ICommand showDataFolder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SageViewModel" /> class.
        /// </summary>
        /// <param name="audit">The audit.</param>
        public SageViewModel(Audit audit) : base(new SageAuditTask())
        {
            this.ViewTitle = "Sage 300 (Timberline)";
            this.audit = audit;

            this.audit.AddTask(this.Task);
        }

        /// <summary>
        ///     Gets the show data folder.
        /// </summary>
        /// <value>The show data folder.</value>
        public ICommand ShowDataFolder
        {
            get
            {
                return this.showDataFolder ?? (this.showDataFolder = new CommandHandler(this.ChooseDataFolderLocation));
            }
        }

        /// <summary>
        ///     Chooses the data folder location.
        /// </summary>
        private void ChooseDataFolderLocation()
        {
            var folderDialog = new VistaFolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.UseDescriptionForTitle = true;
            folderDialog.Description = "Select the Data Folder Location";

            folderDialog.ShowDialog();

            this.ServerName = folderDialog.SelectedPath;
        }
    }
}