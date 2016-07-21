//-----------------------------------------------------------------------
// <copyright file="ConfigurationViewModel.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------

namespace ProcureDevelopment.Audit.Wpf.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using Commands;
    using Services;
    using Services.Configurations;

    /// <summary>
    ///     Handles the audit configuration
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Wpf.ViewModels.ViewModelBase" />
    internal class ConfigurationViewModel : ViewModelBase
    {
        /// <summary>
        ///     The audit this configuration will apply to
        /// </summary>
        private Audit audit;

        /// <summary>
        ///     The audit date
        /// </summary>
        private DateTime auditDate;

        /// <summary>
        ///     The create templates
        /// </summary>
        private ICommand createTemplates;

        /// <summary>
        ///     The location
        /// </summary>
        private string location;

        /// <summary>
        ///     The quarter
        /// </summary>
        private string quarter;

        /// <summary>
        ///     The quarters
        /// </summary>
        private List<string> quarters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationViewModel" /> class.
        /// </summary>
        /// <param name="audit">The audit.</param>
        public ConfigurationViewModel(Audit audit)
        {
            this.ViewTitle = "Configure Audit";

            this.auditDate = DateTime.Now;
            this.createTemplates = new CommandHandler(this.BuildConfiguration, (object x) => { return this.CanPrepareTemplates(); });
            this.quarters = Services.Templating.Quarters.GetQuarters();

            this.audit = audit;
        }

        /// <summary>
        ///     Gets the create templates.
        /// </summary>
        /// <value>The create templates.</value>
        public ICommand CreateTemplates
        {
            get
            {
                return this.createTemplates;
            }
        }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location
        {
            get
            {
                return this.location;
            }

            set
            {
                this.location = value;

                this.NotifyPropertyChanged(this, nameof(this.Location));
            }
        }

        /// <summary>
        ///     Gets or sets the quarter.
        /// </summary>
        /// <value>The quarter.</value>
        public string Quarter
        {
            get
            {
                return this.quarter ?? this.quarters[0];
            }

            set
            {
                this.quarter = value;

                this.NotifyPropertyChanged(this, nameof(this.Quarter));
            }
        }

        /// <summary>
        ///     Gets the quarters.
        /// </summary>
        /// <value>The quarters.</value>
        public List<string> Quarters
        {
            get
            {
                return this.quarters;
            }
        }

        /// <summary>
        ///     Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public string Year
        {
            get
            {
                return this.auditDate.Year.ToString();
            }

            set
            {
                this.auditDate = DateTime.Parse(string.Format("01-01-{0}", value));

                this.NotifyPropertyChanged(this, nameof(this.Year));
            }
        }

        /// <summary>
        ///     Builds the configuration.
        /// </summary>
        private void BuildConfiguration()
        {
            var config = new AuditConfiguration();
            config.Location = this.location;
            config.AuditDate = this.auditDate;
            config.Quarter = this.Quarter;

            var preparationResult = config.Build();

            if (!preparationResult.IsValid)
            {
                MessageBox.Show((string)preparationResult.ErrorContent, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            this.audit.AddConfiguration(config);
        }

        /// <summary>
        ///     Determines whether this instance [can prepare templates].
        /// </summary>
        /// <returns><c>true</c> if the templates can be prepared</returns>
        private bool CanPrepareTemplates()
        {
            return !string.IsNullOrWhiteSpace(this.location) && this.auditDate != default(DateTime) && !string.IsNullOrWhiteSpace(this.quarter);
        }
    }
}