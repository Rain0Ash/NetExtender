// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection.Emit;

namespace NetExtender.Utilities.Core
{
    public static class ReferenceUtilities
    {
        private static Action<Object?, Action<IntPtr>> GetPinnedPtr { get; }

        static ReferenceUtilities()
        {
            DynamicMethod method = new DynamicMethod(nameof(GetPinnedPtr), typeof(void), new[] { typeof(Object), typeof(Action<IntPtr>) }, typeof(ReferenceUtilities).Module);

            ILGenerator generator = method.GetILGenerator();
            generator.DeclareLocal(typeof(Object), true);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Conv_I);
            generator.Emit(OpCodes.Call, typeof(Action<IntPtr>).GetMethod("Invoke") ?? throw new NotSupportedException());
            generator.Emit(OpCodes.Ret);

            GetPinnedPtr = (Action<Object?, Action<IntPtr>>) method.CreateDelegate(typeof(Action<Object?, Action<IntPtr>>));
        }

        public static IntPtr GetPinnedReference(Object? value)
        {
            IntPtr pointer = IntPtr.Zero;

            if (value is not null)
            {
                GetPinnedPtr.Invoke(value, ptr => pointer = ptr);
            }
            
            return pointer;
        }

        public static Boolean GetPinnedReference(Object? value, Action<IntPtr> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (value is null)
            {
                action.Invoke(IntPtr.Zero);
            }
            
            GetPinnedPtr.Invoke(value, action);
            return true;
        }
    }
}