//-----------------------------------------------------------------------
// <copyright file="SageConnection.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services.Connections
{
    using System;
    using System.Globalization;
    using System.Security;
    using System.Windows.Controls;
    using Validation;

    /// <summary>
    ///     The default sage 300 connection for audit
    /// </summary>
    /// <seealso cref="BrookfieldResidentialProperties.Audit.Services.Connections.Connection" />
    public class SageConnection : Connection
    {
        /// <summary>
        ///     Gets or sets the connection password.
        /// </summary>
        /// <value>The password for the connection.</value>
        public override SecureString Password { get; set; }

        /// <summary>
        ///     Gets or sets the name of or location of the server.
        /// </summary>
        /// <value>The name of/location of the server.</value>
        public override string ServerName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [use integrated security].
        /// </summary>
        /// <value><c>true</c> if [use integrated security]; otherwise, <c>false</c>.</value>
        /// <exception cref="System.NotImplementedException">
        ///     Thrown as sage does not have the ability to use integrated security
        /// </exception>
        public override bool UseIntegratedSecurity
        {
            get
            {
                return false;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public override string Username { get; set; }

        /// <summary>
        ///     Tests the connection.
        /// </summary>
        /// <returns><c>IsValid</c> if the connection was successfully tested</returns>
        public override ValidationResult TestConnection()
        {
            var validator = new SageConnectionValidator();
            return validator.Validate(this, CultureInfo.CurrentCulture);
        }
    }
}