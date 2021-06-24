// project=SimTuning.WPF.UI, file=MvxContainer.cs, creation=2020:9:2 Copyright (c) 2021
// tuke productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SimTuning.WPF.UI.Region
{
    /// <summary>
    /// Helper class to create containers. It also define attached properties.
    /// </summary>
    public static class MvxContainer
    {
        /// <summary>
        /// The header property
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached("Header", typeof(object), typeof(MvxContainer),
                new UIPropertyMetadata(null));

        /// <summary>
        /// The holder type property
        /// </summary>
        public static readonly DependencyProperty HolderTypeProperty =
            DependencyProperty.RegisterAttached("HolderType", typeof(Type), typeof(MvxContainer),
                new PropertyMetadata(null));

        /// <summary>
        /// The identifier property
        /// </summary>
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.RegisterAttached("Id", typeof(string), typeof(MvxContainer),
                new PropertyMetadata("", IdChanged));

        /// <summary>
        /// The has closing action property
        /// </summary>
        internal static readonly DependencyProperty HasClosingActionProperty =
            DependencyProperty.RegisterAttached("HasClosingAction", typeof(bool), typeof(MvxContainer),
                new PropertyMetadata(false));

        /// <summary>
        /// The holder history property
        /// </summary>
        internal static readonly DependencyProperty HolderHistoryProperty =
            DependencyProperty.RegisterAttached("HolderHistory", typeof(Stack<FrameworkElement>), typeof(MvxContainer),
                new PropertyMetadata(null));

        /// <summary>
        /// The containers
        /// </summary>
        private static readonly Dictionary<string, WeakReference<ItemsControl>> containers = new Dictionary<string, WeakReference<ItemsControl>>();

        /// <summary>
        /// Gets the container by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static ItemsControl GetContainerById(string id)
        {
            ItemsControl container = null;
            if (containers.TryGetValue(id, out WeakReference<ItemsControl> reference))
                reference.TryGetTarget(out container);
            return container;
        }

        /// <summary>
        /// Gets the containers.
        /// </summary>
        /// <returns></returns>
        public static List<ItemsControl> GetContainers()
        {
            var items = new List<ItemsControl>();
            var deadItems = new List<string>();
            foreach (var item in containers)
            {
                if (item.Value.TryGetTarget(out ItemsControl container)) items.Add(container);
                else deadItems.Add(item.Key);
            }
            foreach (var item in deadItems) containers.Remove(item);
            return items;
        }

        /// <summary>
        /// Gets the first container.
        /// </summary>
        /// <returns></returns>
        public static ItemsControl GetFirstContainer()
        {
            foreach (var item in containers)
                if (item.Value.TryGetTarget(out ItemsControl container)) return container;
            return null;
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static object GetHeader(DependencyObject obj) => obj.GetValue(HeaderProperty);

        /// <summary>
        /// Gets the type of the holder.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Type GetHolderType(DependencyObject obj) => (Type)obj.GetValue(HolderTypeProperty);

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetId(ItemsControl obj) => (string)obj.GetValue(IdProperty);

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetHeader(DependencyObject obj, object value) => obj.SetValue(HeaderProperty, value);

        /// <summary>
        /// Sets the type of the holder.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetHolderType(DependencyObject obj, Type value) => obj.SetValue(HolderTypeProperty, value);

        /// <summary>
        /// Sets the identifier.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetId(ItemsControl obj, string value) => obj.SetValue(IdProperty, value);

        /// <summary>
        /// Gets the has closing action.
        /// </summary>
        /// <param name="holder">The holder.</param>
        /// <returns></returns>
        internal static bool GetHasClosingAction(ContentControl holder) => (bool)holder.GetValue(HasClosingActionProperty);

        /// <summary>
        /// Gets the holder history.
        /// </summary>
        /// <param name="holder">The holder.</param>
        /// <returns></returns>
        internal static Stack<FrameworkElement> GetHolderHistory(ContentControl holder) => (Stack<FrameworkElement>)holder.GetValue(HolderHistoryProperty);

        /// <summary>
        /// Sets the has closing action.
        /// </summary>
        /// <param name="holder">The holder.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        internal static void SetHasClosingAction(ContentControl holder, bool value) => holder.SetValue(HasClosingActionProperty, value);

        /// <summary>
        /// Sets the holder history.
        /// </summary>
        /// <param name="holder">The holder.</param>
        /// <param name="value">The value.</param>
        internal static void SetHolderHistory(ContentControl holder, Stack<FrameworkElement> value) => holder.SetValue(HolderHistoryProperty, value);

        /// <summary>
        /// Identifiers the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">
        /// The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the
        /// event data.
        /// </param>
        /// <exception cref="System.InvalidCastException">
        /// The container must be an ItemsControl
        /// </exception>
        private static void IdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldVlue = e.OldValue as string;
            var newValue = e.NewValue as string;
            var c = d as ItemsControl;
            if (c == null) throw new InvalidCastException("The container must be an ItemsControl");
            if (!string.IsNullOrWhiteSpace(oldVlue)) containers.Remove(oldVlue);
            if (!string.IsNullOrWhiteSpace(newValue))
            {
                if (containers.TryGetValue(newValue, out WeakReference<ItemsControl> oldWeakRef))
                {
                    containers.Remove(newValue);
                    if (oldWeakRef.TryGetTarget(out ItemsControl oldItemsControl))
                        oldItemsControl.ClearValue(IdProperty);
                }
                containers.Add(newValue, new WeakReference<ItemsControl>(c));
            }
        }
    }
}