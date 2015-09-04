﻿using System;
using TwistedLogik.Ultraviolet.UI.Presentation.Documents;
using TwistedLogik.Ultraviolet.UI.Presentation.Media;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Controls.Primitives
{
    /// <summary>
    /// Represents the root visual for elements inside of a <see cref="Popup"/> control.
    /// </summary>
    internal class PopupRoot : FrameworkElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupRoot"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="resized">The action to perform when the popup content is resized.</param>
        public PopupRoot(UltravioletContext uv, Action resized)
            : base(uv, null)
        {
            this.resized = resized;

            this.nonLogicalAdornerDecorator = new NonLogicalAdornerDecorator(uv, null);
            this.nonLogicalAdornerDecorator.ChangeVisualParent(this);
        }
        
        /// <summary>
        /// Gets or sets the popup root's child element.
        /// </summary>
        public UIElement Child
        {
            get { return GetValue<UIElement>(ChildProperty); }
            set { SetValue<UIElement>(ChildProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the popup is currently open.
        /// </summary>
        public Boolean IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; } 
        }
        
        /// <summary>
        /// Identifies the <see cref="Child"/> dependency property.
        /// </summary>
        /// <remarks>The styling name of this dependency property is 'child'.</remarks>
        public static readonly DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(UIElement), typeof(PopupRoot),
            new PropertyMetadata<UIElement>(null, PropertyMetadataOptions.None, HandleChildChanged));

        /// <inheritdoc/>
        protected internal override Int32 VisualChildrenCount
        {
            get { return 1; }
        }

        /// <inheritdoc/>
        protected internal override Int32 LogicalChildrenCount
        {
            get { return base.LogicalChildrenCount; }
        }

        /// <inheritdoc/>
        protected internal override UIElement GetVisualChild(Int32 childIndex)
        {
            if (childIndex != 0)
                throw new ArgumentOutOfRangeException("childIndex");

            return nonLogicalAdornerDecorator;
        }

        /// <inheritdoc/>
        protected internal override UIElement GetLogicalChild(Int32 childIndex)
        {
            return base.GetLogicalChild(childIndex);
        }

        /// <inheritdoc/>
        protected override Size2D MeasureOverride(Size2D availableSize)
        {
            nonLogicalAdornerDecorator.Measure(availableSize);
            return nonLogicalAdornerDecorator.DesiredSize;
        }

        /// <inheritdoc/>
        protected override Size2D ArrangeOverride(Size2D finalSize, ArrangeOptions options)
        {
            nonLogicalAdornerDecorator.Arrange(new RectangleD(Point2D.Zero, finalSize), options);
            return nonLogicalAdornerDecorator.RenderSize;
        }

        /// <inheritdoc/>
        protected override Visual HitTestCore(Point2D point)
        {
            return nonLogicalAdornerDecorator.HitTest(point);
        }

        /// <inheritdoc/>
        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            var popup = Parent as Popup;
            if (popup != null && popup.IsOpen)
            {
                if (resized != null)
                {
                    resized();
                }
            }
            base.OnChildDesiredSizeChanged(child);
        }

        /// <inheritdoc/>
        protected override RectangleD CalculateVisualBounds()
        {
            return base.CalculateVisualBounds();
        }

        /// <inheritdoc/>
        protected override RectangleD CalculateTransformedVisualBounds()
        {
            var popup = Parent as Popup;
            if (popup == null)
                return RectangleD.Empty;

            var visualBounds = VisualBounds;

            var popupTransform = popup.PopupTransformToAncestorWithOrigin;
            RectangleD.TransformAxisAligned(ref visualBounds, ref popupTransform, out visualBounds);

            return visualBounds;
        }

        /// <summary>
        /// Hooks the popup root's children into the visual tree of its parent popup.
        /// </summary>
        internal void HookIntoVisualTree()
        {
            nonLogicalAdornerDecorator.ChangeVisualParent(this);
        }

        /// <summary>
        /// Unhooks the popup root's children from the visual tree of its parent popup.
        /// </summary>
        internal void UnhookFromVisualTree()
        {
            nonLogicalAdornerDecorator.ChangeVisualParent(null);
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Child"/> dependency property changes.
        /// </summary>
        private static void HandleChildChanged(DependencyObject dobj, UIElement oldValue, UIElement newValue)
        {
            var popupRoot = (PopupRoot)dobj;
            var popup = popupRoot.Parent as Popup;

            popupRoot.nonLogicalAdornerDecorator.Child = newValue;
        }

        // State values.
        private readonly Action resized;
        private Boolean isOpen;

        // Popup components.
        private readonly NonLogicalAdornerDecorator nonLogicalAdornerDecorator;
    }
}
