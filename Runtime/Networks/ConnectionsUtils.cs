using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TheLegends.Unity.Utils
{
    public static class ConnectionsUtils
    {
        public static async void GetConnectionType(Action<EConnectionsType> onComplete = null)
        {
            NetworkReachability reachability = Application.internetReachability;

            EConnectionsType connectionType = EConnectionsType.UNKNOWN;

            if (reachability == NetworkReachability.NotReachable)
            {
                connectionType = EConnectionsType.OFFLINE;
                onComplete?.Invoke(connectionType);
                return;
            }

            await WebRequestUtils.SendWebRequest("https://google.com", (request) =>
            {
                switch (reachability)
                {
                    case NetworkReachability.ReachableViaCarrierDataNetwork:
                        connectionType = EConnectionsType.MOBILE_DATA;
                        break;
                    case NetworkReachability.ReachableViaLocalAreaNetwork:
                        connectionType = EConnectionsType.WIFI;
                        break;
                    default:
                        connectionType = EConnectionsType.UNKNOWN;
                        break;
                }
            }, (request, error) =>
            {
                connectionType = EConnectionsType.OFFLINE;
            });

            onComplete?.Invoke(connectionType);
        }
    }
}
