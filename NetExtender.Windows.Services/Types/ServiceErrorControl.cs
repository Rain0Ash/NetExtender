// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Windows.Services.Types
{
    /// <summary>Specifies a service's security context, which defines its logon type.</summary>
    public enum ServiceErrorControl
    {
        /// <summary>
        /// The startup program ignores the error and continues the startup operation.
        /// </summary>
        Ignore,

        /// <summary>
        /// The startup program logs the error in the event log but continues the startup operation.
        /// </summary>
        Normal,

        /// <summary>
        /// The startup program logs the error in the event log. If the last-known-good configuration is being started, the startup operation continues. Otherwise, the system is restarted with the last-known-good configuration.
        /// </summary>
        Severe,

        /// <summary>
        /// The startup program logs the error in the event log, if possible. If the last-known-good configuration is being started, the startup operation fails. Otherwise, the system is restarted with the last-known good configuration.
        /// </summary>
        Critical
    }
}