using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Linq;

namespace ArcGISProSDKDemo
{
    public class ChangeLayerInOperationalLayerButton : Button
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
                // The layer has to be created on the Main CIM Thread (MCT).
                await QueuedTask.Run(() =>
                {
                    var layer = myMap.Layers.FirstOrDefault();
                    if (layer == null)
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Cannot find this layer");
                        return;
                    }
                    else
                    {
                        var oldName = layer.Name;
                        var newName = "Test new name";
                        layer.SetName(newName);
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Renamed " + $"\"{oldName}\" to \"{newName}\"!" + " successfully!");
                    }
                });
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