using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API_Manager : MonoBehaviour
{
    private string JsonString;
    private string APILink;
    public downloadImage DLImage;
    // Start is called before the first frame update
    void Start()
    {
        APILink = "https://68d5117f-1492-495c-8d29-1f479c728ed9-00-31o7o4c3rmgec.kirk.replit.dev";
    }
   
    public void RequestImage(string prompt)
    {
        string uri = APILink + "/robin/" + prompt;
        StartCoroutine(GetRobinAPIRequest(uri));
    }
    // Update is called once per frame
    
    void Update()
    {
        
    }
    IEnumerator GetRobinAPIRequest(string uri)
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
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                JsonString = webRequest.downloadHandler.text;
                Imagejson json = Imagejson.CreateFromJSON(JsonString);

                json.printInfo();
                DLImage.setImage(json.imageURL);
                break;
        }

    }

}
