// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    /// <summary>
    /// The <see cref="Exception"/> class extensions.
    /// </summary>
    [PublicAPI]
    public static class ExceptionUtils
    {
        /// <summary>
        /// Returns detailed exception text.
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <param name="builder"><see cref="StringBuilder"/> instance.</param>
        /// <returns>Detailed exception text.</returns>
        [NotNull]
        public static StringBuilder ToDiagnosticString([NotNull] this Exception exception, StringBuilder builder = null)
        {
            builder ??= new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            ToDiagnosticString(exception, writer, builder.Length == 0);
            writer.Flush();
            return builder;
        }

        /// <summary>
        /// Returns detailed exception text.
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <param name="writer"><see cref="TextWriter"/> instance.</param>
        /// <param name="fromNewLine">If <c>true</c> - do not inject separator line from start.</param>
        /// <returns>Detailed exception text.</returns>
        public static void ToDiagnosticString(this Exception? exception, [NotNull] TextWriter writer, Boolean fromNewLine = true)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            for (Exception? ex = exception; ex is not null; ex = ex?.InnerException)
            {
                String exceptionText = $"Exception: {ex.GetType()}";

                if (!fromNewLine)
                {
                    for (Int32 i = 0; i < exceptionText.Length; i++)
                    {
                        writer.Write('-');
                    }

                    writer.WriteLine();
                }
                else
                {
                    fromNewLine = false;
                }

                writer.WriteLine(exceptionText);

                if (ex.Message.IsNotNullOrEmpty())
                {
                    writer.WriteLine(ex.Message);
                }

                if (ex.StackTrace.IsNotNullOrEmpty())
                {
                    writer.WriteLine(ex.StackTrace);
                }

                switch (ex)
                {
                    case FileNotFoundException notFoundException:
                        FileNotFoundException fex = notFoundException;

                        writer.WriteLine($"File Name: {fex.FileName}");

                        if (fex.FusionLog.IsNullOrEmpty())
                        {
                            writer.WriteLine("Fusion log is empty or disabled.");
                        }
                        else
                        {
                            writer.Write(fex.FusionLog);
                        }

                        break;

                    case AggregateException aex:
                        Boolean foundInnerException = false;

                        foreach (Exception e in aex.InnerExceptions)
                        {
                            foundInnerException = foundInnerException || e != ex.InnerException;
                            ToDiagnosticString(e, writer, false);
                        }

                        if (foundInnerException)
                        {
                            ex = ex.InnerException;
                        }

                        break;

                    case ReflectionTypeLoadException loadEx:
                        if (loadEx.LoaderExceptions is null)
                        {
                            break;
                        }

                        foreach (Exception e in loadEx.LoaderExceptions)
                        {
                            ToDiagnosticString(e, writer, false);
                        }

                        break;
                }
            }
        }
    }
}