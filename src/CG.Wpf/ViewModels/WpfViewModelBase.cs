using CG.Mvvm.ViewModels;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace CG.Wpf.ViewModels
{
    /// <summary>
    /// This class is a base implementation of a view-model, for a WPF project.
    /// </summary>
    public class WpfViewModelBase : ViewModelBase
    {
        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method sets a property value and then calls <see cref="ViewModelBase.OnPropertyChanged(string)"/>
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingField">The backing field associated with the property.</param>
        /// <param name="value">The value to set in the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected override void SetValue<T>(
            ref T backingField,
            T value,
            [CallerMemberName] string propertyName = null
            )
        {
            // Is the new value same as the old value?
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return; // Nothing to do!
            }

            // Set the value of the backing field.
            backingField = value;

            // Send the event via the dispatcher.
            Application.Current.Dispatcher.Invoke(
                DispatcherPriority.Background,
                new ThreadStart(() => OnPropertyChanged(propertyName))
                );
        }

        #endregion
    }
}
