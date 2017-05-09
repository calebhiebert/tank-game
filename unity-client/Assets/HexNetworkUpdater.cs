using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNetworkUpdater : MonoBehaviour {

    public string id;
    public bool local;
    public int updatesPerSecond;

    private Vector2 lastFramePosition;

    private SocketIOComponent socket;
    private Rigidbody2D rigidBody;

    private float lastUpdate;

    private void Awake()
    {
        HexNetworkManager.players.Add(this);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        socket = SocketIOComponent.Socket;

        socket.On("open", d =>
        {
            if (local)
            {
                id = socket.sid;
            }
        });
	}

    // Update is called once per frame
    void Update () {

        if (local && (Time.time - lastUpdate) > 1f / updatesPerSecond)
        {
            JSONObject pos = new JSONObject();
            pos.AddField("x", transform.position.x);
            pos.AddField("y", transform.position.y);
            pos.AddField("r", transform.eulerAngles.z);
            pos.AddField("av", rigidBody.angularVelocity);
            pos.AddField("vx", rigidBody.velocity.x);
            pos.AddField("vy", rigidBody.velocity.y);

            if (((Vector2)transform.position) != lastFramePosition)
            {
                lastFramePosition = transform.position;

                socket.Emit("position-update", pos);
                lastUpdate = Time.time;
            }
        }
	}
}
