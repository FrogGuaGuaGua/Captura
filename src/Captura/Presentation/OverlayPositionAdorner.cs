﻿using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Captura
{
    public class OverlayPositionAdorner : Adorner
    {
        #region Thumbs
        readonly Thumb _topLeft;
        readonly Thumb _topRight;
        readonly Thumb _bottomLeft;
        readonly Thumb _bottomRight;

        readonly Thumb _top;
        readonly Thumb _left;
        readonly Thumb _right;
        readonly Thumb _bottom;

        readonly Thumb _center;
        #endregion

        readonly bool _canResize;

        readonly VisualCollection _visualChildren;

        public OverlayPositionAdorner(UIElement Element, bool CanResize = true) : base(Element)
        {
            _canResize = CanResize;

            _visualChildren = new VisualCollection(this);

            BuildAdornerThumb(ref _center, Cursors.Hand);
            _center.Opacity = 0;

            _center.DragDelta += (S, E) => HandleDrag(HitType.Body, E);

            if (CanResize)
            {
                BuildAdornerThumb(ref _topLeft, Cursors.SizeNWSE);
                BuildAdornerThumb(ref _topRight, Cursors.SizeNESW);
                BuildAdornerThumb(ref _bottomLeft, Cursors.SizeNESW);
                BuildAdornerThumb(ref _bottomRight, Cursors.SizeNWSE);

                BuildAdornerThumb(ref _top, Cursors.SizeNS);
                BuildAdornerThumb(ref _left, Cursors.SizeWE);
                BuildAdornerThumb(ref _right, Cursors.SizeWE);
                BuildAdornerThumb(ref _bottom, Cursors.SizeNS);

                _topLeft.DragDelta += (S, E) => HandleDrag(HitType.UpperLeft, E);
                _topRight.DragDelta += (S, E) => HandleDrag(HitType.UpperRight, E);
                _bottomLeft.DragDelta += (S, E) => HandleDrag(HitType.LowerLeft, E);
                _bottomRight.DragDelta += (S, E) => HandleDrag(HitType.LowerRight, E);

                _top.DragDelta += (S, E) => HandleDrag(HitType.Top, E);
                _left.DragDelta += (S, E) => HandleDrag(HitType.Left, E);
                _right.DragDelta += (S, E) => HandleDrag(HitType.Right, E);
                _bottom.DragDelta += (S, E) => HandleDrag(HitType.Bottom, E);
            }

            Opacity = 0.01;

            MouseEnter += (S, E) => Opacity = 1;
            MouseLeave += (S, E) => Opacity = 0.01;
        }

        void HandleDrag(HitType MouseHitType, DragDeltaEventArgs Args)
        {
            if (!(AdornedElement is FrameworkElement fel))
                return;

            int offsetX = (int)Args.HorizontalChange;
            int offsetY = (int)Args.VerticalChange;

            bool har = fel.HorizontalAlignment == HorizontalAlignment.Right;
            bool vab = fel.VerticalAlignment == VerticalAlignment.Bottom;

            int newX = (int)(har ? fel.Margin.Right : fel.Margin.Left);
            int newY = (int)(vab ? fel.Margin.Bottom : fel.Margin.Top);
            int newWidth = (int)fel.ActualWidth;
            int newHeight = (int)fel.ActualHeight;

            void ModifyX(bool Possitive)
            {
                if (Possitive)
                    newX += offsetX;
                else newX -= offsetX;
            }

            void ModifyY(bool Possitive)
            {
                if (Possitive)
                    newY += offsetY;
                else newY -= offsetY;
            }

            void ModifyWidth(bool Possitive)
            {
                if (Possitive)
                    newWidth += offsetX;
                else newWidth -= offsetX;
            }

            void ModifyHeight(bool Possitive)
            {
                if (Possitive)
                    newHeight += offsetY;
                else newHeight -= offsetY;
            }

            switch (MouseHitType)
            {
                case HitType.Body:
                    ModifyX(!har);
                    ModifyY(!vab);
                    break;

                case HitType.UpperLeft:
                    if (har)
                    {
                        ModifyWidth(false);
                    }
                    else
                    {
                        ModifyX(true);
                        ModifyWidth(false);
                    }

                    if (vab)
                    {
                        ModifyHeight(false);
                    }
                    else
                    {
                        ModifyY(true);
                        ModifyHeight(false);
                    }
                    break;

                case HitType.UpperRight:
                    if (har)
                    {
                        ModifyX(false);
                        ModifyWidth(true);
                    }
                    else ModifyWidth(true);

                    if (vab)
                    {
                        ModifyHeight(false);
                    }
                    else
                    {
                        ModifyY(true);
                        ModifyHeight(false);
                    }
                    break;

                case HitType.LowerRight:
                    if (har)
                    {
                        ModifyX(false);
                        ModifyWidth(true);
                    }
                    else ModifyWidth(true);

                    if (vab)
                    {
                        ModifyY(false);
                        ModifyHeight(true);
                    }
                    else ModifyHeight(true);
                    break;

                case HitType.LowerLeft:
                    if (har)
                    {
                        ModifyWidth(false);
                    }
                    else
                    {
                        ModifyX(true);
                        ModifyWidth(false);
                    }

                    if (vab)
                    {
                        ModifyY(false);
                        ModifyHeight(true);
                    }
                    else ModifyHeight(true);
                    break;

                case HitType.Left:
                    if (har)
                    {
                        ModifyWidth(false);
                    }
                    else
                    {
                        ModifyX(true);
                        ModifyWidth(false);
                    }
                    break;

                case HitType.Right:
                    if (har)
                    {
                        ModifyX(false);
                        ModifyWidth(true);
                    }
                    else ModifyWidth(true);
                    break;

                case HitType.Bottom:
                    if (vab)
                    {
                        ModifyY(false);
                        ModifyHeight(true);
                    }
                    else ModifyHeight(true);
                    break;

                case HitType.Top:
                    if (vab)
                    {
                        ModifyHeight(false);
                    }
                    else
                    {
                        ModifyY(true);
                        ModifyHeight(false);
                    }
                    break;
            }

            if (newWidth > 0 && newHeight > 0)
            {
                if (newX < 0)
                {
                    newX = 0;
                }

                if (newY < 0)
                {
                    newY = 0;
                }

                double left = 0, top = 0, right = 0, bottom = 0;

                if (har)
                    right = newX;
                else left = newX;

                if (vab)
                    bottom = newY;
                else top = newY;

                fel.Margin = new Thickness(left, top, right, bottom);

                PositionUpdated?.Invoke(new Rect(newX, newY, newWidth, newHeight));

                if (MouseHitType != HitType.Body)
                {
                    fel.Width = newWidth;
                    fel.Height = newHeight;
                }
            }
        }

        public event Action<Rect> PositionUpdated;

        void BuildAdornerThumb(ref Thumb CornerThumb, Cursor CustomizedCursors)
        {
            if (CornerThumb != null)
                return;

            CornerThumb = new Thumb
            {
                Cursor = CustomizedCursors,
                Height = 10,
                Width = 10,
                Opacity = 0.5,
                Background = new SolidColorBrush(Colors.Red)
            };

            _visualChildren.Add(CornerThumb);
        }

        protected override Size ArrangeOverride(Size FinalSize)
        {
            base.ArrangeOverride(FinalSize);

            double desireWidth = AdornedElement.RenderSize.Width;
            double desireHeight = AdornedElement.RenderSize.Height;
            
            double adornerWidth = DesiredSize.Width;
            double adornerHeight = DesiredSize.Height;

            _center.Height = desireHeight;
            _center.Width = desireWidth;
            _center.Arrange(new Rect(0, 0, desireWidth, desireHeight));

            if (_canResize)
            {
                _topLeft.Arrange(new Rect(-adornerWidth * 0.5, -adornerHeight * 0.5, adornerWidth, adornerHeight));
                _topRight.Arrange(new Rect(desireWidth - adornerWidth * 0.5, -adornerHeight * 0.5, adornerWidth,
                    adornerHeight));
                _bottomLeft.Arrange(new Rect(-adornerWidth * 0.5, desireHeight - adornerHeight * 0.5, adornerWidth,
                    adornerHeight));
                _bottomRight.Arrange(new Rect(desireWidth - adornerWidth * 0.5, desireHeight - adornerHeight * 0.5,
                    adornerWidth, adornerHeight));

                _top.Arrange(new Rect(desireWidth * 0.5 - adornerWidth * 0.5, -adornerHeight * 0.5, adornerWidth,
                    adornerHeight));
                _left.Arrange(new Rect(-adornerWidth * 0.5, desireHeight * 0.5 - adornerHeight * 0.5, adornerWidth,
                    adornerHeight));
                _right.Arrange(new Rect(desireWidth - adornerWidth * 0.5, desireHeight * 0.5 - adornerHeight * 0.5,
                    adornerWidth, adornerHeight));
                _bottom.Arrange(new Rect(desireWidth * 0.5 - adornerWidth * 0.5, desireHeight - adornerHeight * 0.5,
                    adornerWidth, adornerHeight));
            }

            return FinalSize;
        }

        protected override int VisualChildrenCount => _visualChildren.Count;

        protected override Visual GetVisualChild(int Index) => _visualChildren[Index];
    }
}