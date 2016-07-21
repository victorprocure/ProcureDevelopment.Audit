//-----------------------------------------------------------------------
// <copyright file="AuditConfigurationValidator.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using Configurations;

    /// <summary>
    ///     Validates a given configuration is valid
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    internal class AuditConfigurationValidator : ValidationRule
    {
        /// <summary>
        ///     When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">      The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var auditConfiguration = value as AuditConfiguration;
            if (auditConfiguration == null)
            {
                return new ValidationResult(false, "Did not receive a valid Audit Configuration");
            }

            var dateRule = new DateRule();
            dateRule.MaxYear = DateTime.Now.Year + 1;
            dateRule.MinYear = dateRule.MaxYear - 20;

            var dateResult = dateRule.Validate(auditConfiguration.AuditDate, CultureInfo.CurrentCulture);
            if (!dateResult.IsValid)
            {
                return dateResult;
            }

            var lengthRule = new LengthRule();
            lengthRule.Max = 30;
            lengthRule.Min = 2;

            var lengthResult = lengthRule.Validate(auditConfiguration.Location, CultureInfo.CurrentCulture);
            if (!lengthResult.IsValid)
            {
                return lengthResult;
            }

            lengthRule.Max = 2;
            lengthRule.Min = 2;

            lengthResult = lengthRule.Validate(auditConfiguration.Quarter, CultureInfo.CurrentCulture);

            if (!lengthResult.IsValid)
            {
                return lengthResult;
            }

            return new ValidationResult(true, null);
        }
    }
}