using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace Flowery.Controls
{
    public class DaisyAccordion : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyAccordion);

        public static readonly StyledProperty<DaisyCollapseVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyAccordion, DaisyCollapseVariant>(nameof(Variant), DaisyCollapseVariant.Arrow);

        public static readonly StyledProperty<int> ExpandedIndexProperty =
            AvaloniaProperty.Register<DaisyAccordion, int>(nameof(ExpandedIndex), -1);

        public DaisyCollapseVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public int ExpandedIndex
        {
            get => GetValue(ExpandedIndexProperty);
            set => SetValue(ExpandedIndexProperty, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ExpandedIndexProperty)
            {
                UpdateExpandedStates();
            }
            else if (change.Property == ItemCountProperty)
            {
                SyncItemVariants();
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            SyncItemVariants();
            UpdateExpandedStates();
        }

        internal void OnItemExpanded(DaisyAccordionItem expandedItem)
        {
            var items = this.GetLogicalChildren().OfType<DaisyAccordionItem>().ToList();
            int index = items.IndexOf(expandedItem);
            if (index >= 0)
            {
                ExpandedIndex = index;
            }

            foreach (var item in items)
            {
                if (item != expandedItem && item.IsExpanded)
                {
                    item.SetCurrentValue(DaisyAccordionItem.IsExpandedProperty, false);
                }
            }
        }

        private void UpdateExpandedStates()
        {
            var items = this.GetLogicalChildren().OfType<DaisyAccordionItem>().ToList();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetCurrentValue(DaisyAccordionItem.IsExpandedProperty, i == ExpandedIndex);
            }
        }

        private void SyncItemVariants()
        {
            foreach (var item in this.GetLogicalChildren().OfType<DaisyAccordionItem>())
            {
                item.SetCurrentValue(DaisyAccordionItem.VariantProperty, Variant);
            }
        }
    }

    public class DaisyAccordionItem : HeaderedContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyAccordionItem);

        private DaisyAccordion? _parentAccordion;

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<DaisyAccordionItem, bool>(nameof(IsExpanded));

        public static readonly StyledProperty<DaisyCollapseVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyAccordionItem, DaisyCollapseVariant>(nameof(Variant), DaisyCollapseVariant.Arrow);

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public DaisyCollapseVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == IsExpandedProperty && IsExpanded)
            {
                _parentAccordion?.OnItemExpanded(this);
            }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            _parentAccordion = this.FindAncestorOfType<DaisyAccordion>();
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            _parentAccordion = null;
        }
    }
}
