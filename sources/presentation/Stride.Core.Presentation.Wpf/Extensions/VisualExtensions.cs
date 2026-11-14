// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Stride.Core.Annotations;
using Stride.Core.Presentation.Interop;

namespace Stride.Core.Presentation.Extensions
{
    public static class VisualExtensions
    {
        /// <summary>

        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        /// <remarks>

        /// </remarks>

        /// <seealso cref="Visual.PointFromScreen"/>
        public static Point GetCursorRelativePosition([NotNull] this Visual visual)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            NativeHelper.POINT point;
            NativeHelper.GetCursorPos(out point);
            return visual.PointFromScreen((Point)point);
        }

        /// <summary>

        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point GetCursorScreenPosition([NotNull] this Visual visual)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            return Window.GetWindow(visual).GetCursorScreenPosition();
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Visual FindAdornable([NotNull] this Visual source)
        {
            return FindAdornableOfType<Visual>(source);
        }

        [CanBeNull]
        public static T FindAdornableOfType<T>([NotNull] this Visual source) where T : Visual
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (AdornerLayer.GetAdornerLayer(source) != null && source is T)
                return (T)source;

            var childCount = VisualTreeHelper.GetChildrenCount(source);
            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(source, i) as T;
                var test = child?.FindAdornableOfType<T>();
                if (test != null)
                    return test;
            }

            return null;
        }

        [CanBeNull]
        public static AdornerLayer FindAdornerLayer([NotNull] this Visual source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var adornerLayer = AdornerLayer.GetAdornerLayer(source);
            if (adornerLayer != null)
                return adornerLayer;

            var childCount = VisualTreeHelper.GetChildrenCount(source);
            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(source, i) as Visual;
                var test = child?.FindAdornerLayer();
                if (test != null)
                    return test;
            }

            return null;
        }

        /// <summary>
        /// Converts a <see cref="Rect"/> in screen coordinates into a <see cref="Rect"/> that represents the current coordinate system of the <see cref="Visual"/>.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="rect">The <see cref="Rect"/> value in screen coordinates.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect RectFromScreen([NotNull] this Visual visual, Rect rect)
        {
            return RectFromScreen(visual, ref rect);
        }

        /// <summary>
        /// Converts a <see cref="Rect"/> in screen coordinates into a <see cref="Rect"/> that represents the current coordinate system of the <see cref="Visual"/>.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="rect">The <see cref="Rect"/> value in screen coordinates.</param>
        /// <returns></returns>
        public static Rect RectFromScreen([NotNull] this Visual visual, ref Rect rect)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            var topLeft = visual.PointFromScreen(new Point(rect.Left, rect.Top));
            var bottomRight = visual.PointFromScreen(new Point(rect.Right, rect.Bottom));
            return new Rect(topLeft, bottomRight);
        }

        /// <summary>
        /// Converts a <see cref="Rect"/> that represents the current coordinate system of the <see cref="Visual"/> into a <see cref="Rect"/> in screen coordinates.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="rect">The <see cref="Rect"/> value that represents the current coordinate system of the <see cref="Visual"/>.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect RectToScreen([NotNull] this Visual visual, Rect rect)
        {
            return RectToScreen(visual, ref rect);
        }

        /// <summary>
        /// Converts a <see cref="Rect"/> that represents the current coordinate system of the <see cref="Visual"/> into a <see cref="Rect"/> in screen coordinates.
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="rect">The <see cref="Rect"/> value that represents the current coordinate system of the <see cref="Visual"/>.</param>
        /// <returns></returns>
        public static Rect RectToScreen([NotNull] this Visual visual, ref Rect rect)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            var topLeft = visual.PointToScreen(new Point(rect.Left, rect.Top));
            var bottomRight = visual.PointToScreen(new Point(rect.Right, rect.Bottom));
            return new Rect(topLeft, bottomRight);
        }

    }
}
