#!/usr/bin/env python

# WS server example

import asyncio
import websockets

async def hello(websocket, path):
    print("Client connected!")
    async for name in websocket:
        print(f"<| {name}")

        greeting = "YIPPEEEEE"

        await websocket.send(greeting)
        print(f"|> {greeting}")


start_server = websockets.serve(hello, "localhost", 8765)

print("Starting server!")

asyncio.get_event_loop().run_until_complete(start_server)
asyncio.get_event_loop().run_forever()