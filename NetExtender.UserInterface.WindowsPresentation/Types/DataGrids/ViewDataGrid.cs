// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class ViewDataGrid : FixedDataGrid
    {
        static ViewDataGrid()
        {
            SelectionModeProperty.OverrideMetadataDefaultValue<ViewDataGrid, DataGridSelectionMode>(DataGridSelectionMode.Single);
            AutoGenerateColumnsProperty.OverrideMetadataDefaultValue<ViewDataGrid, Boolean>(false);
            IsReadOnlyProperty.OverrideMetadataDefaultValue<ViewDataGrid, Boolean>(true);
            
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