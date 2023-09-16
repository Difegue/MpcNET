// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetVolumeCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MpcNET.Commands.Playback
{
    using System.Collections.Generic;

    /// <summary>
    /// Command to get volume.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#playback-options
    /// </summary>
    public class GetVolumeCommand : IMpcCommand<string>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVolumeCommand"/> class.
        /// </summary>
        public GetVolumeCommand()
        {
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize() => "getvol";

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public string Deserialize(SerializedResponse response)
        {
            if (response.ResponseValues.Count > 0 && response.ResponseValues[0].Key == "volume")
            {
                return response.ResponseValues[0].Value;
            }

            return "";
        }
    }
}