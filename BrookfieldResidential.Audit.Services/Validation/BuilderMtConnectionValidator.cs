//-----------------------------------------------------------------------
// <copyright file="BuilderMtConnectionValidator.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Validation
{
    using System.Windows.Controls;

    /// <summary>
    ///     Validates a BuilderMT connection is correctly setup
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Services.Validation.ConnectionValidator" />
    internal class BuilderMtConnectionValidator : ConnectionValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BuilderMtConnectionValidator" /> class.
        /// </summary>
        public BuilderMtConnectionValidator()
        {
            this.PasswordLengthMax = 32;
            this.PasswordLengthMin = 6;
            this.UsernameLengthMax = 32;
            this.UsernameLengthMin = 2;
            this.ServerNameLengthMax = 35;
            this.ServerNameLengthMin = 1;
        }

        /// <summary>
        ///     Determines whether this instance can connect.
        /// </summary>
        /// <returns>IsValid if the connection can be made</returns>
        protected override ValidationResult CanConnect()
        {
            return new ValidationResult(true, null);
        }
    }
}