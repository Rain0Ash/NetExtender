// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections;
using System.ComponentModel;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Comparers
{
    public interface ISortDescriptionComparer : IComparer
    {
        public SortDescriptionCollection Descriptions { get; }
    }
}