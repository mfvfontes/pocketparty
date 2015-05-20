using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        

       REST_Play p = new REST_Play();

        Play pl = new Play();
        pl.move = new Vector2(1,0);
        pl.jump = false;

        p.play = pl;

        p.playerID = "test";

        string postData = JsonConvert.SerializeObject(p, Formatting.Indented,
            new JsonSerializerSettings()
                        { 
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }
        );

        Debug.Log(postData);

        HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(input.text);

        request.Method = "POST";

        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] byte1 = encoding.GetBytes(postData);

        // Set the content length of the string being posted.
        request.ContentLength = byte1.Length;

        Stream newStream = request.GetRequestStream();
        newStream.Write(byte1, 0, byte1.Length);

	}
}
