using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Core
{
    public static class MethodBaseUtilities
    {
        public static String DeclarationName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return $"{method.DeclaringType?.Name ?? "UnknownType"}.{method.Name}";
        }

        public static String DeclarationFullName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return $"{method.DeclaringType?.FullName ?? "UnknownType"}.{method.Name}";
        }

        public static String FullName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ParameterInfo[]? parameters = method.GetSafeParameters();
            return $"{method.DeclarationName()}({(parameters is not null ? String.Join(", ", parameters.Select(ParameterInfoUtilities.FullName)) : "Unknown")})";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsVoid(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.ReturnType.IsVoid();
        }

        public static Boolean IsInheritable(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Type? declaring = method.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            return declaring is { IsVisible: true, IsSealed: false } && !method.IsStatic && (method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOverridable(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsAbstract || method.IsInheritable() && method is {IsVirtual: true, IsFinal: false};
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAsync(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Type type = method.ReturnType;
            return type == typeof(ValueTask) || type.Closes(typeof(ValueTask<>)) || type == typeof(Task) || type.Closes(typeof(Task<>));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo TryGetGenericMethodDefinition(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? TryGetGenericArguments(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericArguments() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParameterInfo[]? GetSafeParameters(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            try
            {
                return method.GetParameters();
            }
            catch (TypeLoadException)
            {
                return null;
            }
            catch (NotSupportedException) when (method is ConstructorBuilder builder)
            {
                return CodeGeneratorStorageUtilities.Parameters.ConstructorBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException) when (method is MethodBuilder builder)
            {
                return CodeGeneratorStorageUtilities.Parameters.MethodBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetParameterTypes(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetParameters().Types();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? GetSafeParameterTypes(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetSafeParameters()?.Types();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] GetParameterNames(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetParameters().Names();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetSafeParameterNames(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetSafeParameters()?.Names();
        }
    }
}