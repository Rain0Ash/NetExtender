using System.Collections;
using System.ComponentModel;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Comparers
{
    public interface ISortDescriptionComparer : IComparer
    {
        public SortDescriptionCollection Descriptions { get; }
    }
}