﻿using LibMpc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace LibMpcTest
{
    public class LibMpcTest : IClassFixture<MpdServerTest>, IDisposable
    {
        private readonly MpdServerTest _server;
        private readonly ITestOutputHelper _output;
        private readonly Mpc _mpc;

        public LibMpcTest(MpdServerTest server, ITestOutputHelper output)
        {
            _server = server;
            _output = output;

            _mpc = new Mpc(new IPEndPoint(IPAddress.Loopback, 6600));
            
            var connected = Task.Run(async () => await _mpc.ConnectAsync()).Result;
            if (connected)
            {
                WriteLine("Connected to MPD.");
            }
            else
            {
                WriteLine("Could not connect to MPD.");
            }
        }

        [Fact]
        public async Task TagTypesTest()
        {
            var response = await _mpc.SendAsync(new Commands.Reflection.TagTypes());

            WriteLine("TagTypesTest Result:");
            WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.True(response.Response.Body.Count().Equals(17));
        }

        [Fact]
        public async Task ListAllTest()
        {
            var response = await _mpc.SendAsync(new Commands.Database.ListAll());

            WriteLine("ListAllTest Result:");
            WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.True(response.Response.Body.Count().Equals(7));
        }

        [Fact]
        public async Task FindGenreTest()
        {
            var response = await _mpc.SendAsync(new Commands.Database.Find(MpdTags.Genre, "soundfx"));

            WriteLine("FindGenreTest Result:");
            WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

            Assert.True(response.Response.Body.Count().Equals(6));
        }

        public void Dispose()
        {
            _mpc?.DisconnectAsync().GetAwaiter().GetResult();
        }

        private void WriteLine(string value)
        {
            Console.Out.WriteLine(value);
        }
    }
}
