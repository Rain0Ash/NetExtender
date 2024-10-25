using System.Windows.Controls;
using System.Windows.Data;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class TargetContextMenu : ContextMenu
    {
        protected static Binding TargetBinding { get; } = new Binding(nameof(PlacementTarget) + "." + nameof(DataContext)) { RelativeSource = RelativeSource.Self };
        
        public TargetContextMenu()
        {
            SetBinding(DataContextProperty, TargetBinding);
        }
    }
}