//-----------------------------------------------------------------------
// <copyright file="TextBoxInputRegExBehaviour.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Wpf.Validation
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    ///     Influence how a textbox reacts to user input using regular expressions
    /// </summary>
    /// <seealso cref="System.Windows.Interactivity.Behavior{System.Windows.Controls.TextBox}" />
    public class TextBoxInputRegExBehaviour : Behavior<TextBox>
    {
        /// <summary>
        ///     The empty value property
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register(
                "EmptyValue",
                typeof(string),
                typeof(TextBoxInputRegExBehaviour),
                null);

        /// <summary>
        ///     The maximum length property
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register(
                "MaxLength",
                typeof(int),
                typeof(TextBoxInputRegExBehaviour),
                new FrameworkPropertyMetadata(int.MinValue));

        /// <summary>
        ///     The regular expression property
        /// </summary>
        public static readonly DependencyProperty RegularExpressionProperty =
                            DependencyProperty.Register(
                                "RegularExpression",
                                typeof(string),
                                typeof(TextBoxInputRegExBehaviour),
                                new FrameworkPropertyMetadata(".*"));

        /// <summary>
        ///     Gets or sets the empty value.
        /// </summary>
        /// <value>The empty value.</value>
        public string EmptyValue
        {
            get { return (string)this.GetValue(EmptyValueProperty); }
            set { this.SetValue(EmptyValueProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public int MaxLength
        {
            get { return (int)this.GetValue(MaxLengthProperty); }
            set { this.SetValue(MaxLengthProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the regular expression.
        /// </summary>
        /// <value>The regular expression.</value>
        public string RegularExpression
        {
            get { return (string)this.GetValue(RegularExpressionProperty); }
            set { this.SetValue(RegularExpressionProperty, value); }
        }

        /// <summary>
        ///     Attach our behavior. Add event handlers
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewTextInput += this.PreviewTextInputHandler;
            this.AssociatedObject.PreviewKeyDown += this.PreviewKeyDownHandler;
            DataObject.AddPastingHandler(this.AssociatedObject, this.PastingHandler);
        }

        /// <summary>
        ///     Detach our behavior. remove event handlers
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewTextInput -= this.PreviewTextInputHandler;
            this.AssociatedObject.PreviewKeyDown -= this.PreviewKeyDownHandler;
            DataObject.RemovePastingHandler(this.AssociatedObject, this.PastingHandler);
        }

        /// <summary>
        ///     Handles input when it is pasted into the textbox
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="DataObjectPastingEventArgs" /> instance containing the event data.
        /// </param>
        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!this.ValidateText(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        /// <summary>
        ///     PreviewKeyDown event handler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="KeyEventArgs" /> instance containing the event data.
        /// </param>
        private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.EmptyValue))
            {
                return;
            }

            string text = null;

            // Handle basic key presses
            if (e.Key == Key.Back)
            {
                if (!this.TreatSelectedText(out text))
                {
                    if (this.AssociatedObject.SelectionStart > 0)
                    {
                        text = this.AssociatedObject.Text.Remove(this.AssociatedObject.SelectionStart - 1, 1);
                    }
                }
            }
            else if (e.Key == Key.Delete)
            {
                // If text was selected, delete it
                if (!this.TreatSelectedText(out text) && this.AssociatedObject.Text.Length > this.AssociatedObject.SelectionStart)
                {
                    // Otherwise delete next symbol
                    text = this.AssociatedObject.Text.Remove(this.AssociatedObject.SelectionStart, 1);
                }
            }

            if (text == string.Empty)
            {
                this.AssociatedObject.Text = this.EmptyValue;

                if (e.Key == Key.Back)
                {
                    this.AssociatedObject.SelectionStart++;
                }

                e.Handled = true;
            }
        }

        /// <summary>
        ///     Previews the text input handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     
        ///     The <see cref="TextCompositionEventArgs" /> instance containing the event data.
        /// </param>
        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            string text;
            if (this.AssociatedObject.Text.Length < this.AssociatedObject.CaretIndex)
            {
                text = this.AssociatedObject.Text;
            }
            else
            {
                // Remaining text after removing selected text.
                string remainingTextAfterRemoveSelection;

                text = this.TreatSelectedText(out remainingTextAfterRemoveSelection)
                    ? remainingTextAfterRemoveSelection.Insert(this.AssociatedObject.SelectionStart, e.Text)
                    : this.AssociatedObject.Text.Insert(this.AssociatedObject.CaretIndex, e.Text);
            }

            e.Handled = !this.ValidateText(text);
        }

        /// <summary>
        ///     Handle text selection
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>true if the character was successfully removed; otherwise, false.</returns>
        private bool TreatSelectedText(out string text)
        {
            text = null;
            if (this.AssociatedObject.SelectionLength <= 0)
            {
                return false;
            }

            var length = this.AssociatedObject.Text.Length;
            if (this.AssociatedObject.SelectionStart >= length)
            {
                return true;
            }

            if (this.AssociatedObject.SelectionStart + this.AssociatedObject.SelectionLength >= length)
            {
                this.AssociatedObject.SelectionLength = length - this.AssociatedObject.SelectionStart;
            }

            text = this.AssociatedObject.Text.Remove(this.AssociatedObject.SelectionStart, this.AssociatedObject.SelectionLength);
            return true;
        }

        /// <summary>
        ///     Validate certain text by our regular expression and text length conditions
        /// </summary>
        /// <param name="text">Text for validation</param>
        /// <returns>True - valid, False - invalid</returns>
        private bool ValidateText(string text)
        {
            return (new Regex(this.RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text) && (this.MaxLength == 0 || text.Length <= this.MaxLength);
        }
    }
}