//-----------------------------------------------------------------------
// <copyright file="IAuditTask.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------

namespace ProcureDevelopment.Audit.Services.Tasks
{
    using System;
    using System.ComponentModel;
    using System.Windows.Controls;
    using Connections;

    /// <summary>
    ///     Describes how all audit tasks will function
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IAuditTask : INotifyPropertyChanged
    {
        /// <summary>
        ///     Occurs when [progress updated].
        /// </summary>
        event EventHandler<int> ProgressUpdated;

        /// <summary>
        ///     Gets a value indicating whether this instance can execute.
        /// </summary>
        /// <value><c>true</c> if this instance can execute; otherwise, <c>false</c>.</value>
        bool CanExecute { get; }

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <value>The connection the audit task will be using.</value>
        IConnection Connection { get; }

        /// <summary>
        ///     Gets the name of the task.
        /// </summary>
        /// <value>The name of the task used to display to user.</value>
        string TaskName { get; }

        /// <summary>
        ///     Gets the task progress.
        /// </summary>
        /// <value>The progress of the task currently, if being run. If not 0</value>
        int TaskProgress { get; }

        /// <summary>
        ///     Runs this task
        /// </summary>
        void Run();

        /// <summary>
        ///     Wrapper method for testing the internal connection associated with this task
        /// </summary>
        /// <returns>ValidationResult.IsValid will be true if a connection can be made</returns>
        ValidationResult TestConnection();
    }
}