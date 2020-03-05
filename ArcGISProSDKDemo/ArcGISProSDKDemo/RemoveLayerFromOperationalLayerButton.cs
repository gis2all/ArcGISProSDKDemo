using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Linq;
using System.Windows;

namespace ArcGISProSDKDemo
{
    public class RemoveLayerFromOperationalLayerButton : Button
    {
        protected override async void OnClick()
        {
            try
            {
                var myMap = Utils.GetMapFromActiveMap();
                if (myMap?.IsScene == false)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Failed to get scene.");
                    return;
                }
                var result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Are you sure to remove first layer?", "Remove layer", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
                {
                    return;
                }
                else
                {
                    await QueuedTask.Run(() =>
                    {
                        if (myMap.Layers.Count == 0)
                        {
                            return;
                        }
                        var firstLayer = myMap.Layers.FirstOrDefault();
                        myMap.RemoveLayer(firstLayer);
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"\"{firstLayer.Name}\" has been removed!");
                    });
                }
            }
            catch (Exception exc)
            {
                // Catch any exception found and display a message box.
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Exception caught: " + exc.Message);
                return;
            }
        }
    }
}