//-----------------------------------------------------------------------
// <copyright file="LengthRule.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services.Validation
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    ///     Validation rule that measures string length to a given min/max
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    internal class LengthRule : ValidationRule
    {
        /// <summary>
        ///     The maximum length of the string
        /// </summary>
        private int max;

        /// <summary>
        ///     The minimum length of the string
        /// </summary>
        private int min;

        /// <summary>
        ///     Gets or sets the maximum length of the string.
        /// </summary>
        /// <value>The maximum length of the string.</value>
        public int Max
        {
            get
            {
                return this.max;
            }

            set
            {
                this.max = value;
            }
        }

        /// <summary>
        ///     Gets or sets the minimum length of the string.
        /// </summary>
        /// <value>The minimum length of the string.</value>
        public int Min
        {
            get
            {
                return this.min;
            }

            set
            {
                this.min = value;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">      The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int length = 0;

            try
            {
                length = ((string)value).Length;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((length < this.Min) || (length > this.Max))
            {
                return new ValidationResult(false, $"Length must be between {Min} and {Max} characters");
            }

            return new ValidationResult(true, null);
        }
    }
}