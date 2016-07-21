//-----------------------------------------------------------------------
// <copyright file="DateRule.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    ///     Validates the year of a date given
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    internal class DateRule : ValidationRule
    {
        /// <summary>
        ///     Gets or sets the maximum year.
        /// </summary>
        /// <value>The maximum year.</value>
        public int MaxYear { get; set; }

        /// <summary>
        ///     Gets or sets the minimum year.
        /// </summary>
        /// <value>The minimum year.</value>
        public int MinYear { get; set; }

        /// <summary>
        ///     When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">      The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var date = (DateTime)value;
                if (date.Year < this.MinYear || date.Year > this.MaxYear)
                {
                    return new ValidationResult(false, $"Date must be between the years {this.MinYear} and {this.MaxYear}");
                }
            }
            catch (InvalidCastException ex)
            {
                return new ValidationResult(false, "Date received is not valid: " + ex.Message);
            }

            return new ValidationResult(true, null);
        }
    }
}