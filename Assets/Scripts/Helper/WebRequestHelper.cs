using UnityEngine.Networking;
using UnityEngine;
using Cysharp.Threading.Tasks;

public static class WebRequestHelper
{
    //TODO Add asynchronous retrieval of random text from the website.
    //Public static async UniTask<string> GetTextAsync(string url) {}.

    public static async UniTask<Texture2D> GetTextureAsync(string url)
    {
        var webRequestAsyncOperation = UnityWebRequestTexture.GetTexture(url).SendWebRequest();
        var webRequest = await webRequestAsyncOperation;

        var downloadHandleTexture = (DownloadHandlerTexture)webRequest.downloadHandler;
        var texture = downloadHandleTexture.texture;

        return texture;
    }
}
