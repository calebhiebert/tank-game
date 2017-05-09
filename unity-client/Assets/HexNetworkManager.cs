using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public partial class HexNetworkManager : MonoBehaviour {

    public SocketIOComponent socket;
    public GameObject playerPrefab;

    private List<User> users;
    public static List<HexNetworkUpdater> players = new List<HexNetworkUpdater>();

	void Start () {
        socket.On("player-disconnect", (data) =>
        {
            Debug.LogWarning("Player disconnected");

            string id = "";

            data.data.GetField(ref id, "id");

            var u = FindUser(id);

            if(u != null)
            {
                Destroy(u.gameObject);
            }
        });

        socket.On("position-update", (data) =>
        {
            float x = 0;
            float y = 0;
            float rot = 0;
            float angularVelocity = 0;
            float velocityX = 0;
            float velocityY = 0;
            string id = "";

            data.data.GetField(ref x, "x");
            data.data.GetField(ref y, "y");
            data.data.GetField(ref id, "id");
            data.data.GetField(ref rot, "r");
            data.data.GetField(ref angularVelocity, "av");
            data.data.GetField(ref velocityX, "vx");
            data.data.GetField(ref velocityY, "vy");

            var u = FindUser(id);

            if(u == null)
            {
                Debug.LogWarning("Creating new player");
                GameObject go = Instantiate(playerPrefab);
                go.transform.position = new Vector2(x, y);
                go.GetComponent<HexNetworkUpdater>().id = id;
            } else if (!u.local)
            {
                u.GetComponent<SpriteRenderer>().color = Color.cyan;
                u.transform.position = new Vector2(x, y);
                u.transform.rotation = Quaternion.Euler(0, 0, rot);
                u.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
                u.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY);
            }
        });
	}

    HexNetworkUpdater FindUser(string id)
    {
        foreach(var u in players)
        {
            if (u.id == id)
                return u;
        }

        return null;
    }
}
