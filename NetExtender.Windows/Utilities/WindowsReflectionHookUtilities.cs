// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Core.Types.Disposable;

namespace NetExtender.Utilities.Core
{
    public static class WindowsReflectionHookUtilities
    {
        private enum PageProtection : uint
        {
            PageNoAccess = 1,
            PageReadOnly = 2,
            PageReadWrite = 4,
            PageWriteCopy = 8,
            PageExecute = 16,
            PageExecuteRead = 32,
            PageExecuteReadWrite = 64,
            PageExecuteWriteCopy = 128,
            PageGuard = 256,
            PageNoCache = 512,
            PageWriteCombine = 1024
        }
        
        private static Boolean Is64Bit { get; }
        
        private static Dictionary<MethodInfo, Byte[]> Hooks { get; } = new Dictionary<MethodInfo, Byte[]>();

        static WindowsReflectionHookUtilities()
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            Is64Bit = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => true,
                Architecture.X86 => false,
                _ => throw new NotSupportedException("Only X32/X64 processors are supported.")
            };
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean FlushInstructionCache(IntPtr hProcess, IntPtr lpBaseAddress, UIntPtr dwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, UInt32 flNewProtect, out UInt32 lpflOldProtect);

        /// <summary>
        /// Replaces the method call original with the method replacement with a standard x86/x64 JMP hook. The methods do not have to be in the same Assembly.
        /// Original may be static, but replacement MUST be static and accept the same arguments (if the hooked method is non-static, the first argument should be
        /// of the class type).
        /// </summary>
        /// <param name="original">MethodInfo for the function to be hooked.</param>
        /// <param name="replacement">MethodInfo for the function to replace the original function.</param>
        /// <exception cref="ArgumentNullException">If original or replacement are null.</exception>
        /// <exception cref="ArgumentException">If original and replacement are the same function, original is generic, replacement is generic or non-static, or if the target function is already hooked.</exception>
        /// <exception cref="Win32Exception">If a native call fails. This is unrecoverable.</exception>
        public static IDisposable Hook(this MethodInfo original, MethodInfo replacement)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (replacement is null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            if (original.IsGenericMethod)
            {
                throw new ArgumentException("Original method cannot be generic");
            }

            if (replacement.IsGenericMethod || !replacement.IsStatic)
            {
                throw new ArgumentException("Hook method must be static and non-generic");
            }

            if (Hooks.ContainsKey(original))
            {
                throw new ArgumentException("Attempting to hook an already hooked method");
            }
            
            if (original == replacement)
            {
                return Disposable.Empty;
            }

            Byte[] opcodes = PatchJMP(original, replacement);
            Hooks.Add(original, opcodes);
            return new HookDisposableToken(original);
        }

        private sealed class HookDisposableToken : DisposableToken
        {
            public MethodInfo Original { get; }

            public HookDisposableToken(MethodInfo original)
            {
                Original = original ?? throw new ArgumentNullException(nameof(original));
            }

            protected override Boolean Dispose(Boolean dispose)
            {
                return Original.Unhook();
            }
        }

        /// <summary>
        /// Unhooks a previously hooked Method. Method must have already been hooked.
        /// </summary>
        /// <param name="original"></param>
        /// <exception cref="ArgumentNullException">If original or replacement are null.</exception>
        /// <exception cref="Win32Exception">If a native call fails. This is unrecoverable.</exception>
        private static Boolean Unhook(this MethodInfo original)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (!Hooks.ContainsKey(original))
            {
                return false;
            }

            Byte[] opcodes = Hooks[original];
            UnhookJMP(original, opcodes);
            return Hooks.Remove(original);
        }

        private static unsafe Byte[] PatchJMP(MethodBase original, MethodBase replacement)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (replacement is null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            RuntimeHelpers.PrepareMethod(original.MethodHandle);
            RuntimeHelpers.PrepareMethod(replacement.MethodHandle);

            IntPtr originalsite = original.MethodHandle.GetFunctionPointer();
            IntPtr replacementsite = replacement.MethodHandle.GetFunctionPointer();

            //instruction opcodes are 13 bytes on 64-bit, 7 bytes on 32-bit //TODO: 7 bytes or 6?
            Byte[] opcodes = new Byte[Is64Bit ? 13u : 7u];

            UInt32 protection = VirtualProtect(originalsite, (UInt32) opcodes.Length, (UInt32) PageProtection.PageExecuteReadWrite);

            Byte* sitepointer = (Byte*) originalsite.ToPointer();

            for (Int32 k = 0; k < opcodes.Length; k++)
            {
                opcodes[k] = *(sitepointer + k);
            }

            if (Is64Bit)
            {
                //mov r11, replacementsite
                *sitepointer = 0x49;
                *(sitepointer + 1) = 0xBB;
                *(UInt64*) (sitepointer + 2) = (UInt64) replacementsite.ToInt64();

                //jmp r11
                *(sitepointer + 10) = 0x41;
                *(sitepointer + 11) = 0xFF;
                *(sitepointer + 12) = 0xE3;
            }
            else
            {
                //push replacementsite
                *sitepointer = 0x68;
                *(UInt32*) (sitepointer + 1) = (UInt32) replacementsite.ToInt32();

                //ret
                *(sitepointer + 5) = 0xC3;
            }

            FlushInstructionCache(originalsite, (UInt32) opcodes.Length);
            VirtualProtect(originalsite, (UInt32) opcodes.Length, protection);
            return opcodes;
        }

        private static unsafe void UnhookJMP(MethodBase original, IReadOnlyList<Byte> opcodes)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (opcodes is null)
            {
                throw new ArgumentNullException(nameof(opcodes));
            }

            IntPtr site = original.MethodHandle.GetFunctionPointer();

            UInt32 protection = VirtualProtect(site, (UInt32) opcodes.Count, (UInt32) PageProtection.PageExecuteReadWrite);

            Byte* sitepointer = (Byte*) site.ToPointer();

            //put the original bytes back where they belong
            for (Int32 k = 0; k < opcodes.Count; k++)
            {
                *(sitepointer + k) = opcodes[k];
            }

            FlushInstructionCache(site, (UInt32) opcodes.Count);
            VirtualProtect(site, (UInt32) opcodes.Count, protection);
        }

        private static UInt32 VirtualProtect(IntPtr address, UInt32 size, UInt32 flags)
        {
            if (!VirtualProtect(address, (UIntPtr) size, flags, out UInt32 protection))
            {
                throw new Win32Exception();
            }

            return protection;
        }

        private static void FlushInstructionCache(IntPtr address, UInt32 size)
        {
            if (!FlushInstructionCache(GetCurrentProcess(), address, (UIntPtr) size))
            {
                throw new Win32Exception();
            }
        }
    }
}