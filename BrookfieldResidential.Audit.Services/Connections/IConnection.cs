//-----------------------------------------------------------------------
// <copyright file="IConnection.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------

namespace BrookfieldResidentialProperties.Audit.Services.Connections
{
    using System.Security;
    using System.Windows.Controls;

    /// <summary>
    ///     Describes all connections that will be needed for the audit
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        ///     Gets a value indicating whether this instance can validate connection.
        /// </summary>
        /// <value><c>true</c> if this instance can validate connection; otherwise, <c>false</c>.</value>
        bool CanValidateConnection { get; }

        /// <summary>
        ///     Gets or sets the connection password.
        /// </summary>
        /// <value>The password for the connection.</value>
        SecureString Password { get; set; }

        /// <summary>
        ///     Gets or sets the name of or location of the server.
        /// </summary>
        /// <value>The name of/location of the server.</value>
        string ServerName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [use integrated security].
        /// </summary>
        /// <value><c>true</c> if [use integrated security]; otherwise, <c>false</c>.</value>
        bool UseIntegratedSecurity { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        string Username { get; set; }

        /// <summary>
        ///     Tests the connection.
        /// </summary>
        /// <returns><c>IsValid</c> if the connection was successfully tested</returns>
        ValidationResult TestConnection();
    }
}