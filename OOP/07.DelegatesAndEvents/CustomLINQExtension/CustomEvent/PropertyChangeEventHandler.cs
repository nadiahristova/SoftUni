using CustomLINQExtension.Entities;

public delegate void PropertyChangeEventHandler<T>(Student student, PropertyChangeEventArgs eventArgs);

public class PropertyChangeEventArgs
{
    public PropertyChangeEventArgs(string propertyName, object oldValue, object newValue)
    {
        this.PropertyName = propertyName;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }

    public string PropertyName { get; set; }
    public object OldValue { get; set; }
    public object NewValue { get; set; }
}
   