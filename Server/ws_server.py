#!/usr/bin/env python

# WS server example

import asyncio
from aioconsole import ainput, aprint
import websockets

from websockets import WebSocketServerProtocol, WebSocketServer

address = "localhost"
port = 8765

async def initialize():
    await aprint(f"Websocket@ws://{address}:{port} started listening.")
    async with websockets.serve(serve_file_manually, address, port) as webserver:
        while True:
            command: str = await ainput(">>> ")
            if command == "stop" or command == "exit" or command == "quit":
                webserver.close()
                break
            elif command == "status":
                await aprint("> Is Serving?", webserver.is_serving())
            elif command == "inspect":
                file_name: str = await ainput("FILE NAME >>> ")
                with open(file_name, 'r') as file:
                    data = file.read()
                    await aprint(data)
            elif command == "sockets":
                sockets = webserver.sockets
                if sockets != None:
                    for socket in sockets:
                        await aprint(">", socket)
            else:
                await aprint("> Command [", command, "] not recognized.")
    await aprint("Websocket closed.")

counter: int = 0

async def serve_file_manually(websocket: WebSocketServerProtocol, path: str):
    print(f"Client@{websocket.remote_address[0]} connected!")
    async for msg in websocket:
        print(f"<- {msg}")

        if msg == "Return":
            global counter
            file_name: str = f"iris{counter + 1}.csv"
            counter = (counter + 1) % 3

            with open(file_name, 'r') as file:
                data = file.read()
                await websocket.send(f"INPUT:{data}")
                await aprint(f"Sent data from {file_name}")
        else:
            greeting = f"Echoing {msg}"
            await websocket.send(greeting)
            await aprint(f"-> {greeting}")
    print(f"Client@{websocket.remote_address[0]} disconnected!")

asyncio.get_event_loop().run_until_complete(initialize())
# asyncio.get_event_loop().run_until_complete(start_server)
# asyncio.get_event_loop().run_forever()