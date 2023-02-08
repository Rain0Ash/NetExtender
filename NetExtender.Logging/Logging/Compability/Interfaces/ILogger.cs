// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>Represents a type used to perform logging.</summary>
    /// <remarks>Aggregates most logging patterns to a single method.</remarks>
    /// <typeparam name="TLevel">The type whose is used for the logger logging level.</typeparam>
    public interface ICustomLogger<in TLevel> : ILogger where TLevel : unmanaged, Enum
    {
        /// <summary>Writes a log entry.</summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <see cref="T:System.String" /> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public void Log<TState>(TLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, String> formatter);

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public Boolean IsEnabled(TLevel logLevel);
    }
    
    /// <summary>
    /// A generic interface for logging where the category name is derived from the specified
    /// <typeparamref name="TCategoryName" /> type name.
    /// Generally used to enable activation of a named <see cref="T:Microsoft.Extensions.Logging.ILogger" /> from dependency injection.
    /// </summary>
    /// <typeparam name="TLevel">The type whose is used for the logger logging level.</typeparam>
    /// <typeparam name="TCategoryName">The type whose name is used for the logger category name.</typeparam>
    public interface ICustomLogger<in TLevel, out TCategoryName> : ICustomLogger<TLevel>, ILogger<TCategoryName> where TLevel : unmanaged, Enum
    {
    }
}