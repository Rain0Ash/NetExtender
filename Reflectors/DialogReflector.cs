// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;

namespace NetExtender.Reflectors
{
    public class DialogReflector
    {
        private readonly String _nms;
        private readonly Assembly _assembly;

        public DialogReflector(String assemblyName, String namespaceName)
        {
            _nms = namespaceName;
            _assembly = null;
            AssemblyName[] alist = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName assembly in alist)
            {
                if (!assembly.FullName.StartsWith(assemblyName))
                {
                    continue;
                }

                _assembly = Assembly.Load(assembly);
                break;
            }
        }

        public Type GetType(String typeName)
        {
            Type type = null;
            String[] names = typeName.Split('.');

            if (names.Length > 0)
            {
                type = _assembly.GetType(_nms + "." + names[0]);
            }

            for (Int32 i = 1; i < names.Length; ++i)
            {
                type = type?.GetNestedType(names[i], BindingFlags.NonPublic);
            }

            return type;
        }

        public Object Call(Type type, Object obj, String func, Object[] parameters)
        {
            MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return methInfo?.Invoke(obj, parameters);
        }

        public Object GetField(Type type, Object obj, String field)
        {
            FieldInfo fieldInfo = type.GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fieldInfo?.GetValue(obj);
        }
    }
}