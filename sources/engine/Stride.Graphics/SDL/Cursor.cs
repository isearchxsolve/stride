// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
#if STRIDE_UI_SDL
using System;
using System.Diagnostics;
using Silk.NET.SDL;
using Stride.Core.Mathematics;
using Point = Stride.Core.Mathematics.Point;

namespace Stride.Graphics.SDL
{
    /// <summary>

    /// </summary>
    public unsafe class Cursor
    {
        private static Sdl SDL => Window.SDL;

#region Initialization
        /// <summary>


        /// </summary>

        /// <param name="mask">Mask for <paramref name="data"/></param>




        public unsafe Cursor(byte[] data, byte[] mask, int w, int h, int hot_x, int hot_y)
        {
            Debug.Assert(
                (w | h) >= 0 &&
                (uint)w * (uint)h <= (uint)Math.Min(data.Length, mask.Length));
            fixed (byte* dataPtr = data)
            fixed (byte* maskPtr = mask)
                Handle = (IntPtr)SDL.CreateCursor(dataPtr, maskPtr, w, h, hot_x, hot_y);
        }
#endregion

#region Access

        /// <summary>

        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>

        /// </summary>
        public static Point Position
        {
            get { return Application.MousePosition; }
            set { Application.MousePosition = value; }
        }
#endregion

#region Basic Operations
        /// <summary>

        /// </summary>
        public static void Hide()
        {
            SDL.ShowCursor(0);
        }

        /// <summary>

        /// </summary>
        public static void Show()
        {
            SDL.ShowCursor(1);
        }

        /// <summary>

        /// </summary>

        public static void SetCursor(Cursor cur)
        {
            SDL.SetCursor((Silk.NET.SDL.Cursor*)cur.Handle);
        }
#endregion
    }
}
#endif
