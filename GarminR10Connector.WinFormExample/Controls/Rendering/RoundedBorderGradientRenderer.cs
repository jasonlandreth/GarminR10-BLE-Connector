using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarminR10Connector.WinFormExample.Controls.Rendering
{
    /// <summary>
    /// Provides reusable rounded-region and gradient-border rendering helpers for WinForms controls.
    /// </summary>
    public static class RoundedBorderGradientRenderer
    {
        /// <summary>
        /// Creates a rounded clipping region for a control.
        /// </summary>
        /// <param name="bounds">
        /// The control bounds.
        /// </param>
        /// <param name="cornerRadius">
        /// The corner radius in pixels.
        /// </param>
        /// <returns>
        /// A new rounded <see cref="Region"/>.
        /// </returns>
        public static Region CreateRoundedRegion(Rectangle bounds, int cornerRadius)
        {
            using GraphicsPath path = CreateRoundedPath(bounds, Math.Max(0, cornerRadius));
            return new Region(path);
        }

        /// <summary>
        /// Paints a solid interior with a gradient border on a rounded rectangle.
        /// </summary>
        /// <param name="graphics">
        /// The target graphics surface.
        /// </param>
        /// <param name="bounds">
        /// The bounds to paint within.
        /// </param>
        /// <param name="cornerRadius">
        /// The corner radius in pixels.
        /// </param>
        /// <param name="borderWidth">
        /// The border width in pixels.
        /// </param>
        /// <param name="backgroundColor">
        /// The interior fill color.
        /// </param>
        /// <param name="gradientStartColor">
        /// The border gradient start color.
        /// </param>
        /// <param name="gradientEndColor">
        /// The border gradient end color.
        /// </param>
        /// <param name="gradientAngle">
        /// The border gradient angle in degrees.
        /// </param>
        public static void PaintRoundedGradientBorder(
            Graphics graphics,
            Rectangle bounds,
            int cornerRadius,
            float borderWidth,
            Color backgroundColor,
            Color gradientStartColor,
            Color gradientEndColor,
            float gradientAngle)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int inset = (int)Math.Ceiling(borderWidth / 2f) + 1;
            Rectangle drawRect = Rectangle.Inflate(bounds, -inset, -inset);

            if (drawRect.Width <= 1 || drawRect.Height <= 1)
            {
                return;
            }

            int effectiveRadius = Math.Max(1, cornerRadius - inset);

            using GraphicsPath path = CreateRoundedPath(drawRect, effectiveRadius);

            using SolidBrush fillBrush = new(backgroundColor);
            graphics.FillPath(fillBrush, path);

            using LinearGradientBrush borderBrush = new(
                drawRect,
                gradientStartColor,
                gradientEndColor,
                gradientAngle);

            using Pen borderPen = new(borderBrush, Math.Max(1f, borderWidth))
            {
                LineJoin = LineJoin.Round
            };

            graphics.DrawPath(borderPen, path);
        }

        /// <summary>
        /// Creates a rounded rectangle path for a given rectangle and radius.
        /// </summary>
        /// <param name="Rect">
        /// The rectangle bounds.
        /// </param>
        /// <param name="radius">
        /// The corner radius in pixels.
        /// </param>
        /// <returns>
        /// A rounded rectangle <see cref="GraphicsPath"/>.
        /// </returns>
        private static GraphicsPath CreateRoundedPath(Rectangle Rect, int Radius)
        {
            GraphicsPath path = new();

            if (Radius <= 0)
            {
                path.AddRectangle(Rect);
                return path;
            }

            int diameter = Radius * 2;
            Rectangle arc = new(Rect.Location, new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = Rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = Rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = Rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
