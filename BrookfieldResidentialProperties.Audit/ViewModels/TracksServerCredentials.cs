//-----------------------------------------------------------------------
// <copyright file="TracksServerCredentials.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Wpf.ViewModels
{
    using System.Security;
    using System.Windows;
    using System.Windows.Input;
    using Commands;
    using Services.Tasks;

    /// <summary>
    ///     Base class for all views that store and process server credentials
    /// </summary>
    /// <seealso cref="BrookfieldResidentialProperties.Audit.Wpf.ViewModels.ViewModelBase" />
    internal abstract class TracksServerCredentials : ViewModelBase
    {
        /// <summary>
        ///     The password changed command
        /// </summary>
        private ICommand passwordChanged;

        /// <summary>
        ///     The audit task
        /// </summary>
        private IAuditTask task;

        /// <summary>
        ///     The validate connection command
        /// </summary>
        private ICommand validateConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TracksServerCredentials" /> class.
        /// </summary>
        /// <param name="task">The task.</param>
        protected TracksServerCredentials(IAuditTask task)
        {
            this.Task = task;

            this.validateConnection = new CommandHandler(this.TestConnection, (object x) => { return this.Task.Connection.CanValidateConnection; });
        }

        /// <summary>
        ///     Gets a value indicating whether this instance can validate connection.
        /// </summary>
        /// <value><c>true</c> if this instance can validate connection; otherwise, <c>false</c>.</value>
        public bool CanValidateConnection
        {
            get
            {
                return this.Task.Connection.CanValidateConnection;
            }
        }

        /// <summary>
        ///     Gets the password changed.
        /// </summary>
        /// <value>The password changed.</value>
        public ICommand PasswordChanged
        {
            get
            {
                return this.passwordChanged ?? (this.passwordChanged = new RelayCommand(this.SetPassword));
            }
        }

        /// <summary>
        ///     Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName
        {
            get
            {
                return this.Task.Connection.ServerName;
            }

            set
            {
                this.Task.Connection.ServerName = value;
                this.NotifyPropertyChanged(this, nameof(this.ServerName));
                this.NotifyPropertyChanged(this, nameof(this.CanValidateConnection));
            }
        }

        /// <summary>
        ///     Gets or sets the task.
        /// </summary>
        /// <value>The task.</value>
        public IAuditTask Task
        {
            get
            {
                return this.task;
            }

            set
            {
                this.task = value;
                this.NotifyPropertyChanged(this, nameof(this.Task));
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [use integrated security].
        /// </summary>
        /// <value><c>true</c> if [use integrated security]; otherwise, <c>false</c>.</value>
        public virtual bool UseIntegratedSecurity
        {
            get
            {
                return this.Task.Connection.UseIntegratedSecurity;
            }

            set
            {
                this.Task.Connection.UseIntegratedSecurity = value;
                this.NotifyPropertyChanged(this, nameof(this.UseIntegratedSecurity));
                this.NotifyPropertyChanged(this, nameof(this.CanValidateConnection));
            }
        }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get
            {
                return this.Task.Connection.Username;
            }

            set
            {
                this.Task.Connection.Username = value;
                this.NotifyPropertyChanged(this, nameof(this.Username));
                this.NotifyPropertyChanged(this, nameof(this.CanValidateConnection));
            }
        }

        /// <summary>
        ///     Gets the validate connection.
        /// </summary>
        /// <value>The validate connection.</value>
        public virtual ICommand ValidateConnection
        {
            get
            {
                return this.validateConnection;
            }
        }

        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <param name="param">The parameter.</param>
        protected virtual void SetPassword(object param)
        {
            var securePassword = (SecureString)param;
            this.Task.Connection.Password = securePassword;

            this.NotifyPropertyChanged(this, nameof(this.CanValidateConnection));
        }

        /// <summary>
        ///     Tests the connection.
        /// </summary>
        private void TestConnection()
        {
            var validation = this.Task.TestConnection();

            if (!validation.IsValid)
            {
                MessageBox.Show((string)validation.ErrorContent, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}