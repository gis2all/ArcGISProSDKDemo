using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGISProSDKDemo
{
    public static class Utils
    {
        #region Test data source

        public static string DataSoureUrl = "https://services.arcgisonline.com/ArcGIS/rest/services/NatGeo_World_Map/MapServer";

        #endregion Test data source

        #region Get map or scene

        public static Task<Map> GetMapFromProject(Project project, string mapName)
        {
            // Return null if either of the two parameters are invalid.
            if (project == null || string.IsNullOrEmpty(mapName))
                return null;

            // Find the first project item with name matches with mapName            
            MapProjectItem mapProjItem =
                project.GetItems<MapProjectItem>().FirstOrDefault(item => item.Name.Equals(mapName, StringComparison.CurrentCultureIgnoreCase));

            if (mapProjItem != null)
                return QueuedTask.Run<Map>(() => { return mapProjItem.GetMap(); }, Progressor.None);
            else
                return null;
        }

        public static Map GetMapFromActiveMap()
        {
            return MapView.Active?.Map;
        }

        #endregion Get map or scene
    }
}
