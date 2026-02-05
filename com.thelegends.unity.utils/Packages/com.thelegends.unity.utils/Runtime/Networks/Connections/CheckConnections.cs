using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TheLegends.Unity.Utils
{
    public class CheckConnections
    {
        public Action<EConnectionsType> OnConnectionTypeChecked;
        private EConnectionsType connectionType;

        private IEnumerator CheckConnectionCoroutine()
        {
            NetworkReachability reachability = Application.internetReachability;

            switch (reachability)
            {
                case NetworkReachability.NotReachable:
                    connectionType = EConnectionsType.OFFLINE;
                    break;
                default:
                    yield return CheckConnectionType();
                    break;
            }

            OnConnectionTypeChecked?.Invoke(connectionType);
        }

        private IEnumerator CheckConnectionType()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://google.com");
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                connectionType = EConnectionsType.OFFLINE;
            }
            else
            {
                NetworkReachability reachability = Application.internetReachability;

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
            }

            request.Dispose();
        }
    }
}
