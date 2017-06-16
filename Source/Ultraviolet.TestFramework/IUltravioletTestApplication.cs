﻿using System;
using System.Drawing;
using Ultraviolet.Content;
using Ultraviolet.Input;

namespace Ultraviolet.TestFramework
{
    /// <summary>
    /// Represents an Ultraviolet application used in unit tests.
    /// </summary>
    public interface IUltravioletTestApplication : IDisposable
    {
        /// <summary>
        /// Specifies the application's audio subsystem assembly.
        /// </summary>
        /// <param name="audioSubsystem">The fully-qualified name of the application's audio subsystem assembly.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication WithAudioSubsystem(String audioSubsystem);

        /// <summary>
        /// Specifies that the application should configure the Presentation Foundation.
        /// </summary>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication WithPresentationFoundationConfigured();

        /// <summary>
        /// Specifies the application's initialization code.
        /// </summary>
        /// <param name="initializer">An action which will initialize the application.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication WithInitialization(Action<UltravioletContext> initializer);

        /// <summary>
        /// Specifies the application's content loading code.
        /// </summary>
        /// <param name="loader">An action which will load the unit test's required content.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication WithContent(Action<ContentManager> loader);

        /// <summary>
        /// Specifies the application's disposal code.
        /// </summary>
        /// <param name="disposer">An action which will dispose the unit test's resources.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication WithDispose(Action disposer);

        /// <summary>
        /// Registers an action to be performed on the specified frame.
        /// </summary>
        /// <param name="frame">The index of the frame in which to perform the action.</param>
        /// <param name="action">The action to perform on the specified frame.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication OnFrame(Int32 frame, Action<IUltravioletTestApplication> action);

        /// <summary>
        /// Skips the specified number of frames prior to rendering the tested scene.
        /// </summary>
        /// <param name="frameCount">The number of frames to skip.</param>
        /// <returns>The Ultraviolet test application.</returns>
        IUltravioletTestApplication SkipFrames(Int32 frameCount);        

        /// <summary>
        /// Renders a scene and outputs the resulting image.
        /// </summary>
        /// <param name="renderer">An action which will render the desired scene.</param>
        /// <returns>A bitmap containing the result of rendering the specified scene.</returns>
        Bitmap Render(Action<UltravioletContext> renderer);

        /// <summary>
        /// Runs the application until the specified predicate is true.
        /// </summary>
        /// <param name="predicate">The predicate that evaluates when the application should exit.</param>
        void RunUntil(Func<Boolean> predicate);

        /// <summary>
        /// Runs the application until the specified period of time has elapsed.
        /// </summary>
        /// <param name="time">The amount of time for which to run the application.</param>
        void RunFor(TimeSpan time);

        /// <summary>
        /// Runs a single frame of the application.
        /// </summary>
        void RunForOneFrame();

        /// <summary>
        /// Runs the application until there are no more frame actions.
        /// </summary>
        void RunAllFrameActions();

        /// <summary>
        /// Immediately pushes a message onto the Ultraviolet message queue which spoofs a key down event.
        /// </summary>
        /// <param name="scancode">The <see cref="Scancode"/> value of the key to press.</param>
        /// <param name="key">The <see cref="Key"/> value of the key to press.</param>
        /// <param name="ctrl">A value indicating whether the Ctrl modifier is active.</param>
        /// <param name="alt">A value indicating whether the Alt modifier is active.</param>
        /// <param name="shift">A value indicating whether the Shift modifier is active.</param>
        void SpoofKeyDown(Scancode scancode, Key key, Boolean ctrl, Boolean alt, Boolean shift);

        /// <summary>
        /// Immediately pushes a message onto the Ultraviolet message queue which spoofs a key up event.
        /// </summary>
        /// <param name="scancode">The <see cref="Scancode"/> value of the key to release.</param>
        /// <param name="key">The <see cref="Key"/> value of the key to release.</param>
        /// <param name="ctrl">A value indicating whether the Ctrl modifier is active.</param>
        /// <param name="alt">A value indicating whether the Alt modifier is active.</param>
        /// <param name="shift">A value indicating whether the Shift modifier is active.</param>
        void SpoofKeyUp(Scancode scancode, Key key, Boolean ctrl, Boolean alt, Boolean shift);

        /// <summary>
        /// Immediately pushes a message onto the Ultraviolet message queue which spoofs a key down event,
        /// followed by a message which spoofs a key up event.
        /// </summary>
        /// <param name="scancode">The <see cref="Scancode"/> value of the key to press.</param>
        /// <param name="key">The <see cref="Key"/> value of the key to press.</param>
        /// <param name="ctrl">A value indicating whether the Ctrl modifier is active.</param>
        /// <param name="alt">A value indicating whether the Alt modifier is active.</param>
        /// <param name="shift">A value indicating whether the Shift modifier is active.</param>
        void SpoofKeyPress(Scancode scancode, Key key, Boolean ctrl, Boolean alt, Boolean shift);

        /// <summary>
        /// Gets the Ultraviolet context.
        /// </summary>
        UltravioletContext Ultraviolet { get; }
    }
}
