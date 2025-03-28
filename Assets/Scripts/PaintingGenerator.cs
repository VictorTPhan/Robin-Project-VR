using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaintingGenerator : MonoBehaviour
{
    public DownloadImage DLImage;
    private string APILink;

    void Start()
    {
        APILink = "https://bella-server.onrender.com/";
    }
   
    public void RequestImage(string prompt)
    {
        string uri = APILink + "/get_image/" + prompt;
        StartCoroutine(GetAPIRequest(uri));
    }
    
    IEnumerator GetAPIRequest(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

      
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                break;
            case UnityWebRequest.Result.ProtocolError:
                break;
            case UnityWebRequest.Result.Success:
                string imageUrl = webRequest.downloadHandler.text;
                imageUrl = imageUrl.Substring(1, imageUrl.Length - 3);
                Debug.Log(imageUrl);
                DLImage.setImage(imageUrl);
                break;
        }

    }

}
