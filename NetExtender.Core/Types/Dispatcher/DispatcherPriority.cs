// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Dispatchers
{
    /// <summary>
    /// An enunmeration describing the priorities at which
    /// operations can be invoked via the Dispatcher.
    /// </summary>
    ///
    public enum DispatcherPriority
    {
        /// <summary>
        /// This is an invalid priority.
        /// </summary>
        Invalid = -1,

        /// <summary>
        /// Operations at this priority are not processed.
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// Operations at this priority are processed when the system
        /// is idle.
        /// </summary>
        SystemIdle,

        /// <summary>
        /// Operations at this priority are processed when the application
        /// is idle.
        /// </summary>
        ApplicationIdle,

        /// <summary>
        /// Operations at this priority are processed when the context
        /// is idle.
        /// </summary>
        ContextIdle,

        /// <summary>
        /// Operations at this priority are processed after all other
        /// non-idle operations are done.
        /// </summary>
        Background,

        /// <summary>
        /// Operations at this priority are processed at the same
        /// priority as input.
        /// </summary>
        Input,

        /// <summary>
        /// Operations at this priority are processed when layout and render is
        /// done but just before items at input priority are serviced. Specifically
        /// this is used while firing the Loaded event
        /// </summary>
        Loaded,

        /// <summary>
        /// Operations at this priority are processed at the same
        /// priority as rendering.
        /// </summary>
        Render,

        /// <summary>
        /// Operations at this priority are processed at the same
        /// priority as data binding.
        /// </summary>
        DataBind,

        /// <summary>
        /// Operations at this priority are processed at normal priority.
        /// </summary>
        Normal,

        /// <summary>
        /// Operations at this priority are processed before other
        /// asynchronous operations.
        /// </summary>
        Send
    }
}