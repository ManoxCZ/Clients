using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace Clients.Behaviors;

public class FocusOnPropertyChangedBehavior : Behavior<Control>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is { })
        {
            AssociatedObject.PropertyChanged += FocuseControl;
        }
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (AssociatedObject is { })
        {
            AssociatedObject.PropertyChanged -= FocuseControl;
        }        
    }
    private void FocuseControl(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (AssociatedObject is { } &&
            !AssociatedObject.IsFocused)
        {
            AssociatedObject.Focus();
        }
    }
}
