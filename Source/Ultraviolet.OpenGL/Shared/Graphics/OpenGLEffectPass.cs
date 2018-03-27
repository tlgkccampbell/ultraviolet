﻿using System;
using System.Collections.Generic;
using Ultraviolet.Core;
using Ultraviolet.Graphics;

namespace Ultraviolet.OpenGL.Graphics
{
    /// <summary>
    /// Represents the OpenGL implementation of the EffectPass class.
    /// </summary>
    public sealed class OpenGLEffectPass : EffectPass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLEffectPass"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="name">The effect pass' name.</param>
        /// <param name="programs">The effect pass' collection of shader programs.</param>
        public OpenGLEffectPass(UltravioletContext uv, String name, ICollection<OpenGLShaderProgram> programs)
            : base(uv)
        {
            Contract.RequireNotEmpty(programs, nameof(programs));

            this.Name = name ?? String.Empty;
            this.programs = new OpenGLShaderProgramCollection(programs);
        }

        /// <inheritdoc/>
        public override void Apply()
        {
            var program = programs[programIndex];
            OpenGLState.UseProgram(program);

            foreach (var uniform in program.Uniforms)
            {
                uniform.Apply();
            }
        }

        /// <inheritdoc/>
        public override String Name { get; }

        /// <summary>
        /// Gets or sets the effect pass' current program index.
        /// </summary>
        public Int32 ProgramIndex
        {
            get { return programIndex; }
            set
            {
                Contract.EnsureRange(value >= 0 && value < ProgramCount, nameof(value));

                programIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the effect pass' program count.
        /// </summary>
        public Int32 ProgramCount => programs.Count;

        /// <summary>
        /// Gets the effect pass' collection of shader programs.
        /// </summary>
        public OpenGLShaderProgramCollection Programs => programs;

        // Property values.
        private readonly OpenGLShaderProgramCollection programs;
        private Int32 programIndex;
    }
}
