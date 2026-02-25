using System;

namespace NetExtender.Harmony.Types
{
    public enum HarmonyExceptionBlockType : Byte
    {
        /// <summary>The beginning of an exception block</summary>
        BeginExceptionBlock,

        /// <summary>The beginning of a catch block</summary>
        BeginCatchBlock,

        /// <summary>The beginning of an except filter block (currently not supported to use in a patch)</summary>
        BeginExceptFilterBlock,

        /// <summary>The beginning of a fault block</summary>
        BeginFaultBlock,

        /// <summary>The beginning of a finally block</summary>
        BeginFinallyBlock,

        /// <summary>The end of an exception block</summary>
        EndExceptionBlock
    }

    public sealed record HarmonyExceptionBlock(Type? Type, HarmonyExceptionBlockType Block)
    {
        public static HarmonyExceptionBlock Exception { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.BeginExceptionBlock);
        public static HarmonyExceptionBlock Catch { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.BeginCatchBlock);
        public static HarmonyExceptionBlock ExceptFilter { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.BeginExceptFilterBlock);
        public static HarmonyExceptionBlock Fault { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.BeginFaultBlock);
        public static HarmonyExceptionBlock Finally { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.BeginFinallyBlock);
        public static HarmonyExceptionBlock End { get; } = new HarmonyExceptionBlock(null, HarmonyExceptionBlockType.EndExceptionBlock);
    }
}