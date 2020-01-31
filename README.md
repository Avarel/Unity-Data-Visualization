# Websocket Data Visualization

This project visualizes CSV files through a websocket. It consists of a Unity client written in C# that connects to a websocket server written in Python, which sends the data for the client to graph and display.

## Video
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/oQD3KqCNSr4/0.jpg)](https://www.youtube.com/watch?v=oQD3KqCNSr4)

[Video Link](https://www.youtube.com/watch?v=oQD3KqCNSr4)

# Instructions for Running
* Clone the repository.

## Running the Python Server
* `cd Server/`
* Install `asyncio`, `websockets`, and `aioconsole` through `pip`.
* Run `python3 ws_server.py`.

## Running the Unity Client
* Download Unity version `2019.3.0f6`.
* Open the project directory in the Unity editor.
* Click the run button.