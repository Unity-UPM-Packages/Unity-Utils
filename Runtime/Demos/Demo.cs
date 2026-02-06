using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace TheLegends.Unity.Utils
{
    public class Demo : MonoBehaviour
    {
        public void TestGetConnection()
        {
            InvokeRepeating(nameof(A), 0f, 5f);
        }

        public async void A()
        {
            ConnectionsUtils.GetConnectionType((connectionType) =>
            {
                switch (connectionType)
                {
                    case EConnectionsType.OFFLINE:
                        Debug.Log("Connection Type: OFFLINE");
                        break;
                    case EConnectionsType.WIFI:
                        Debug.Log("Connection Type: WIFI");
                        break;
                    case EConnectionsType.MOBILE_DATA:
                        Debug.Log("Connection Type: MOBILE DATA");
                        break;
                    default:
                        Debug.Log("Connection Type: UNKNOWN");
                        break;
                }
            });
        }
    }
}
