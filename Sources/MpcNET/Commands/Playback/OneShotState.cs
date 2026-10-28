// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OneShotState.cs" company="MpcNET">
// Copyright (c) MpcNET. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MpcNET.Commands.Playback
{
    /// <summary>
    /// State accepted by the <c>single</c> and <c>consume</c> commands.
    /// <c>OneShot</c> on <c>single</c> requires MPD 0.21+, on <c>consume</c> requires MPD 0.24+.
    /// </summary>
    public enum OneShotState
    {
        /// <summary>
        /// Disabled (wire value: <c>0</c>).
        /// </summary>
        Off,

        /// <summary>
        /// Enabled (wire value: <c>1</c>).
        /// </summary>
        On,

        /// <summary>
        /// One-shot (wire value: <c>oneshot</c>).
        /// </summary>
        OneShot,
    }
}
