using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNetworkUpdater : MonoBehaviour {

    public SocketIOComponent socket;

    private Vector2 lastFramePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        JSONObject pos = new JSONObject();
        pos.AddField("x", transform.position.x);
        pos.AddField("y", transform.position.y);

        if(((Vector2)transform.position) != lastFramePosition)
        {
            lastFramePosition = transform.position;

            socket.Emit("position-update", pos);

            Debug.Log(pos.ToString());
        }
	}
}
