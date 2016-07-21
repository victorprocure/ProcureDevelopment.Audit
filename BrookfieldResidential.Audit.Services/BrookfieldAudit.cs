//-----------------------------------------------------------------------
// <copyright file="BrookfieldAudit.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Configurations;
    using Tasks;

    /// <summary>
    ///     Houses all audit tasks and configurations, runs all tasks when requested
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BrookfieldAudit : INotifyPropertyChanged
    {
        /// <summary>
        ///     The audit configuration
        /// </summary>
        private IAuditConfiguration configuration;

        /// <summary>
        ///     The path the audit is being saved to
        /// </summary>
        private string savePath;

        /// <summary>
        ///     The collection of all the tasks to be run
        /// </summary>
        private ICollection<IAuditTask> tasks;

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
                return this.tasks.Any(s => s.CanExecute) && (this.configuration != null) && this.configuration.Build().IsValid;
            }
        }

        /// <summary>
        ///     Gets or sets the audit save path.
        /// </summary>
        /// <value>The audit save path.</value>
        public string SavePath
        {
            get
            {
                return this.savePath;
            }

            set
            {
                this.savePath = value;
                this.NotifyPropertyChanged(nameof(this.SavePath));
            }
        }

        /// <summary>
        ///     Gets the collection of audit tasks.
        /// </summary>
        /// <value>The audit tasks.</value>
        public ICollection<IAuditTask> Tasks
        {
            get
            {
                return this.tasks;
            }
        }

        /// <summary>
        ///     Adds the configuration to the current audit.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     Thrown if the configuration given is null
        /// </exception>
        public void AddConfiguration(IAuditConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.configuration = configuration;
        }

        /// <summary>
        ///     Adds an audit task to the list
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>An audit task if it was already found in the list of tasks</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when no task given</exception>
        public IAuditTask AddTask(IAuditTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (this.tasks == null)
            {
                this.tasks = new List<IAuditTask>();
            }

            var searchTask = this.tasks.FirstOrDefault(s => s.GetType() == task.GetType());

            if (searchTask == null)
            {
                this.tasks.Add(task);
                searchTask = task;
            }

            return searchTask;
        }

        /// <summary>
        ///     Runs all the audit tasks in the list
        /// </summary>
        public void RunAll()
        {
            foreach (var task in this.tasks)
            {
                task.Run();
            }
        }

        /// <summary>
        ///     Notifies the property changed.
        /// </summary>
        /// <param name="v">The name of the property that was changed</param>
        private void NotifyPropertyChanged(string v)
        {
            var handler = this.PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}