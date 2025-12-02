using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Flowery.Controls
{
    public class DaisyCarousel : Carousel
    {
        protected override Type StyleKeyOverride => typeof(DaisyCarousel);

        private Button? _previousButton;
        private Button? _nextButton;
        private TransitioningContentControl? _transitionControl;
        private bool _isForward = true;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            // Unsubscribe from old buttons
            if (_previousButton != null)
                _previousButton.Click -= OnPreviousClick;
            if (_nextButton != null)
                _nextButton.Click -= OnNextClick;

            // Find and subscribe to new buttons
            _previousButton = e.NameScope.Find<Button>("PART_PreviousButton");
            _nextButton = e.NameScope.Find<Button>("PART_NextButton");
            _transitionControl = e.NameScope.Find<TransitioningContentControl>("PART_TransitioningContentControl");

            if (_previousButton != null)
                _previousButton.Click += OnPreviousClick;
            if (_nextButton != null)
                _nextButton.Click += OnNextClick;

            UpdateTransition();
        }

        private void UpdateTransition()
        {
            if (_transitionControl == null) return;

            var slide = new PageSlide(TimeSpan.FromMilliseconds(300), PageSlide.SlideAxis.Horizontal)
            {
                SlideInEasing = new CubicEaseOut(),
                SlideOutEasing = new CubicEaseIn()
            };

            _transitionControl.PageTransition = new DirectionalPageSlide(_isForward);
        }

        private void OnPreviousClick(object? sender, RoutedEventArgs e)
        {
            _isForward = false;
            UpdateTransition();
            Previous();
        }

        private void OnNextClick(object? sender, RoutedEventArgs e)
        {
            _isForward = true;
            UpdateTransition();
            Next();
        }
    }

    public class DirectionalPageSlide : IPageTransition
    {
        private readonly bool _forward;
        private readonly TimeSpan _duration = TimeSpan.FromMilliseconds(300);

        public DirectionalPageSlide(bool forward)
        {
            _forward = forward;
        }

        public async Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken)
        {
            if (to == null) return;

            var direction = _forward ? 1 : -1;
            var parentBounds = (to.GetVisualParent() as Visual)?.Bounds ?? new Rect(0, 0, 400, 300);
            var width = parentBounds.Width;

            var tasks = new List<Task>();

            if (from != null)
            {
                var outAnimation = new Animation
                {
                    Duration = _duration,
                    Easing = new CubicEaseInOut(),
                    Children =
                    {
                        new KeyFrame
                        {
                            Cue = new Cue(0),
                            Setters = { new Setter(Visual.OpacityProperty, 1.0), new Setter(TranslateTransform.XProperty, 0.0) }
                        },
                        new KeyFrame
                        {
                            Cue = new Cue(1),
                            Setters = { new Setter(Visual.OpacityProperty, 0.0), new Setter(TranslateTransform.XProperty, -direction * width) }
                        }
                    }
                };
                from.RenderTransform = new TranslateTransform();
                tasks.Add(outAnimation.RunAsync(from, cancellationToken));
            }

            var inAnimation = new Animation
            {
                Duration = _duration,
                Easing = new CubicEaseInOut(),
                Children =
                {
                    new KeyFrame
                    {
                        Cue = new Cue(0),
                        Setters = { new Setter(Visual.OpacityProperty, 0.0), new Setter(TranslateTransform.XProperty, direction * width) }
                    },
                    new KeyFrame
                    {
                        Cue = new Cue(1),
                        Setters = { new Setter(Visual.OpacityProperty, 1.0), new Setter(TranslateTransform.XProperty, 0.0) }
                    }
                }
            };
            to.RenderTransform = new TranslateTransform();
            tasks.Add(inAnimation.RunAsync(to, cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
