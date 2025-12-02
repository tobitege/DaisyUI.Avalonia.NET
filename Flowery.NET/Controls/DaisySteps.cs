using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Flowery.Controls
{
    public class DaisySteps : ListBox
    {
        protected override Type StyleKeyOverride => typeof(DaisySteps);

        // We use SelectedIndex from ListBox.

        protected override void PrepareContainerForItemOverride(Control container, object? item, int index)
        {
            base.PrepareContainerForItemOverride(container, item, index);
            UpdateContainerClass(container, index);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == SelectedIndexProperty)
            {
                UpdateAllItemClasses();
            }
        }

        private void UpdateAllItemClasses()
        {
            var count = ItemCount; // ListBox has ItemCount
            for (int i = 0; i < count; i++)
            {
                var container = ContainerFromIndex(i);
                if (container != null)
                {
                    UpdateContainerClass(container, i);
                }
            }
        }

        private void UpdateContainerClass(Control container, int index)
        {
             // Add "active" class if index <= SelectedIndex
             if (index <= SelectedIndex)
             {
                 if (!container.Classes.Contains("active"))
                     container.Classes.Add("active");
             }
             else
             {
                 if (container.Classes.Contains("active"))
                     container.Classes.Remove("active");
             }
        }
    }
}
