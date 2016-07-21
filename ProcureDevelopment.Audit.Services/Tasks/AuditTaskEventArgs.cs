//-----------------------------------------------------------------------
// <copyright file="AuditTaskEventArgs.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Tasks
{
    using System;

    /// <summary>
    ///     Used to pass event arguments from audit tasks
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class AuditTaskEventArgs : EventArgs
    {
        /// <summary>
        ///     The message the user will see displayed
        /// </summary>
        private string message;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuditTaskEventArgs" /> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public AuditTaskEventArgs(string msg)
        {
            this.message = msg;
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message the user will see displayed.</value>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
            }
        }
    }
}