// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace NetExtender.Utilities.Types
{
    /// <summary>
    /// The <see cref="Exception"/> class extensions.
    /// </summary>
    public static class ExceptionUtilities
    {
        public static String ToDiagnosticString(this Exception source)
        {
            return ToDiagnosticString(source, null);
        }

        // ReSharper disable once CognitiveComplexity
        private static String ToDiagnosticString(this Exception source, StringBuilder? builder)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            builder ??= new StringBuilder(200);

            for (Exception? exception = source; exception is not null; exception = exception?.InnerException)
            {
                String text = $"Exception: {exception.GetType()}";

                for (Int32 i = 0; i < text.Length; i++)
                {
                    builder.Append('-');
                }

                builder.AppendLine(text);

                if (exception.Message.IsNotNullOrEmpty())
                {
                    builder.AppendLine(exception.Message);
                }

                if (exception.StackTrace.IsNotNullOrEmpty())
                {
                    builder.AppendLine(exception.StackTrace);
                }

                switch (exception)
                {
                    case FileNotFoundException file:
                    {
                        builder.AppendLine($"File Name: {file.FileName}");
                        builder.AppendLine(file.FusionLog.IsNullOrEmpty() ? "Fusion log is empty or disabled." : file.FusionLog);

                        break;
                    }
                    case AggregateException aex:
                    {
                        Boolean inner = false;

                        foreach (Exception ex in aex.InnerExceptions)
                        {
                            inner = inner || ex != exception.InnerException;
                            ToDiagnosticString(ex, builder);
                        }

                        if (inner)
                        {
                            exception = exception.InnerException;
                        }

                        break;
                    }
                    case ReflectionTypeLoadException reflection:
                    {
                        foreach (Exception ex in reflection.LoaderExceptions.WhereNotNull())
                        {
                            ToDiagnosticString(ex, builder);
                        }

                        break;
                    }
                }
            }

            return builder.ToString();
        }
    }
}