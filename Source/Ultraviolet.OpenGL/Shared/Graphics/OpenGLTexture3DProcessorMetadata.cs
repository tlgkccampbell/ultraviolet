﻿using System;
using Newtonsoft.Json;

namespace Ultraviolet.OpenGL.Graphics
{
    /// <summary>
    /// Contains metadata for OpenGLTexture3DProcessor.
    /// </summary>
    internal sealed class OpenGLTexture3DProcessorMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLTexture3DProcessorMetadata"/> class.
        /// </summary>
        public OpenGLTexture3DProcessorMetadata()
        {
            PremultiplyAlpha = true;
            Opaque = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLTexture2DProcessorMetadata"/> class.
        /// </summary>
        /// <param name="premultiplyAlpha">A value indicating whether to premultiply the texture's alpha.</param>
        /// <param name="opaque">A value indicating whether the texture is opaque and color keying should be disabled.</param>
        [JsonConstructor]
        public OpenGLTexture3DProcessorMetadata(Boolean premultiplyAlpha, Boolean opaque)
        {
            PremultiplyAlpha = premultiplyAlpha;
            Opaque = opaque;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to premultiply the texture's alpha.
        /// </summary>
        [JsonProperty(PropertyName = "premultiplyAlpha")]
        public Boolean PremultiplyAlpha { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the texture is opaque and color keying should be disabled.
        /// </summary>
        [JsonProperty(PropertyName = "opaque")]
        public Boolean Opaque { get; private set; }
    }
}
