using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;


public class NetworkHandler : MonoBehaviour
{

    public string serverAddress = "http://192.168.1.71:8080/test/";

    JsonSerializerSettings jsonSettings;

    private void Start()
    {
        jsonSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

    }

    public void sendObject(object obj)
    {
        string jsonObj = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSettings);
        sendMessage(jsonObj);
    }

    public void sendMessage(string message)
    {
        HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(serverAddress);

        request.Method = "POST";

        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] bytes = encoding.GetBytes(message);

        // Set the content length of the string being posted.
        request.ContentLength = bytes.Length;

        Stream newStream = request.GetRequestStream();
        newStream.Write(bytes, 0, bytes.Length);
    }
}
