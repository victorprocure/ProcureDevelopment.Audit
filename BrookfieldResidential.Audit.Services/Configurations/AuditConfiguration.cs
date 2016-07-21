//-----------------------------------------------------------------------
// <copyright file="AuditConfiguration.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Services.Configurations
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using Validation;

    /// <summary>
    ///     Default audit configuration information
    /// </summary>
    /// <seealso cref="BrookfieldResidentialProperties.Audit.Services.Configurations.IAuditConfiguration" />
    public class AuditConfiguration : IAuditConfiguration
    {
        /// <summary>
        ///     Gets or sets the audit date.
        /// </summary>
        /// <value>The date the audit is being run for</value>
        public DateTime AuditDate { get; set; }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        /// <value>The location this audit is being run against</value>
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the quarter.
        /// </summary>
        /// <value>The quarter of the year this audit is run for</value>
        public string Quarter { get; set; }

        /// <summary>
        ///     The purpose of this method is to validate the configuration that has been provided
        /// </summary>
        /// <returns>A validation result based on any rules within the configuration</returns>
        public ValidationResult Build()
        {
            var validator = new AuditConfigurationValidator();

            return validator.Validate(this, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Runs the configuration, I'm actually not sure this is needed.
        /// </summary>
        /// <returns>True if it was successful</returns>
        /// <exception cref="System.NotImplementedException">
        ///     Thrown as this has not been implemented and will most likely be removed
        /// </exception>
        public bool Run()
        {
            throw new NotImplementedException();
        }
    }
}