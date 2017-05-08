using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class SocketTest : MonoBehaviour
{

    public SocketIOComponent comp;
    private float lat;

	// Use this for initialization
	void Start () {
        comp.On("boop", (e) => Debug.Log(e.name + " " + e.data.ToString()));
        comp.On("latency", (e) => Debug.Log((Time.time - lat) + " units of time"));
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        SocketIOComponent.Socket.Emit("test-event", new JSONObject("{\"nme\": \"hdr\"}"));
	    } else if (Input.GetKeyDown(KeyCode.P))
	    {
	        comp.Emit("latency");
	        lat = Time.time;
	    }
	}
}
