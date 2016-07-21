//-----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace BrookfieldResidentialProperties.Audit.Wpf.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    ///     Handles commands where the action is void
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class CommandHandler : ICommand
    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Predicate<object> canExecute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandHandler" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public CommandHandler(Action action) : this(action, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandHandler" /> class.
        /// </summary>
        /// <param name="action">    The action.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the action is null</exception>
        public CommandHandler(Action action, Predicate<object> canExecute)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this
        ///     object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this
        ///     object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            this.action();
        }
    }
}