using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class www_test : MonoBehaviour {

	WWWForm form;
	Text input;
    Text log;

	// Use this for initialization
	void Start () {
	
		input = GameObject.FindGameObjectWithTag ("IP").GetComponent<Text>();
        log = GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if (Input.GetButton ("Jump")) {
			connect();
		}
		*/
	}

	public void connect ()
	{
		HttpWebRequest request = (HttpWebRequest) System.Net.WebRequest.Create(input.text);
		
		request.Method = "POST";
		
		string postData = "test one";
		ASCIIEncoding encoding = new ASCIIEncoding ();
		byte[] byte1 = encoding.GetBytes (postData);
		
		// Set the content length of the string being posted.
		request.ContentLength = byte1.Length;
		
		Stream newStream = request.GetRequestStream ();
        newStream.Write(byte1, 0, byte1.Length);

        log.text += "Sent message";
		//StartCoroutine(Test());
	}

	IEnumerator Test () {

		form = new WWWForm ();
		form.AddField ("test", 1);

		WWW www = new WWW("http://127.0.0.1:8000/test/");

		yield return www;

		if (!string.IsNullOrEmpty(www.error))
			print("Error downloading");
		else
			Debug.Log(www.text);
	}
}
