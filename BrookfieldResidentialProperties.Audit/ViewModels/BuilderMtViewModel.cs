//-----------------------------------------------------------------------
// <copyright file="BuilderMtViewModel.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Wpf.ViewModels
{
    using Services;
    using Services.Tasks;

    /// <summary>
    ///     View model for builderMt task setup
    /// </summary>
    /// <seealso cref="ProcureDevelopment.Audit.Wpf.ViewModels.TracksServerCredentials" />
    internal class BuilderMtViewModel : TracksServerCredentials
    {
        /// <summary>
        ///     The audit the task will be added to
        /// </summary>
        private Audit audit;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BuilderMtViewModel" /> class.
        /// </summary>
        /// <param name="audit">The audit.</param>
        public BuilderMtViewModel(Audit audit) : base(new BuilderMtAuditTask())
        {
            this.ViewTitle = "BuilderMT";
            this.audit = audit;

            this.Task = this.audit.AddTask(this.Task);
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is credentials enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is credentials enabled; otherwise, <c>false</c>.</value>
        public bool IsCredentialsEnabled
        {
            get
            {
                return !this.UseIntegratedSecurity;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [use integrated security].
        /// </summary>
        /// <value><c>true</c> if [use integrated security]; otherwise, <c>false</c>.</value>
        public sealed override bool UseIntegratedSecurity
        {
            get
            {
                return base.UseIntegratedSecurity;
            }

            set
            {
                base.UseIntegratedSecurity = value;

                this.NotifyPropertyChanged(this, nameof(this.UseIntegratedSecurity));
                this.NotifyPropertyChanged(this, nameof(this.IsCredentialsEnabled));
                this.NotifyPropertyChanged(this, nameof(this.CanValidateConnection));
            }
        }
    }
}