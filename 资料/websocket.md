# WebSocket使用例子

##  WebSocket服务器端（c#）
控制台程序
```cs
/*需要引用Fleck
github地址：https://github.com/statianzo/Fleck
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Sockets;
using Fleck;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            #region ===========websocket服务器=============
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://127.0.0.1:7181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }

            #endregion 
        }
    }
}
```
##  WebSocket客户端
```js
<script type="text/javascript">
        var start = function () {
            var inc = document.getElementById('incomming');
            var wsImpl = window.WebSocket || window.MozWebSocket;
            var form = document.getElementById('sendForm');
            var input = document.getElementById('sendText');

            inc.innerHTML += "connecting to server ..<br/>";

            // create a new websocket and connect
            window.ws = new wsImpl('ws://localhost:7181/');

            // when data is comming from the server, this metod is called
            ws.onmessage = function (evt) {
                inc.innerHTML += evt.data + '<br/>';
            };

            // when the connection is established, this method is called
            ws.onopen = function () {
                inc.innerHTML += '.. connection open<br/>';
            };

            // when the connection is closed, this method is called
            ws.onclose = function () {
                inc.innerHTML += '.. connection closed<br/>';
            }

            form.addEventListener('submit', function (e) {
                e.preventDefault();
                var val = input.value;
                ws.send(val);
                input.value = "";
            });

        }
        window.onload = start;
    </script>
```
# SignalR库(web与服务器间的通讯)
```cs
```


