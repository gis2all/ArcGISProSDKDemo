using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Net.Http;

namespace ArcGISProSDKDemo
{
    public class AddDataToOperationalLayerButton : Button
    {
        protected override async void OnClick()
        {
            try
            {
                // Get scene from the current project.
                var myMap = Utils.GetMapFromActiveMap();
                if (myMap?.IsScene == false)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Failed to get scene.");
                    return;
                }
                // The layer has to be created on the Main CIM Thread (MCT).
                await QueuedTask.Run(() =>
                {
                    // OperationalLayer
                    var tiledServiceLayer = (TiledServiceLayer)LayerFactory.Instance.CreateLayer(new Uri(Utils.DataSoureUrl), myMap);

                    // Basemaps
                    // myMap.SetBasemapLayers(Basemap.Satellite);

                    // ElevationSurface                                   
                    // myMap.SetElevationSurface(surface);

                    if (tiledServiceLayer == null)
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Failed to create layer for url: " + $"\"{Utils.DataSoureUrl}\"");
                    }
                    else
                    {
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Create layer successfully for url: " + $"\"{Utils.DataSoureUrl}\"");
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