# WPF - Combobox with a null item
It adds an item for "Select All" or "Empty" functionality into a combobox
1. Converter
   ```csharp
    public class ComboBoxEmptyItemConverter : IValueConverter
    {
        /// <summary> 
        /// this object is the empty item in the combobox. A dynamic object that 
        /// returns null for all property request. 
        /// </summary> 
        private class EmptyItem : DynamicObject
        {

            public string Name { get; set; } = "Empty";
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                // just set the result to null and return true 
                result = null;
                return true;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // assume that the value at least inherits from IEnumerable 
            // otherwise we cannot use it. 
            IEnumerable container = value as IEnumerable;

            if (container != null)
            {
                // everything inherits from object, so we can safely create a generic IEnumerable 
                IEnumerable<object> genericContainer = container.OfType<object>();
                // create an array with a single EmptyItem object that serves to show en empty line 
                IEnumerable<object> emptyItem = new object[] { new EmptyItem() };
                // use Linq to concatenate the two enumerable 
                return emptyItem.Concat(genericContainer);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is EmptyItem ? null : value;
        }
    }
   ```
2. XAML
   ```xml

    <StackPanel>
        <ComboBox ItemsSource="{Binding  Items, Converter={StaticResource ComboBoxEmptyItemConverter}}" 
              SelectedItem="{Binding SelectedItem, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, Converter={StaticResource ComboBoxEmptyItemConverter}}"       
              DisplayMemberPath="Name" />
        <TextBlock Text="{Binding SelectedItem.Name}"/>
    </StackPanel>
    
    ```

    
   * https://github.com/atorres16/wpf-dropdown-null-item
   * https://stackoverflow.com/a/17903258/3596441
   * http://remyblok.tweakblogs.net/blog/7237/wpf-combo-box-with-empty-item-using-net-4-dynamic-objects.html