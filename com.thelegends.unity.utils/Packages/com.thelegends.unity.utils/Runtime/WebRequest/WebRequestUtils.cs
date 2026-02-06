using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TheLegends.Unity.Utils
{
    public static class WebRequestUtils
    {
        public static async Task<UnityWebRequest> SendWebRequest(
            string url,
            Action<UnityWebRequest> onSuccess = null,
            Action<UnityWebRequest, string> onError = null)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            try
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke(request);
                }
                else
                {
                    onError?.Invoke(request, request.error);
                }

                return request;
            }
            catch (Exception e)
            {
                Debug.LogError($"Web request failed: {e.Message}");
                onError?.Invoke(request, e.Message);
                throw;
            } finally
            {
                request?.Dispose();
            }
        }

        public static async Task<string> GetTextAsync(string url)
        {
            using UnityWebRequest request = await SendWebRequest(url);
            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.text;
            }

            throw new Exception($"Request failed: {request.error}");
        }

        public static async Task<byte[]> GetBytesAsync(string url)
        {
            using (UnityWebRequest request = await SendWebRequest(url))
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    return request.downloadHandler.data;
                }

                throw new Exception($"Request failed: {request.error}");
            }
        }

        public static async Task<T> PostJsonAsync<T>(string url, object data)
        {
            string jsonData = JsonUtility.ToJson(data);
            using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData, "application/json"))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    return JsonUtility.FromJson<T>(request.downloadHandler.text);
                }

                throw new Exception($"Request failed: {request.error}");
            }
        }
    }
}