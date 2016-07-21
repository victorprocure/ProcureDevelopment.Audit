//-----------------------------------------------------------------------
// <copyright file="ConnectionValidator.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Validation
{
    using System.Globalization;
    using System.Windows.Controls;
    using Connections;

    /// <summary>
    ///     Base class for all connection validators
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    internal abstract class ConnectionValidator : ValidationRule
    {
        /// <summary>
        ///     Gets or sets the password length maximum.
        /// </summary>
        /// <value>The password length maximum.</value>
        public int PasswordLengthMax { get; set; }

        /// <summary>
        ///     Gets or sets the password length minimum.
        /// </summary>
        /// <value>The password length minimum.</value>
        public int PasswordLengthMin { get; set; }

        /// <summary>
        ///     Gets or sets the server name length maximum.
        /// </summary>
        /// <value>The server name length maximum.</value>
        public int ServerNameLengthMax { get; set; }

        /// <summary>
        ///     Gets or sets the server name length minimum.
        /// </summary>
        /// <value>The server name length minimum.</value>
        public int ServerNameLengthMin { get; set; }

        /// <summary>
        ///     Gets or sets the username length maximum.
        /// </summary>
        /// <value>The username length maximum.</value>
        public int UsernameLengthMax { get; set; }

        /// <summary>
        ///     Gets or sets the username length minimum.
        /// </summary>
        /// <value>The username length minimum.</value>
        public int UsernameLengthMin { get; set; }

        /// <summary>
        ///     When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">      The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var connection = value as Connection;
            if (connection == null)
            {
                return new ValidationResult(false, "Not a valid connection");
            }

            var serverNameRule = new LengthRule { Min = this.ServerNameLengthMin, Max = this.ServerNameLengthMax };
            var usernameRule = new LengthRule { Min = this.UsernameLengthMin, Max = this.UsernameLengthMax };

            var serverNameResult = serverNameRule.Validate(connection.ServerName, cultureInfo);

            if (!serverNameResult.IsValid)
            {
                return serverNameResult;
            }

            if (!connection.UseIntegratedSecurity)
            {
                var usernameResult = usernameRule.Validate(connection.Username, cultureInfo);
                if (!usernameResult.IsValid)
                {
                    return usernameResult;
                }
            }

            return this.CanConnect();
        }

        /// <summary>
        ///     Determines whether this instance can connect.
        /// </summary>
        /// <returns>IsValid if the connection can be made</returns>
        protected abstract ValidationResult CanConnect();
    }
}