// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public enum ActiveBindingPathTokenType
    {
        /// <summary>
        /// Math, e.g. Math.Sin, Math.PI
        /// </summary>
        Math,
        /// <summary>
        /// Usual PropertyPath, e.g. Name, Caption, Models.Count
        /// </summary>
        Property,
        /// <summary>
        /// Static PropertyPath, e.g. local:StaticVM.Property
        /// </summary>
        StaticProperty,
        /// <summary>
        /// Enum, e.g. local:Enum.Value
        /// </summary>
        Enum
    }
}
