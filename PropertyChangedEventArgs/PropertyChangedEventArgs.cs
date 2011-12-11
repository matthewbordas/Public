namespace BellaCode.ComponentModel
{
    using System.ComponentModel;

    /// <summary>
    /// Extends PropertyChangedEventArgs to provide old/new values when a value changes.
    /// </summary>
    /// <remarks>
    /// While PropertyChangingEventArgs can be used, it is inefficient to raise two events on every change.
    /// As well, remembering the old value between two different event handlers is problematic for calculates that want to ensure no invalid intermediate state.
    /// </remarks>
    /// <typeparam name="T">The type of the value that changed.</typeparam>
    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public PropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public T NewValue { get; private set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public T OldValue { get; private set; }
    }
}
