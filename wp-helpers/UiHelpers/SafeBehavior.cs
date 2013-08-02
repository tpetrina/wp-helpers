// Taken from http://wp7nl.codeplex.com/SourceControl/changeset/view/11210#Wp7nl/Wp7nl/Behaviors/SafeBehavior.cs
// The MIT License (MIT)
// Copyright (c) 2011 Joost van Schaik, #wp7nl
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the  "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge,  publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do  so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE  FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Wp7nl.Behaviors
{
    /// <summary>
    /// A base class implementing the safe event detachment pattern for behaviors.
    /// Optional re-init after page back navigation.
    /// </summary>
    /// <typeparam name="T">The framework element type this behavior attaches to</typeparam>
    public abstract class SafeBehavior<T> : Behavior<T> where T : FrameworkElement
    {
        protected SafeBehavior()
        {
            IsCleanedUp = true;
        }

        /// <summary>
        ///Setting this value to true in the constructor makes the behavior
        ///re-init after a page back event.
        /// </summary>
        protected bool ListenToPageBackEvent { get; set; }

        #region Setup

        /// <summary>
        /// The page this behavior is on
        /// </summary>
        protected PhoneApplicationFrame ParentPage;

        /// <summary>
        /// The uri of the page this behavior is on
        /// </summary>
        private Uri pageSource;

        protected override void OnAttached()
        {
            base.OnAttached();
            InitBehavior();
        }

        /// <summary>
        /// Does the initial wiring of events
        /// </summary>
        protected void InitBehavior()
        {
            if (IsCleanedUp)
            {
                IsCleanedUp = false;
                AssociatedObject.Loaded += AssociatedObjectLoaded;
                AssociatedObject.Unloaded += AssociatedObjectUnloaded;
            }
        }

        /// <summary>
        /// Does further event wiring and initialization after load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            // Find the page this control is on and listen to its orientation changed events
            if (ParentPage == null && ListenToPageBackEvent)
            {
                ParentPage = Application.Current.RootVisual as PhoneApplicationFrame;
                pageSource = ParentPage.CurrentSource;
                ParentPage.Navigated += ParentPageNavigated;
            }
            OnSetup();
        }

        /// <summary>
        /// Fired whe page navigation happens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentPageNavigated(object sender, NavigationEventArgs e)
        {
            // Re-setup when this page is navigated BACK to
            if (IsNavigatingBackToBehaviorPage(e))
            {
                if (IsCleanedUp)
                {
                    InitBehavior();
                }
            }
            OnParentPageNavigated(sender, e);
        }

        protected virtual void OnParentPageNavigated(object sender, NavigationEventArgs e)
        {

        }

        /// <summary>
        /// Override this to add your own setup
        /// </summary>
        protected virtual void OnSetup()
        {

        }

        /// <summary>
        /// Checks if the back navigation navigates back to the page
        /// on which this behavior is on
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected bool IsNavigatingBackToBehaviorPage(NavigationEventArgs e)
        {
            return (e.NavigationMode == NavigationMode.Back && e.Uri.Equals(pageSource));
        }

        #endregion

        #region Cleanup

        protected bool IsCleanedUp { get; private set; }

        /// <summary>
        /// Executes at OnDetaching or OnUnloaded (usually the last)
        /// </summary>
        private void Cleanup()
        {
            if (!IsCleanedUp)
            {
                AssociatedObject.Loaded -= AssociatedObjectLoaded;
                AssociatedObject.Unloaded -= AssociatedObjectUnloaded;
                OnCleanup();
                IsCleanedUp = true;
            }
        }

        protected override void OnDetaching()
        {
            Cleanup();
            base.OnDetaching();
        }

        private void AssociatedObjectUnloaded(object sender, RoutedEventArgs e)
        {
            Cleanup();
        }

        /// <summary>
        /// Override this to add your own cleanup
        /// </summary>
        protected virtual void OnCleanup()
        {
        }
        #endregion
    }
}