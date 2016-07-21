//-----------------------------------------------------------------------
// <copyright file="SageConnectionValidator.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services.Validation
{
    using System.Windows.Controls;

    /// <summary>
    ///     Validates the sage 300 connection
    /// </summary>
    /// <seealso cref="BrookfieldResidentialProperties.Audit.Services.Validation.ConnectionValidator" />
    internal class SageConnectionValidator : ConnectionValidator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SageConnectionValidator" /> class.
        /// </summary>
        public SageConnectionValidator()
        {
            this.PasswordLengthMax = 32;
            this.PasswordLengthMin = 6;
            this.UsernameLengthMax = 32;
            this.UsernameLengthMin = 2;
            this.ServerNameLengthMax = 150;
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