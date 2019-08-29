// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ImageSearcher.Components
{
    /// <summary>
    /// Represents a command that can perform a given action.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> commandExecuteAction;
        private readonly Func<bool> commandCanExecute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">
        /// The action to execute when called.
        /// </param>
        /// <param name="canExecute">
        /// The function to call to determine if the command can execute the action.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the execute action is null.
        /// </exception>
        public DelegateCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            commandExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
            commandCanExecute = canExecute ?? (() => true);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// The parameter used by the command.
        /// </param>
        /// <returns>
        /// Returns a value indicating whether this command can be executed.
        /// </returns>
        public bool CanExecute(object parameter = null)
        {
            try
            {
                return commandCanExecute();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// The parameter used by the command.
        /// </param>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            try
            {
                commandExecuteAction(parameter);
            }
            catch
            {
                Debugger.Break();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
