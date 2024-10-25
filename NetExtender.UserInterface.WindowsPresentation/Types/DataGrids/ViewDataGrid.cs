using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class ViewDataGrid : FixedDataGrid
    {
        static ViewDataGrid()
        {
            SelectionModeProperty.OverrideMetadataDefaultValue<ViewDataGrid>(DataGridSelectionMode.Single);
            AutoGenerateColumnsProperty.OverrideMetadataDefaultValue<ViewDataGrid>(false);
            IsReadOnlyProperty.OverrideMetadataDefaultValue<ViewDataGrid>(true);
            
            /*CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter()
                    {
                        Property = TemplateProperty,
                        Value = WindowsPresentationTemplateUtilities.CreateControlTemplate(typeof(DataGridCell), () =>
                        {
                            return new ContentPresenter() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
                        })
                    },
                }
            };*/
        }
    }
}