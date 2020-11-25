// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Numerics;
using NetExtender.GUI.WinForms.Labels;
using NetExtender.Types.Lists;

namespace NetExtender.GUI.WinForms.Controls
{
    public class PageControl<T> : LocalizationControl where T : Control
    {
        public readonly EventQueueList<T> ControlsList;

        public event EmptyHandler PositionChanged;

        private TabAlignment _position = TabAlignment.Bottom;

        public TabAlignment Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position == value)
                {
                    return;
                }

                _position = value;
                PositionChanged?.Invoke();
            }
        }

        public event EmptyHandler AligmentChanged;

        private ContentAlignment _alignment = ContentAlignment.BottomLeft;

        public ContentAlignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                if (_alignment == value)
                {
                    return;
                }

                _alignment = value;
                AligmentChanged?.Invoke();
            }
        }

        public Boolean LoopedPages = true;

        public readonly Button PreviousPageButton;
        public readonly Button NextPageButton;
        public readonly CurrentMaxValueLabel PageValueLabel;

        public Int32 ButtonHeight { get; set; } = 32;

        public PageControl()
        {
            ControlsList = new EventQueueList<T>();

            Size size = new Size(100, ButtonHeight);

            PreviousPageButton = new Button
            {
                Size = size
            };

            NextPageButton = new Button
            {
                Size = size
            };

            PageValueLabel = new CurrentMaxValueLabel
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Separator = "\\"
            };
            PageValueLabel.SizeChanged += (sender, args) => SetSizeAndPosition();
            PageValueLabel.FontChanged += (sender, args) => SetLabelSize();
            PageValueLabel.TextChanged += (sender, args) => SetLabelSize();

            CurrentPageChanged += OnPageChanged;
            PreviousPageButton.Click += (sender, args) =>
            {
                if (ModifierKeys == Keys.Shift)
                {
                    CurrentPage = MinimumPage;
                    return;
                }

                CurrentPage--;
            };

            NextPageButton.Click += (sender, args) =>
            {
                if (ModifierKeys == Keys.Shift)
                {
                    CurrentPage = MaximumPage - 1;
                    return;
                }

                CurrentPage++;
            };

            ControlsList.OnAdd += AddToControls;
            ControlsList.OnRemove += RemoveFromControls;

            PositionChanged += SetSizeAndPosition;
            AligmentChanged += SetSizeAndPosition;
            ClientSizeChanged += (sender, args) => SetSizeAndPosition();

            Controls.Add(PreviousPageButton);
            Controls.Add(NextPageButton);
            Controls.Add(PageValueLabel);

            OnPageChanged();
        }

        private void AddToControls(ref T control)
        {
            Controls.Add(control);
            OnPageChanged();
        }

        private void RemoveFromControls(ref T control)
        {
            Controls.Remove(control);
            OnPageChanged();
        }

        public event EmptyHandler CurrentPageChanged;

        private Int32 _currentPage;

        public const Int32 MinimumPage = 0;

        public Int32 MaximumPage
        {
            get
            {
                return ControlsList.Count;
            }
        }

        public Int32 CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                Int32 nextPage = value.ToRange(MinimumPage, MaximumPage - 1);
                if (_currentPage == nextPage)
                {
                    return;
                }

                _currentPage = nextPage;
                CurrentPageChanged?.Invoke();
            }
        }

        public void Reload()
        {
            foreach (T control in ControlsList)
            {
                control.Visible = false;
            }

            if (ControlsList.Any())
            {
                ControlsList[CurrentPage.ToRange(MinimumPage, MaximumPage - 1)].Visible = true;
            }
        }

        private void OnPageChanged()
        {
            PageValueLabel.MaximumValue = MaximumPage;
            PageValueLabel.CurrentValue = CurrentPage + 1;
            Reload();
        }

        protected virtual void SetLabelSize()
        {
            PageValueLabel.Size = new Size((Int32) (TextRenderer.MeasureText(PageValueLabel.Text, PageValueLabel.Font).Width * 1.20),
                ButtonHeight);
        }

        protected virtual void SetSizeAndPosition()
        {
            InitializeButtons();
        }

        protected virtual void InitializeButtons()
        {
            Int32 previousPageLocationX;
            Int32 nextPageLocationX;
            Int32 pageLabelLocationX;
            switch (Alignment)
            {
                case ContentAlignment.BottomCenter:
                    previousPageLocationX = 0;
                    pageLabelLocationX = PreviousPageButton.Size.Width;
                    nextPageLocationX = pageLabelLocationX + PageValueLabel.Size.Width;
                    break;
                case ContentAlignment.BottomLeft:
                    pageLabelLocationX = 0;
                    previousPageLocationX = PageValueLabel.Size.Width;
                    nextPageLocationX = previousPageLocationX + PreviousPageButton.Size.Width;
                    break;
                case ContentAlignment.BottomRight:
                    previousPageLocationX = 0;
                    nextPageLocationX = PreviousPageButton.Size.Width;
                    pageLabelLocationX = PreviousPageButton.Size.Width + NextPageButton.Size.Width;
                    break;
                case ContentAlignment.TopCenter:
                    previousPageLocationX = Size.Width - NextPageButton.Size.Width - PageValueLabel.Size.Width -
                                            PreviousPageButton.Size.Width;
                    pageLabelLocationX = Size.Width - NextPageButton.Size.Width - PageValueLabel.Size.Width;
                    nextPageLocationX = Size.Width - NextPageButton.Size.Width;
                    break;
                case ContentAlignment.TopLeft:
                    pageLabelLocationX = Size.Width - NextPageButton.Size.Width - PreviousPageButton.Size.Width - PageValueLabel.Size.Width;
                    previousPageLocationX = Size.Width - NextPageButton.Size.Width - PreviousPageButton.Size.Width;
                    nextPageLocationX = Size.Width - NextPageButton.Size.Width;
                    break;
                case ContentAlignment.TopRight:
                    previousPageLocationX = Size.Width - PageValueLabel.Size.Width - NextPageButton.Size.Width -
                                            PreviousPageButton.Size.Width;
                    nextPageLocationX = Size.Width - PageValueLabel.Size.Width - NextPageButton.Size.Width;
                    pageLabelLocationX = Size.Width - PageValueLabel.Size.Width;
                    break;
                default:
                    throw new NotImplementedException();
            }

            Int32 previousPageLocationY;
            Int32 nextPageLocationY;
            Int32 pageLabelLocationY;
            switch (Position)
            {
                case TabAlignment.Bottom:
                    previousPageLocationY = Size.Height - PreviousPageButton.Size.Height;
                    nextPageLocationY = Size.Height - NextPageButton.Size.Height;
                    pageLabelLocationY = Size.Height - PageValueLabel.Size.Height;
                    break;
                default:
                    previousPageLocationY = 0;
                    nextPageLocationY = 0;
                    pageLabelLocationY = 0;
                    break;
            }

            PreviousPageButton.Location = new Point(previousPageLocationX, previousPageLocationY);
            NextPageButton.Location = new Point(nextPageLocationX, nextPageLocationY);
            PageValueLabel.Location = new Point(pageLabelLocationX, pageLabelLocationY);
        }

        public void Add(T control)
        {
            ControlsList.Add(control);
        }

        public void Remove(T control)
        {
            ControlsList.Remove(control);
        }
    }
}