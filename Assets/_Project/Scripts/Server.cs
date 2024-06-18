using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{ 
    public static IEnumerator Send(string apiUrl, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(apiUrl))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest(); 
            callback?.Invoke(request);
        }
    } 
}
