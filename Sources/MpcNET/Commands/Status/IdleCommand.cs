// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdleCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Status
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Idles mpd until something happens.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#querying-mpd-s-status
    /// </summary>
    public class IdleCommand : IMpcCommand<string>
    {
        private readonly string subSystems;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdleCommand"/> class.
        /// </summary>
        /// <param name="subSystem">The sub system.</param>
        public IdleCommand(string subSystem)
        {
            this.subSystems = subSystem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdleCommand"/> class with multiple subsystems.
        /// </summary>
        /// <param name="subSystems">The sub systems to wait on.</param>
        public IdleCommand(params string[] subSystems)
        {
            this.subSystems = subSystems == null
                ? null
                : string.Join(" ", subSystems.Where(s => !string.IsNullOrEmpty(s)));
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            if (string.IsNullOrEmpty(this.subSystems))
            {
                return "idle";
            }

            return "idle " + this.subSystems;
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
