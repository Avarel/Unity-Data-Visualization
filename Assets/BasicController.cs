using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public SocketClient socket;

    // bool keyDown = false;
    private HashSet<KeyCode> keyDown = new HashSet<KeyCode>();

    // Update is called once per frame
    void Update()
    {
        CheckKey(KeyCode.W);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.S);
        CheckKey(KeyCode.D);
        CheckKey(KeyCode.Return);
    }

    void CheckKey(KeyCode k)
    {
        if (Input.GetKeyDown(k))
        {
            if (!keyDown.Contains(k))
            {
                keyDown.Add(k);
                OnPress(k);
            }
        }
        else
        {
            keyDown.Remove(k);
        }
    }

    void OnPress(KeyCode k)
    {
        socket.websocket.SendText(k.ToString());
    }
}
