// project=SimTuning.Forms.UI, file=BehaviorBase.cs, creation=2020:7:31 Copyright (c) 2021
// tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Behaviors
{
    using System;
    using Xamarin.Forms;

    /// <summary>
    /// BehaviorBase.
    /// </summary>
    /// <typeparam name="T">object.</typeparam>
    /// <seealso cref="Xamarin.Forms.Behavior{T}" />
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}