// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlChangesCommand.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MpcNET.Commands.Queue
{
    using System.Collections.Generic;
    using MpcNET.Types;

    /// <summary>
    /// Displays changed songs currently in the queue since VERSION, optionally limited to a START:END range.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#the-queue
    /// </summary>
    public class PlChangesCommand : IMpcCommand<IEnumerable<IMpdFile>>
    {
        private readonly int version;
        private readonly int startpos;
        private readonly int endpos;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlChangesCommand"/> class.
        /// </summary>
        /// <param name="version">Version to compare to the current playlist.</param>
        public PlChangesCommand(int version = -1)
        {
            this.version = version;
            this.startpos = -1;
            this.endpos = int.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlChangesCommand"/> class with a range.
        /// </summary>
        /// <param name="version">Version to compare to the current playlist.</param>
        /// <param name="startpos">Inclusive start position.</param>
        /// <param name="endpos">Exclusive end position.</param>
        public PlChangesCommand(int version, int startpos, int endpos)
        {
            this.version = version;
            this.startpos = startpos;
            this.endpos = endpos;
        }

        /// <summary>
        /// Serializes the command.
        /// </summary>
        /// <returns>
        /// The serialize command.
        /// </returns>
        public string Serialize()
        {
            if (startpos < 0)
            {
                return $"plchanges {version}";
            }
            return $"plchanges {version} {startpos}:{endpos}";
        }

        /// <summary>
        /// Deserializes the specified response text pairs.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The deserialized response.
        /// </returns>
        public IEnumerable<IMpdFile> Deserialize(SerializedResponse response)
        {
            return MpdFile.CreateList(response.ResponseValues);
        }
    }
}
