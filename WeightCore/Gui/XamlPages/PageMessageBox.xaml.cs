﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages
{
    /// <summary>
    /// Interaction logic for PageMessageBox.xaml
    /// </summary>
    public partial class PageMessageBox : UserControl
    {
        #region Private fields and properties

        public MessageBoxEntity MessageBox { get; set; } = new MessageBoxEntity();
        public RoutedEventHandler OnClose { get; set; }
        public Grid GridMain { get; private set; }

        #endregion

        #region Constructor and destructor

        public PageMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ushort colCount = GetGridColCount();
            ushort rowCount = GetGridRowCount();
            GridMain = GetGridMain(colCount, rowCount);

            ushort row = 0;
            GetFieldCaption(GridMain, colCount, ref row);
            GetFieldMessage(GridMain, colCount, ref row);

            ushort col = 0;
            GetButtonCustom(GridMain, ref col, row);
            GetButtonYes(GridMain, ref col, row);
            GetButtonRetry(GridMain, ref col, row);
            GetButtonNo(GridMain, ref col, row);
            GetButtonIgnore(GridMain, ref col, row);
            GetButtonCancel(GridMain, ref col, row);
            GetButtonAbort(GridMain, ref col, row);
            GetButtonOk(GridMain, ref col, row);

            borderMain.Child = GridMain;
            SetButtonFocus();
        }

        private Grid GetGridMain(ushort colCount, ushort rowCount)
        {
            Grid GridMain = new()
            {
                DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                Margin = new Thickness(2),
            };
            GridMain.KeyUp += Button_KeyUp;

            Grid.SetColumn(GridMain, 0);
            for (ushort col = 0; col < colCount; col++)
            {
                ColumnDefinition column = new() { Width = new GridLength(1, GridUnitType.Star) };
                GridMain.ColumnDefinitions.Add(column);
            }

            Grid.SetRow(GridMain, 0);
            if (rowCount <= 1)
            {
                RowDefinition row = new() { Height = new GridLength(MessageBox.SizeCaption, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row);
            }
            else if (rowCount == 2)
            {
                RowDefinition row = new() { Height = new GridLength(MessageBox.SizeMessage, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row);
                RowDefinition row2 = new() { Height = new GridLength(MessageBox.SizeButton, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row2);
            }
            else if (rowCount == 3)
            {
                RowDefinition row = new() { Height = new GridLength(MessageBox.SizeCaption, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row);
                RowDefinition row2 = new() { Height = new GridLength(MessageBox.SizeMessage, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row2);
                RowDefinition row3 = new() { Height = new GridLength(MessageBox.SizeButton, GridUnitType.Star) };
                GridMain.RowDefinitions.Add(row3);
            }

            FocusManager.SetIsFocusScope(GridMain, true);
            return GridMain;
        }

        private ushort GetGridColCount()
        {
            ushort count = 0;
            if (MessageBox.VisibilitySettings.ButtonCustomVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonYesVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonRetryVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonNoVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonIgnoreVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonCancelVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonAbortVisibility == Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonOkVisibility == Visibility.Visible)
                count++;
            return count;
        }

        private ushort GetGridRowCount()
        {
            ushort count = 1;
            if (!string.IsNullOrEmpty(MessageBox.Caption))
                count++;
            if (!string.IsNullOrEmpty(MessageBox.Message))
                count++;
            return count;
        }

        private void GetFieldCaption(Grid GridMain, ushort colCount, ref ushort row)
        {
            if (!string.IsNullOrEmpty(MessageBox.Caption))
            {
                TextBlock field = new()
                {
                    DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeCaption,
                    FontWeight = FontWeights.Bold,
                    FontStretch = FontStretches.Expanded,
                    TextDecorations = TextDecorations.Underline,
                    TextWrapping = TextWrapping.Wrap,
                    Background = System.Windows.Media.Brushes.Transparent,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                System.Windows.Data.Binding binding = new("Caption") { Mode = System.Windows.Data.BindingMode.OneWay, IsAsync = true, Source = MessageBox };
                System.Windows.Data.BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
                field.KeyUp += Button_KeyUp;
                Grid.SetColumn(field, 0);
                Grid.SetColumnSpan(field, colCount);
                Grid.SetRow(field, row);
                GridMain.Children.Add(field);
                row++;
            }
        }

        private void GetFieldMessage(Grid GridMain, ushort colCount, ref ushort row)
        {
            if (!string.IsNullOrEmpty(MessageBox.Message))
            {
                ScrollViewer scrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
                TextBlock field = new()
                {
                    DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeMessage,
                    FontWeight = FontWeights.Regular,
                    FontStretch = FontStretches.Normal,
                    TextWrapping = TextWrapping.Wrap,
                    Background = System.Windows.Media.Brushes.Transparent,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                System.Windows.Data.Binding binding = new("Message") { Mode = System.Windows.Data.BindingMode.OneWay, IsAsync = true, Source = MessageBox };
                System.Windows.Data.BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
                field.KeyUp += Button_KeyUp;

                scrollViewer.Content = field;
                Grid.SetColumn(scrollViewer, 0);
                Grid.SetColumnSpan(scrollViewer, colCount);
                Grid.SetRow(scrollViewer, row);
                GridMain.Children.Add(scrollViewer);
                row++;
            }
        }

        private void GetButtonCustom(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonCustomVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonCustomContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonCustom_OnClick;
                col++;
            }
        }

        private void GetButtonYes(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonYesVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonYesContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonYes_OnClick;
                col++;
            }
        }

        private void GetButtonRetry(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonRetryVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonRetryContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonRetry_OnClick;
                col++;
            }
        }

        private void GetButtonNo(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonNoVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonNoContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonNo_OnClick;
                col++;
            }
        }

        private void GetButtonIgnore(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonIgnoreVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonIgnoreContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonIgnore_OnClick;
                col++;
            }
        }

        private void GetButtonCancel(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonCancelVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonCancelContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonCancel_OnClick;
                col++;
            }
        }

        private void GetButtonAbort(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonAbortVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonAbortContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonAbort_OnClick;
                col++;
            }
        }

        private void GetButtonOk(Grid GridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonOkVisibility == Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonOkContent,
                    Margin = new Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                GridMain.Children.Add(button);
                button.Click += ButtonOk_OnClick;
                col++;
            }
        }

        private void SetButtonFocus()
        {
            foreach (object child in GridMain.Children)
            {
                if (child is Button button)
                {
                    //button.IsDefault = true;
                    //button.Focus();
                    button.KeyUp += Button_KeyUp;
                    button.Focusable = true;
                    Keyboard.Focus(button);
                    FocusManager.SetFocusedElement(GridMain, button);
                }
            }

            //FocusNavigationDirection focusNavigationDirection = FocusNavigationDirection.First;
            //TraversalRequest request = new(focusNavigationDirection);
            //UIElement element = Keyboard.FocusedElement as UIElement;
            //if (element != null)
            //{
            //    element.MoveFocus(request);
            //}
        }

        #endregion

        #region Public and private methods - Actions

        public void ButtonCustom_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Retry;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonYes_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Yes;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonRetry_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Retry;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonNo_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.No;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonIgnore_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Ignore;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Cancel;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonAbort_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Abort;
            OnClose?.Invoke(sender, e);
        }

        public void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.OK;
            OnClose?.Invoke(sender, e);
        }

        private void Button_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ButtonCancel_OnClick(sender, e);
            }
        }

        #endregion
    }
}
