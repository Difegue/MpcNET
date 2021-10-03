<img src="icon.png" width="128">
  
MpcNET
===========
Pure .NET Client Library for [**Music Player Daemon**](https://www.musicpd.org/) Servers.  
The heart and soul of [Stylophone](https://github.com/Difegue/Stylophone).

[**Nightly Packages**](https://github.com/Difegue/MpcNET/packages/) - [**Stable Versions**](https://www.nuget.org/packages/MpcNET/) 

## Usage

### Connection
To create a client for MPD, you must first create a `IPEndPoint` for the Server with the right IP and Port. 
````C#
var mpdEndpoint = new IPEndPoint(IPAddress.Loopback, 6600);
````
Then create a Client and Connect to MPD.
````C#
var client = new MpcConnection(mpdEndpoint);
var connected = await client.ConnectAsync();
````
The `ConnectAsync()` method is returning a bool to indicate if the connection was successfully. However, this can be queried directly on the Client also:
````C#
var isConnected = client.IsConnected;
````
and for MPD version, additional property is available:
````C#
var mpdVersion = client.Version
````
To disconnect the Client use the follow method:
````C#
await client.DisconnectAsync();
````
or just dispose the client:
````C#
client.Dispose();
````
### Send Command

````C#
using (var client = new MpcConnection(mpdEndpoint)) {
    await client.ConnectAsync();

    // Look in /Commands to see everything that implements IMpcCommand
    var request = await client.SendAsync(new IMpcCommand<List<string>>(parameters));

    if (!request.IsResponseValid) {
        var mpdError = request.Response?.Result?.MpdError;
        if (mpdError != null && mpdError != "")
            Console.WriteLine($"Error: {mpdError}");
        else
            Console.WriteLine($"Invalid server response: {response}.");

    } else {
        List<string> response = request.Response.Content;
        // do stuff
    }
}
````

### Command Lists

````C#
var commandList = new CommandList();

commandList.Add(new IMpcCommand<?>(firstCommandArgument);
commandList.Add(new IMpcCommand<?>(secondCommandArgument);
commandList.Add(new IMpcCommand<?>(thirdCommandArgument);

using (var client = new MpcConnection(mpdEndpoint)) {
    await client.ConnectAsync();
    var request = await client.SendAsync(commandList);

    // Response string contains responses of all the commands, split by commas
    string response = request.Response?.Content;
}

````

### Binary Responses

````C#
// Get albumart from MPD
List<byte> data = new List<byte>();

using (var client = new MpcConnection(mpdEndpoint))
{
    long totalBinarySize = 9999;
    long currentSize = 0;

    do
    {
        var albumReq = await client.SendAsync(new AlbumArtCommand(f.Path, currentSize));
        if (!albumReq.IsResponseValid) break;

        var response = albumReq.Response.Content;
        if (response.Binary == 0) break; // MPD isn't giving us any more data, let's roll with what we have.

        totalBinarySize = response.Size;
        currentSize += response.Binary;
        data.AddRange(response.Data);

        Debug.WriteLine($"Downloading albumart: {currentSize}/{totalBinarySize}");
    } while (currentSize < totalBinarySize);
}

````

