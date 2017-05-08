using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public partial class HexUpdaterScript : MonoBehaviour {

    public SocketIOComponent socket;

    private List<User> users;

	// Use this for initialization
	void Start () {
        socket.On("position-update", (data) =>
        {
            Debug.Log(data.ToString());
        });
	}

    User FindUser(string id)
    {
        foreach(var u in users)
        {
            if (u.id == id)
                return u;
        }

        return null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
