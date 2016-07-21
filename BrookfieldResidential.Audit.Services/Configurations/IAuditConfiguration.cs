//-----------------------------------------------------------------------
// <copyright file="IAuditConfiguration.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Configurations
{
    using System.Windows.Controls;

    /// <summary>
    ///     This interface describes any current and possible future configurations for the audit system
    /// </summary>
    public interface IAuditConfiguration
    {
        /// <summary>
        ///     The purpose of this method is to validate the configuration that has been provided
        /// </summary>
        /// <returns>A validation result based on any rules within the configuration</returns>
        ValidationResult Build();

        /// <summary>
        ///     Runs the configuration, I'm actually not sure this is needed.
        /// </summary>
        /// <returns>True if it was successful</returns>
        bool Run();
    }
}