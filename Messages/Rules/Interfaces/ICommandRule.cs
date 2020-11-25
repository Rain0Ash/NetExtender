// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Localizations;

namespace NetExtender.Messages.Rules.Interfaces
{
    public interface ICommandRule<T> : IConsoleRule<T>
    {
        public T Id { get; }
        public LocaleStrings Name { get; set; }
        public LocaleStrings Annotation { get; set; }
    }
}