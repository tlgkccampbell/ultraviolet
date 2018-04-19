﻿using System;
using Newtonsoft.Json;

namespace Ultraviolet.FreeType2
{
    /// <summary>
    /// Represents the asset metadata for the <see cref="FreeTypeFontProcessor"/> class.
    /// </summary>
    public sealed class FreeTypeFontProcessorMetadata
    {
        /// <summary>
        /// Gets a value which specifies whether to use the nearest matching pixel size if <see cref="SizeInPixels"/> is specified
        /// but the font face does not contain that pixel size.
        /// </summary>
        public Boolean UseClosestPixelSize { get; private set; } = false;

        /// <summary>
        /// Gets the width, in pixels, of the texture atlases which contain the font's glyphs.
        /// </summary>
        public Int32 AtlasWidth { get; private set; } = 1024;

        /// <summary>
        /// Gets the height, in pixels, of the texture atlases which contain the font's glyphs.
        /// </summary>
        public Int32 AtlasHeight { get; private set; } = 1024;

        /// <summary>
        /// Gets the spacing, in pixels, between glyphs on the font's texture atlases.
        /// </summary>
        public Int32 AtlasSpacing { get; private set; } = 4;

        /// <summary>
        /// Gets the size of the font in points. If <see cref="SizeInPixels"/> has a non-zero value, this value must be zero.
        /// </summary>
        public Int32 SizeInPoints { get; private set; } = 0;

        /// <summary>
        /// Gets the size of the font in pixels. If <see cref="SizeInPoints"/> has a non-zero value, this value must be zero.
        /// </summary>
        public Int32 SizeInPixels { get; private set; } = 0;

        /// <summary>
        /// An adjustment, in pixels, which is applied to the horizontal advance of the face's glyphs.
        /// </summary>
        public Int32 AdjustHorizontalAdvance { get; private set; } = 0;

        /// <summary>
        /// An adjustment, in pixels, which is applied to the vertical advance of the face's glyphs.
        /// </summary>
        public Int32 AdjustVerticalAdvance { get; private set; } = 0;

        /// <summary>
        /// Gets the radius of the stroke which is applied to the font's glyphs.
        /// </summary>
        public Int32 StrokeRadius { get; private set; } = 0;

        /// <summary>
        /// Gets the miter limit which is used when stroking the font's glyphs, if 
        /// the font's <see cref="StrokeLineJoin"/> property is set to <see cref="FreeTypeLineJoinMode.Miter"/>,
        /// <see cref="FreeTypeLineJoinMode.MiterFixed"/>, or <see cref="FreeTypeLineJoinMode.MiterVariable"/>.
        /// </summary>
        public Int32 StrokeMiterLimit { get; private set; } = 4;

        /// <summary>
        /// Gets the <see cref="FreeTypeLineCapMode"/> value which specifies the line cap mode used
        /// by the glyph stroker, if the font has stroked glyphs.
        /// </summary>
        public FreeTypeLineCapMode StrokeLineCap { get; private set; } = FreeTypeLineCapMode.Butt;

        /// <summary>
        /// Gets the <see cref="FreeTypeLineJoinMode"/> value which specifies the line join mode used
        /// by the glyph stroker, if the font has stroked glyphs.
        /// </summary>
        public FreeTypeLineJoinMode StrokeLineJoin { get; private set; } = FreeTypeLineJoinMode.Round;

        /// <summary>
        /// Gets the font face's substitution character.
        /// </summary>
        public Char? Substitution { get; private set; } = null;

        /// <summary>
        /// Gets a string representing the list of glyphs which should be prepopulated on the font's texture atlas.
        /// </summary>
        public String PrepopulatedGlyphs { get; private set; } = "ASCII";
    }
}
