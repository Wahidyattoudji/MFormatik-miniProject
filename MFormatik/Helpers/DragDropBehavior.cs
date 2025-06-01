using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace MFormatik.Helpers;


public class DragDropBehavior : Behavior<ListBox>
{
    public static readonly DependencyProperty DragDataProperty =
        DependencyProperty.Register(nameof(DragData), typeof(object), typeof(DragDropBehavior), new PropertyMetadata(null));

    public object DragData
    {
        get => GetValue(DragDataProperty);
        set => SetValue(DragDataProperty, value);
    }

    public static readonly DependencyProperty DropCommandProperty =
        DependencyProperty.Register(nameof(DropCommand), typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(null));

    public ICommand DropCommand
    {
        get => (ICommand)GetValue(DropCommandProperty);
        set => SetValue(DropCommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.Drop += OnDrop;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.Drop -= OnDrop;
    }

    private Point _dragStartPoint;

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _dragStartPoint = e.GetPosition(null);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        Point mousePos = e.GetPosition(null);
        Vector diff = _dragStartPoint - mousePos;

        if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
             Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
        {
            var listBox = sender as ListBox;
            var item = FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);
            if (item != null)
            {
                DragData = listBox.ItemContainerGenerator.ItemFromContainer(item);
                DragDrop.DoDragDrop(item, DragData, DragDropEffects.Move);
            }
        }
    }

    private void OnDrop(object sender, DragEventArgs e)
    {
        var data = e.Data.GetData(e.Data.GetFormats()[0]);
        var target = GetObjectDataFromPoint((ListBox)sender, e.GetPosition((ListBox)sender));

        if (DropCommand != null && DropCommand.CanExecute(new DropInfo { Data = data, Target = target }))
        {
            DropCommand.Execute(new DropInfo
            {
                Data = data,
                Target = target
            });
        }
    }

    private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
    {
        while (current != null)
        {
            if (current is T) return (T)current;
            current = VisualTreeHelper.GetParent(current);
        }
        return null;
    }

    private object GetObjectDataFromPoint(ListBox source, Point point)
    {
        var element = source.InputHitTest(point) as UIElement;
        if (element != null)
        {
            var item = FindAncestor<ListBoxItem>(element);
            if (item != null)
                return source.ItemContainerGenerator.ItemFromContainer(item);
        }
        return null;
    }
}

public class DropInfo
{
    public object Data { get; set; }
    public object Target { get; set; }
}
