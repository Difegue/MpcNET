// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListAllCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using MpcNET.Types;

    /// <summary>
    /// Lists all songs and directories.
    /// https://www.musicpd.org/doc/protocol/database.html.
    /// </summary>
    public class ListAllCommand : IMpcCommand<IEnumerable<MpdDirectory>>
    {
        private readonly string uri;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAllCommand"/> class.
        /// </summary>
        public ListAllCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAllCommand"/> class restricted to a URI.
        /// </summary>
        /// <param name="uri">Directory or file URI to list.</param>
        public ListAllCommand(string uri)
        {
            this.uri = uri;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => string.IsNullOrEmpty(uri) ? "listall" : $"listall \"{uri}\"";

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public IEnumerable<MpdDirectory> Deserialize(SerializedResponse response)
        {
            var rootDirectory = new List<MpdDirectory>
            {
                new MpdDirectory("/"), // Add by default the root directory
            };

            foreach (var line in response.ResponseValues)
            {
                if (line.Key.Equals("file"))
                {
                    rootDirectory.Last().AddFile(line.Value);
                }

                if (line.Key.Equals("directory"))
                {
                    rootDirectory.Add(new MpdDirectory(line.Value));
                }
            }

            return rootDirectory;
        }
    }

    // TODO: rescan
}
