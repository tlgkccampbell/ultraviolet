﻿using System;
using TwistedLogik.Ultraviolet.Input;
using TwistedLogik.Ultraviolet.UI.Presentation.Input;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Controls
{
    /// <summary>
    /// Represents an item in a <see cref="ComboBox"/> control.
    /// </summary>
    [UvmlKnownType(null, "TwistedLogik.Ultraviolet.UI.Presentation.Controls.Templates.ComboBoxItem.xml")]
    public class ComboBoxItem : ListBoxItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxItem"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="name">The element's identifying name within its namescope.</param>
        public ComboBoxItem(UltravioletContext uv, String name)
            : base(uv, name)
        {
            HighlightOnSelect    = false;
            HighlightOnMouseOver = !Generic.IsTouchDeviceAvailable;
        }

        /// <inheritdoc/>
        protected override void OnGenericInteraction(UltravioletResource device, ref RoutedEventData data)
        {
            if (!data.Handled)
            {
                var comboBox = ItemsControl.ItemsControlFromItemContainer(this) as ComboBox;
                if (comboBox != null)
                {
                    comboBox.HandleItemClicked(this);
                }
                data.Handled = true;
            }
            base.OnGenericInteraction(device, ref data);
        }
        
        /// <inheritdoc/>
        protected override void OnContentChanged()
        {
            var comboBox = ItemsControl.ItemsControlFromItemContainer(this) as ComboBox;
            if (comboBox != null)
            {
                comboBox.HandleItemChanged(this);
            }
            base.OnContentChanged();
        }
    }
}
