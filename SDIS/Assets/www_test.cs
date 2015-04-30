using UnityEngine;
using System.Collections;

public class www_test : MonoBehaviour {

	WWWForm form;
	// Use this for initialization
	void Start () {
	
		form = new WWWForm ();
		form.AddField("frameCount", Time.frameCount.ToString());

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Jump")) {

			
			form = new WWWForm ();
			form.AddField("frameCount", Time.frameCount.ToString());

			Debug.Log("JUMP");
			WWW www = new WWW("127.0.0.1:8000/test/", form);
			while (!www.isDone)
			{}
			Debug.Log(www.text);
		}
	}
}
