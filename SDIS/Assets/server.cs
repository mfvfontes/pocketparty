using UnityEngine;
using System.Collections;
using System.Net;
using System;

public class server : MonoBehaviour {

	public string[] prefixes;

	// Use this for initialization
	void Start () {
 	
		// Verificar se HttpListener e suportado
		if (!HttpListener.IsSupported)
		{
			Debug.Log ("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
			return;
		}

		if (prefixes.Length == 0) {
			Debug.Log("There are no prefixes defined");
			return;
		}


		HttpListener listener = new HttpListener ();

		// Atribuir os prefixos ao listener
		foreach (string prefix in prefixes) {
			listener.Prefixes.Add (prefix);
		}

		listener.Start ();
		Debug.Log ("Start Listening");

		listener.BeginGetContext (new AsyncCallback(ListenerCallback),listener);

	}

	void ListenerCallback(IAsyncResult result) {
		Debug.Log ("CallBack");
		HttpListener listener = (HttpListener)result.AsyncState;

        // Acabar a receção
		HttpListenerContext context = listener.EndGetContext (result);

        // Voltar a registar o callback
        listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);

        // request -> dados do request 
		HttpListenerRequest request = context.Request;

        // Dados estao no reader
		System.IO.Stream body = request.InputStream;
		System.Text.Encoding encoding = request.ContentEncoding;
		System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

        if (request.ContentType != null)
        {
            Debug.Log("Client data content type " + request.ContentType);
        }
        else
        {
            Debug.Log("Chegou vazio");
        }

		Debug.Log ("Client data content length  " + request.ContentLength64);
		
		Debug.Log ("Start of client data:");
		// Convert the data to a string and display it on the console.
		string s = reader.ReadToEnd();

        

		Debug.Log (s);
		Debug.Log ("End of client data:");

        // Fechar streams
		body.Close();
		reader.Close();


        // Resposta
		HttpListenerResponse response = context.Response;

		string responseString = "<html><body>You found me!</body></html>";
		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		response.ContentLength64 = buffer.Length;
		System.IO.Stream output = response.OutputStream;
		output.Write(buffer,0,buffer.Length);
		
		output.Close();
	}
}
