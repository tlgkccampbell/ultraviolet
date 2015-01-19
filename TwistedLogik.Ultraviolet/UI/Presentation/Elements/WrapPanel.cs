﻿using System;
using System.Xml.Linq;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Elements
{
    /// <summary>
    /// Represents an element container which stacks its children either directly on top of each
    /// other (if <see cref="StackPanel.Orientation"/> is <see cref="F:Orientation.Vertical"/>) or
    /// side-by-side if (see <see cref="StackPanel.Orientation"/> is <see cref="F:Orientation.Horizontal"/>,
    /// wrapping the content if necessary to fit within the available space.
    /// </summary>
    [UIElement("WrapPanel")]
    public class WrapPanel : Panel
    {
        /// <summary>
        /// Initializes the <see cref="WrapPanel"/> type.
        /// </summary>
        static WrapPanel()
        {
            ComponentTemplate = LoadComponentTemplateFromManifestResourceStream(typeof(WrapPanel).Assembly,
                "TwistedLogik.Ultraviolet.UI.Presentation.Elements.Templates.WrapPanel.xml");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapPanel"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="id">The element's unique identifier within its view.</param>
        public WrapPanel(UltravioletContext uv, String id)
            : base(uv, id)
        {
            LoadComponentRoot(ComponentTemplate);
        }

        /// <summary>
        /// Gets or sets the template used to create the control's component tree.
        /// </summary>
        public static XDocument ComponentTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the panel's orientation.
        /// </summary>
        public Orientation Orientation
        {
            get { return GetValue<Orientation>(OrientationProperty); }
            set { SetValue<Orientation>(OrientationProperty, value); }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Orientation"/> property changes.
        /// </summary>
        public event UIElementEventHandler OrientationChanged;

        /// <summary>
        /// Identifies the <see cref="Orientation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WrapPanel),
            new DependencyPropertyMetadata(HandleOrientationChanged, () => Orientation.Horizontal, DependencyPropertyOptions.AffectsMeasure));

        /// <inheritdoc/>
        protected override Size2D MeasureContent(Size2D availableSize)
        {
            foreach (var child in Children)
                child.Measure(new Size2D(Double.PositiveInfinity, Double.PositiveInfinity));

            var contentSize = (Orientation == Orientation.Vertical) ? 
                MeasureVertically(availableSize) :
                MeasureHorizontally(availableSize);

            return contentSize;
        }

        /// <inheritdoc/>
        protected override Size2D ArrangeContent(Size2D finalSize, ArrangeOptions options)
        {
            if (Orientation == Orientation.Vertical)
            {
                finalSize = ArrangeVertically(finalSize, options);
            }
            else
            {
                finalSize = ArrangeHorizontally(finalSize, options);
            }

            return finalSize;
        }

        /// <inheritdoc/>
        protected override void PositionContent(Point2D position)
        {
            foreach (var child in Children)
                child.Position(position);

            base.PositionContent(position);
        }

        /// <summary>
        /// Raises the <see cref="OrientationChanged"/> event.
        /// </summary>
        protected virtual void OnOrientationChanged()
        {
            var temp = OrientationChanged;
            if (temp != null)
            {
                temp(this);
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Orientation"/> dependency property changes.
        /// </summary>
        /// <param name="dobj">The dependency object that raised the event.</param>
        private static void HandleOrientationChanged(DependencyObject dobj)
        {
            var wrapPanel = (WrapPanel)dobj;
            wrapPanel.OnOrientationChanged();
        }

        /// <summary>
        /// Measures the panel when it is oriented vertically.
        /// </summary>
        /// <param name="availableSize">The size of the area which the element's parent has 
        /// specified is available for the element's layout.</param>
        /// <returns>The panel's measured size.</returns>
        private Size2D MeasureVertically(Size2D availableSize)
        {
            var contentWidth  = 0.0;
            var contentHeight = 0.0;

            var index     = 0;            
            var colCount  = 0;
            var colWidth  = 0.0;
            var colHeight = 0.0;

            while (CalculateColumnProperties(availableSize, index, out colCount, out colWidth, out colHeight))
            {
                contentWidth  = contentWidth + colWidth;
                contentHeight = Math.Max(contentHeight, colHeight);

                index += colCount;
            }

            return new Size2D(contentWidth, contentHeight);
        }

        /// <summary>
        /// Measures the panel when it is oriented horizontally.
        /// </summary>
        /// <param name="availableSize">The size of the area which the element's parent has 
        /// specified is available for the element's layout.</param>
        /// <returns>The panel's measured size.</returns>
        private Size2D MeasureHorizontally(Size2D availableSize)
        {
            var contentWidth  = 0.0;
            var contentHeight = 0.0;

            var index     = 0;
            var rowCount  = 0;
            var rowWidth  = 0.0;
            var rowHeight = 0.0;

            while (CalculateRowProperties(availableSize, index, out rowCount, out rowWidth, out rowHeight))
            {
                contentWidth  = Math.Max(contentWidth, rowWidth);
                contentHeight = contentHeight + rowHeight;

                index += rowCount;
            }

            return new Size2D(contentWidth, contentHeight);
        }

        /// <summary>
        /// Arranges the panel when it is oriented vertically.
        /// </summary>
        /// <param name="finalSize">The element's final size after arrangement.</param>
        /// <param name="options">A set of <see cref="ArrangeOptions"/> values specifying the options for this arrangement.</param>
        /// <returns>The panel's size after arrangement.</returns>
        private Size2D ArrangeVertically(Size2D finalSize, ArrangeOptions options)
        {
            var index     = 0;
            var positionX = 0.0;
            var positionY = 0.0;

            var colCount  = 0;
            var colWidth  = 0.0;
            var colHeight = 0.0;

            while (CalculateColumnProperties(finalSize, index, out colCount, out colWidth, out colHeight))
            {
                for (int i = 0; i < colCount; i++)
                {
                    var child = Children[index + i];
                    var childRect = new RectangleD(positionX, positionY, colWidth, child.DesiredSize.Height);

                    child.Arrange(childRect);

                    positionY += childRect.Height;
                }

                positionX = positionX + colWidth;
                positionY = 0;

                index += colCount;
            }

            return finalSize;
        }

        /// <summary>
        /// Arranges the panel when it is oriented horizontally.
        /// </summary>
        /// <param name="finalSize">The element's final relative to its parent element.</param>
        /// <param name="options">A set of <see cref="ArrangeOptions"/> values specifying the options for this arrangement.</param>
        /// <returns>The panel's size after arrangement.</returns>
        private Size2D ArrangeHorizontally(Size2D finalSize, ArrangeOptions options)
        {
            var index     = 0;
            var positionX = 0.0;
            var positionY = 0.0;

            var rowCount  = 0;
            var rowWidth  = 0.0;
            var rowHeight = 0.0;

            while (CalculateRowProperties(finalSize, index, out rowCount, out rowWidth, out rowHeight))
            {
                for (int i = 0; i < rowCount; i++)
                {
                    var child = Children[index + i];
                    var childRect = new RectangleD(positionX, positionY, child.DesiredSize.Width, rowHeight);

                    child.Arrange(childRect);

                    positionX += childRect.Width;
                }

                positionX = 0;
                positionY = positionY + rowHeight;

                index += rowCount;
            }

            return finalSize;
        }

        /// <summary>
        /// Calculates the properties of the specified layout row.
        /// </summary>
        /// <param name="availableSize">The amount of space that is available for laying out the panel.</param>
        /// <param name="index">The index of the first element in the layout row.</param>
        /// <param name="rowCount">The number of elements in the layout row.</param>
        /// <param name="rowWidth">The width of the layout row in device independent pixels.</param>
        /// <param name="rowHeight">The height of the layout row in device independent pixels.</param>
        /// <returns><c>true</c> if the specified index is the beginning of a valid layout row; otherwise, <c>false</c>.</returns>
        private Boolean CalculateRowProperties(Size2D availableSize, Int32 index, out Int32 rowCount, out Double rowWidth, out Double rowHeight)
        {
            rowCount  = 0;
            rowWidth  = 0;
            rowHeight = 0.0;

            if (index >= Children.Count)
                return false;

            var position = 0.0;
            var height   = 0.0;
            var count    = 0;

            for (int i = index; i < Children.Count; i++)
            {
                var child = Children[i];
                if (position + child.DesiredSize.Width > availableSize.Width)
                    break;

                count  = count + 1;
                height = Math.Max(height, child.DesiredSize.Height);

                position += child.DesiredSize.Width;
            }

            rowCount  = count;
            rowWidth  = position;
            rowHeight = height;

            return rowCount > 0;
        }

        /// <summary>
        /// Calculates the properties of the specified layout column.
        /// </summary>
        /// <param name="availableSize">The amount of space that is available for laying out the panel.</param>
        /// <param name="index">The index of the first element in the layout column.</param>
        /// <param name="columnCount">The number of elements in the layout column.</param>
        /// <param name="columnWidth">The width of the layout column in pixels.</param>
        /// <param name="columnHeight">The height of the layout column in pixels.</param>
        /// <returns><c>true</c> if the specified index is the beginning of a valid layout column; otherwise, <c>false</c>.</returns>
        private Boolean CalculateColumnProperties(Size2D availableSize, Int32 index, out Int32 columnCount, out Double columnWidth, out Double columnHeight)
        {
            columnCount  = 0;
            columnWidth  = 0.0;
            columnHeight = 0.0;

            if (index >= Children.Count)
                return false;

            var position = 0.0;
            var width    = 0.0;
            var count    = 0;

            for (int i = index; i < Children.Count; i++)
            {
                var child = Children[i];
                if (position + child.DesiredSize.Height > availableSize.Height)
                    break;

                count = count + 1;
                width = Math.Max(width, child.DesiredSize.Width);

                position += child.DesiredSize.Height;
            }

            columnCount  = count;
            columnWidth  = width;
            columnHeight = position;

            return columnCount > 0;
        }
    }
}
