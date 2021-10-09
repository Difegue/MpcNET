// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsumeCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playback
{
    using System.Collections.Generic;

    /// <summary>
    /// Command to set consume state. 
    /// Sets consume state to STATE, STATE should be 0 or 1. When consume is activated, each song played is removed from playlist.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#playback-options
    /// </summary>
    public class ConsumeCommand : IMpcCommand<string>
    {
        private readonly string single;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeCommand" /> class.
        /// </summary>
        /// <param name="single">if set to <c>true</c> [consume].</param>
        public ConsumeCommand(bool single)
        {
            this.single = single ? "1" : "0";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeCommand"/> class.
        /// </summary>
        public ConsumeCommand()
        {
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            if (this.single == null)
            {
                return string.Join(" ", "consume");
            }

            return string.Join(" ", "consume", this.single);
        }

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