using System;
using System.Runtime.Serialization;

namespace Employees.DataStore.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Any error related to wrong or missed configuration
    /// </summary>
    [Serializable]
    public class ConfigurationException : ApplicationException
    {
        /// <inheritdoc />
        public ConfigurationException()
        { }

        /// <inheritdoc />
        public ConfigurationException(string message) : base(message)
        { }

        /// <inheritdoc />
        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <inheritdoc />
        public ConfigurationException(string sectionName, string key)
            : base($"Configuration is wrong. Section name is '{sectionName ?? "root"}' key name is '{key}'")
        {
            ConfigurationSection = sectionName ?? "root";
            ConfigurationKey = key;
        }

        /// <inheritdoc />
        public ConfigurationException(string sectionName, string key, string detail)
            : base($"{detail ?? "Configuration is wrong"}. Section name is '{sectionName ?? "root"}' key name is '{key}'")
        {
            ConfigurationSection = sectionName ?? "root";
            ConfigurationKey = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.ApplicationException"></see> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            ConfigurationSection = info.GetString(nameof(Section));
            ConfigurationKey = info.GetString(nameof(Key));
        }

        /// <summary>
        /// Serialize current instance of exception
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);

            info.AddValue(nameof(Section), Section);
            info.AddValue(nameof(Key), Key);
        }

        /// <summary>
        /// ConfigurationName of the section that is missing or contains wrong configuration
        /// </summary>
        protected string ConfigurationSection;

        /// <summary>
        /// ConfigurationName of the section that is missing or contains wrong configuration
        /// </summary>
        public string Section => ConfigurationSection;

        /// <summary>
        /// ConfigurationName of the configuration key that is missing or contains wrong value
        /// </summary>
        public string Key => ConfigurationKey;

        /// <summary>
        /// ConfigurationName of the configuration key that is missing or contains wrong value
        /// </summary>
        protected string ConfigurationKey;
    }
}