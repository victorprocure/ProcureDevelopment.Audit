//-----------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Wpf.ViewModels
{
    using System.ComponentModel;

    /// <summary>
    ///     Base class all views use, has common methods used by all
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     The view's title
        /// </summary>
        private string viewTitle;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets the view title.
        /// </summary>
        /// <value>The view title.</value>
        public string ViewTitle
        {
            get
            {
                return this.viewTitle;
            }

            set
            {
                this.viewTitle = value;
                this.NotifyPropertyChanged(this, nameof(this.ViewTitle));
            }
        }

        /// <summary>
        ///     Notifies the property changed.
        /// </summary>
        /// <param name="sender"> The sender.</param>
        /// <param name="control">The control.</param>
        protected virtual void NotifyPropertyChanged(object sender, string control)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(sender, new PropertyChangedEventArgs(control));
        }
    }
}