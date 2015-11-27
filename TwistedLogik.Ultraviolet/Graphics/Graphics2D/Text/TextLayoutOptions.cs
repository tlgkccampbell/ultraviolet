﻿using System;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text
{
    /// <summary>
    /// Represents a set of options which change how the layout engine lays out text.
    /// </summary>
    [Flags]
    public enum TextLayoutOptions
    {
        /// <summary>
        /// No layout options.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Indicates that words which are split across multiple lines should be hyphenated.
        /// </summary>
        Hyphenate = 0x0001,

        /// <summary>
        /// Indicates that white space at the end of lines should be preserved, rather than
        /// being removed from the layout stream.
        /// </summary>
        PreserveTrailingWhiteSpace = 0x0002,
    }
}
