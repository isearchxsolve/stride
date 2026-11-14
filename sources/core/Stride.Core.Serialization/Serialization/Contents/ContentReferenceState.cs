// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace Stride.Core.Serialization.Contents;

public enum ContentReferenceState
{
    /// <summary>
    /// Never try to load the data reference.
    /// </summary>
    NeverLoad = 0,

    /// <summary>
    /// Data reference has already been loaded.
    /// </summary>
    Loaded = 3,

    /// <summary>
    /// Data reference has been set to a new value by the user.

    /// </summary>
    Modified = 5,
}