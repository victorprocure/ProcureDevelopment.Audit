﻿//-----------------------------------------------------------------------
// <copyright file="SageAuditTask.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Tasks
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Controls;
    using Connections;

    /// <summary>
    ///     Describes the Sage 300 Audit task
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Services.Tasks.IAuditTask" />
    public class SageAuditTask : IAuditTask
    {
        /// <summary>
        ///     The connection to the pervasive database for sage
        /// </summary>
        private SageConnection connection;

        /// <summary>
        ///     Track the task progress internally
        /// </summary>
        private int taskProgress;

        /// <summary>
        ///     Occurs when [progress updated].
        /// </summary>
        public event EventHandler<int> ProgressUpdated;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets a value indicating whether this instance can execute.
        /// </summary>
        /// <value><c>true</c> if this instance can execute; otherwise, <c>false</c>.</value>
        public bool CanExecute
        {
            get
            {
                return this.Connection.TestConnection().IsValid;
            }
        }

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <value>The connection the audit task will be using.</value>
        public IConnection Connection
        {
            get
            {
                return this.connection ?? (this.connection = new SageConnection());
            }
        }

        /// <summary>
        ///     Gets a value indicating whether [task complete].
        /// </summary>
        /// <value><c>true</c> if [task complete]; otherwise, <c>false</c>.</value>
        public bool TaskComplete
        {
            get
            {
                return this.TaskProgress == 100;
            }
        }

        /// <summary>
        ///     Gets the name of the task.
        /// </summary>
        /// <value>The name of the task used to display to user.</value>
        public string TaskName
        {
            get
            {
                return "Sage Audit";
            }
        }

        /// <summary>
        ///     Gets the task progress.
        /// </summary>
        /// <value>The progress of the task currently, if being run. If not 0</value>
        public int TaskProgress
        {
            get
            {
                return this.taskProgress;
            }

            private set
            {
                this.taskProgress = value;
                this.NotifyPropertyChanged(nameof(this.TaskProgress));
            }
        }

        /// <summary>
        ///     Runs this task
        /// </summary>
        public void Run()
        {
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += this.DoWork;
            worker.ProgressChanged += this.ProgressChanged;

            worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Wrapper method for testing the internal connection associated with this task
        /// </summary>
        /// <returns>ValidationResult.IsValid will be true if a connection can be made</returns>
        public ValidationResult TestConnection()
        {
            return this.Connection.TestConnection();
        }

        /// <summary>
        ///     Called when [progress updated].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnProgressUpdated(int e)
        {
            this.TaskProgress = e;

            var handler = this.ProgressUpdated;
            handler?.Invoke(this, e);
        }

        /// <summary>
        ///     Processes the audit task on a background thread using a worker
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="DoWorkEventArgs" /> instance containing the event data.
        /// </param>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            for (int i = 0; i < 100; i++)
            {
                worker.ReportProgress(i);
                Thread.Sleep(50);
            }
        }

        /// <summary>
        ///     Notifies the property changed.
        /// </summary>
        /// <param name="v">The control that had its property changed.</param>
        private void NotifyPropertyChanged(string v)
        {
            var handler = this.PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        /// <summary>
        ///     Progresses the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="ProgressChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.TaskProgress = e.ProgressPercentage;
        }
    }
}