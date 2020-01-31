using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class SocketClient : MonoBehaviour
{
    public WebSocket websocket;

    public DataPlotter plotter;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8765");

        websocket.OnOpen += OnOpen;
        websocket.OnError += OnError;
        websocket.OnClose += OnClose;
        websocket.OnMessage += OnMessage;

        // Send greeting after 5 seconds!
        InvokeRepeating("SendWebSocketMessage", 0.0f, 5.0f);
        await websocket.Connect();
    }

    void OnOpen()
    {
        Debug.Log("Connection open.");
    }

    void OnError(string e)
    {
        Debug.Log("Error: " + e);
    }

    void OnClose(WebSocketCloseCode e)
    {
        Debug.Log("Connection closed code: " + e);
    }

    void OnMessage(byte[] bytes)
    {
        var message = System.Text.Encoding.UTF8.GetString(bytes);
        if (message.StartsWith("INPUT:"))
        {
            Debug.Log("Received data!");
            var data = message.Substring(6);
            plotter.PlotDataFromString(data);
        }
        else
        {
            Debug.Log("Message received: " + message);
        }
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}