//-----------------------------------------------------------------------
// <copyright file="NavigationButton.xaml.cs" company="Brookfield Residential Properties">
//     Copyright (c) Brookfield Residential Properties. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------

namespace BrookfieldResidentialProperties.Audit.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using FontAwesome.WPF;

    /// <summary>
    ///     Interaction logic for Navigation Button
    /// </summary>
    public partial class NavigationButton : UserControl, ICommandSource
    {
        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(NavigationButton),
                new PropertyMetadata(null));

        /// <summary>
        ///     The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
                    DependencyProperty.Register(
                        "Command",
                        typeof(ICommand),
                        typeof(NavigationButton),
                        new PropertyMetadata(default(ICommand), OnCommandChanged));

        /// <summary>
        ///     The command target property
        /// </summary>
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                "CommandTarget",
                typeof(IInputElement),
                typeof(NavigationButton),
                new PropertyMetadata(null));

        /// <summary>
        ///     The disabled color property
        /// </summary>
        public static readonly DependencyProperty DisabledColorProperty =
            DependencyProperty.Register(
                "DisabledColor",
                typeof(Brush),
                typeof(NavigationButton),
                new PropertyMetadata(Brushes.DarkGray));

        /// <summary>
        ///     The hover color property
        /// </summary>
        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register(
                "HoverColor",
                typeof(Brush),
                typeof(NavigationButton),
                new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        /// <summary>
        ///     The icon property
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                "Icon",
                typeof(FontAwesomeIcon),
                typeof(NavigationButton),
                new PropertyMetadata(FontAwesomeIcon.Flag));

        /// <summary>
        ///     The label property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                "Label",
                typeof(string),
                typeof(NavigationButton),
                new PropertyMetadata("Label"));

        /// <summary>
        ///     The can this navigation button execute
        /// </summary>
        private bool canExecute;

        /// <summary>
        ///     The can execute changed handler
        /// </summary>
        private EventHandler canExecuteChangedHandler;

        /// <summary>
        ///     The icon size
        /// </summary>
        private int iconSize;

        /// <summary>
        ///     The label size
        /// </summary>
        private int labelSize;

        /// <summary>
        ///     The original foreground
        /// </summary>
        private Brush originalForeground;

        /// <summary>
        ///     The was mouse down on control
        /// </summary>
        private bool wasMouseDownOnControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationButton" /> class.
        /// </summary>
        public NavigationButton()
        {
            this.InitializeComponent();

            this.labelSize = (int)Math.Round(this.Height / 1.30, 0);
            this.iconSize = (int)Math.Round(this.Height / 1.70, 0);

            this.Cursor = Cursors.Hand;

            this.IsEnabledChanged += this.OnIsEnabledChanged;

            this.originalForeground = this.Foreground;
        }

        /// <summary>
        ///     Gets or sets the command. Gets the command that will be executed when the command
        ///     source is invoked.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the Command Parameter. Represents a user defined data value that can be
        ///     passed to the command when it is executed.
        /// </summary>
        public object CommandParameter
        {
            get { return this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the object that the command is being executed on.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (IInputElement)this.GetValue(CommandTargetProperty); }
            set { this.SetValue(CommandTargetProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the color of the disabled.
        /// </summary>
        /// <value>The color of the disabled.</value>
        public Brush DisabledColor
        {
            get { return (Brush)this.GetValue(DisabledColorProperty); }
            set { this.SetValue(DisabledColorProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        public Brush HoverColor
        {
            get { return (Brush)this.GetValue(HoverColorProperty); }
            set { this.SetValue(HoverColorProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        /// <summary>
        ///     Gets the size of the icon.
        /// </summary>
        /// <value>The size of the icon.</value>
        public int IconSize
        {
            get
            {
                return (int)(this.ActualHeight * 0.60);
            }
        }

        /// <summary>
        ///     Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        /// <summary>
        ///     Gets the size of the label.
        /// </summary>
        /// <value>The size of the label.</value>
        public int LabelSize
        {
            get
            {
                return (int)(this.ActualHeight * 0.25);
            }
        }

        /// <summary>
        ///     Gets a value that becomes the return value of
        ///     <see cref="P:System.Windows.UIElement.IsEnabled" /> in derived classes.
        /// </summary>
        protected override bool IsEnabledCore
        {
            get
            {
                return base.IsEnabledCore && this.canExecute;
            }
        }

        /// <summary>
        ///     Invoked when an unhandled
        ///     <see cref="E:System.Windows.UIElement.MouseLeftButtonDown" /> routed event is raised
        ///     on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the
        ///     event data. The event data reports that the left mouse button was pressed.
        /// </param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.wasMouseDownOnControl = true;
        }

        /// <summary>
        ///     Invoked when an unhandled
        ///     <see cref="E:System.Windows.UIElement.MouseLeftButtonUp" /> routed event reaches an
        ///     element in its route that is derived from this class. Implement this method to add
        ///     class handling for this event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the
        ///     event data. The event data reports that the left mouse button was released.
        /// </param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.wasMouseDownOnControl)
            {
                var command = this.Command;
                var parameter = this.CommandParameter;
                var target = this.CommandTarget;

                var routedCmd = command as RoutedCommand;
                if (routedCmd != null && routedCmd.CanExecute(parameter, target))
                {
                    routedCmd.Execute(parameter, target);
                }
                else if (command != null && command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }

                this.wasMouseDownOnControl = false;
            }
        }

        /// <summary>
        ///     Called when [command changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NavigationButton)d;

            control.Command = (ICommand)e.NewValue;

            control.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        /// <summary>
        ///     Adds the command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void AddCommand(ICommand command)
        {
            var handler = new EventHandler(this.CanExecuteChanged);
            this.canExecuteChangedHandler = handler;

            if (command != null)
            {
                command.CanExecuteChanged += this.canExecuteChangedHandler;
            }
        }

        /// <summary>
        ///     Determines whether this instance [can execute changed] the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (this.Command != null)
            {
                var command = this.Command as RoutedCommand;

                if (command != null)
                {
                    this.canExecute = command.CanExecute(this.CommandParameter, this.CommandTarget);
                }
                else
                {
                    this.canExecute = this.Command.CanExecute(this.CommandParameter);
                }
            }

            this.CoerceValue(NavigationButton.IsEnabledProperty);
        }

        /// <summary>
        ///     Hooks up command.
        /// </summary>
        /// <param name="oldCommand">The old command.</param>
        /// <param name="newCommand">The new command.</param>
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                this.RemoveCommand(oldCommand);
            }

            this.AddCommand(newCommand);
        }

        /// <summary>
        ///     Called when [is enabled changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                this.SetValue(NavigationButton.ForegroundProperty, this.originalForeground);
            }
            else
            {
                this.SetValue(NavigationButton.ForegroundProperty, this.DisabledColor);
            }
        }

        /// <summary>
        ///     Called when [mouse].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="MouseEventArgs" /> instance containing the event data.
        /// </param>
        private void OnMouse(object sender, MouseEventArgs e)
        {
            var control = (StackPanel)sender;

            var currentHoverColor = this.HoverColor;

            this.HoverColor = this.Foreground;

            this.Foreground = currentHoverColor;
        }

        /// <summary>
        ///     Removes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void RemoveCommand(ICommand command)
        {
            EventHandler handler = this.CanExecuteChanged;
            command.CanExecuteChanged -= handler;
        }
    }
}