using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Linq;

namespace ArcGISProSDKDemo
{
    public class SearchLayerInOperationalLayerButton : Button
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
                    // You can use URI to find unique layer
                    // var layer = myMap.FindLayer(layer.URI);
                    if (layer == null)
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Cannot find this layer");
                        return;
                    }
                    else
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                            $"Layer Name : {layer.Name}" + "\r\n" +
                            $"Layer MinScale : {layer.MinScale.ToString()}" + "\r\n" +
                            $"Layer MaxScale : {layer.MaxScale.ToString()}" + "\r\n", "First layer info"
                            );
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