// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddIdCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Queue
{
    using MpcNET;
    using System.Collections.Generic;

    /// <summary>
    /// Adds a song to the playlist (non-recursive) and returns the song id.
    /// https://www.musicpd.org/doc/protocol/queue.html.
    /// </summary>
    public class AddIdCommand : IMpcCommand<string>
    {
        private readonly string uri;
        private readonly string position;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommand"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="position">Optional position to place the song at. If the parameter starts with + or -, then it is relative to the current song [8]; 
        /// e.g. +0 inserts right after the current song and -0 inserts right before the current song (i.e. zero songs between the current song and the newly added song).</param>
        public AddIdCommand(string uri, int position)
        {
            this.uri = uri;
            this.position = position.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddIdCommand"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public AddIdCommand(string uri)
        {
            this.uri = uri;
            this.position = "";
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => string.Join(" ", "addid", $"\"{uri}\"", position);

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public string Deserialize(SerializedResponse response)
        {
            return string.Join(", ", response.ResponseValues);
        }
    }
}