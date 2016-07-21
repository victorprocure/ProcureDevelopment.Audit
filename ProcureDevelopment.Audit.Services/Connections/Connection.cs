//-----------------------------------------------------------------------
// <copyright file="Connection.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Connections
{
    using System.Security;
    using System.Windows.Controls;

    /// <summary>
    ///     Base abstract class for all audit connections, contains generic methods that can be used
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Services.Connections.IConnection" />
    public abstract class Connection : IConnection
    {
        /// <summary>
        ///     Gets a value indicating whether this instance can validate connection.
        /// </summary>
        /// <value><c>true</c> if this instance can validate connection; otherwise, <c>false</c>.</value>
        public virtual bool CanValidateConnection
        {
            get
            {
                return this.TestConnection().IsValid;
            }
        }

        /// <summary>
        ///     Gets or sets the connection password.
        /// </summary>
        /// <value>The password for the connection.</value>
        public abstract SecureString Password { get; set; }

        /// <summary>
        ///     Gets or sets the name of or location of the server.
        /// </summary>
        /// <value>The name of/location of the server.</value>
        public abstract string ServerName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [use integrated security].
        /// </summary>
        /// <value><c>true</c> if [use integrated security]; otherwise, <c>false</c>.</value>
        public abstract bool UseIntegratedSecurity { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public abstract string Username { get; set; }

        /// <summary>
        ///     Tests the connection.
        /// </summary>
        /// <returns><c>IsValid</c> if the connection was successfully tested</returns>
        public abstract ValidationResult TestConnection();
    }
}