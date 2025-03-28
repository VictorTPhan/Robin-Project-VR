using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadImage : MonoBehaviour
{
    public MeshRenderer meshRenderer;
   
    public void setImage(string url)
    {
        StartCoroutine(Download(url));

    }

    IEnumerator Download(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //Texture.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //texture=SetTexture("Image", ((DownloadHandlerTexture)request.downloadHandler).texture);

            Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            var mat = new Material(Shader.Find("UI/Default"));
            mat.mainTexture = myTexture;
            meshRenderer.material.mainTexture = myTexture;
        }
    }   
}
