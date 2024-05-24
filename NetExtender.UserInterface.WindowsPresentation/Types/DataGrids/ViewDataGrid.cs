using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation.Types.DataGrids
{
    public class ViewDataGrid : FixedDataGrid
    {
        public ViewDataGrid()
        {
            SelectionMode = DataGridSelectionMode.Single;
            AutoGenerateColumns = false;
            IsReadOnly = true;
            
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