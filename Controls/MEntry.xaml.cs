using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace MControls;

public partial class MEntry : IMEntry
{

    #region TEXT PROPERTY

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(MEntry),
                                                                                   string.Empty,
                                                                                   BindingMode.TwoWay,
                                                                                   propertyChanged: TextChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMEntry)bindable).Text((string)newValue);
    }

    void IMEntry.Text(string value)
    {
        Text = value;
    }

    #endregion

    #region ISPASSWORD PROPERTY
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),
                                                                                         typeof(bool),
                                                                                         typeof(MEntry),
                                                                                         false,
                                                                                         BindingMode.OneWay,
                                                                                         propertyChanged: IsPasswordChanged);


    private static void IsPasswordChanged(BindableObject bindable, object oldValue, object newObject)
    {
        ((IMEntry)bindable).IsPassword((bool)newObject);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    void IMEntry.IsPassword(bool value)
    {
        ImAnEntry.IsPassword = value;
    }

    #endregion

    #region PLACEHOLDER PROPERTY
    
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
                                                                                          typeof(string),
                                                                                          typeof(MEntry),
                                                                                          string.Empty,
                                                                                          BindingMode.OneWay,
                                                                                          propertyChanged: PlaceholderChanged);
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMEntry)bindable).Placeholder((string)newValue);
    }

    void IMEntry.Placeholder(string value)
    {
        ImALabel.Text = value;
    }
    
    #endregion

    #region ICON PROPERTY

    public static readonly BindableProperty IcoDataProperty = BindableProperty.Create(nameof(IcoData),
                                                                                      typeof(Microsoft.Maui.Controls.Shapes.Geometry),
                                                                                      typeof(MEntry),
                                                                                      null,
                                                                                      BindingMode.OneWay,
                                                                                      propertyChanged: IcoDataChanged);


    [TypeConverter(typeof(PathGeometryConverter))]
    public Geometry IcoData
    {
        get => (Geometry)GetValue(IcoDataProperty);
        set => SetValue(IcoDataProperty, value);
    }

    private static void IcoDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMEntry)bindable).SetIcoData((Geometry)newValue);
    }

    void IMEntry.SetIcoData(Geometry value)
    {
        ImAnIcon.Data = value;
    }

    #endregion

    #region MASK PROPERTY

    public static readonly BindableProperty MaskProperty = BindableProperty.Create(nameof(Mask),
                                                                                   typeof(string),
                                                                                   typeof(MEntry),
                                                                                   null,
                                                                                   BindingMode.OneWay,
                                                                                   propertyChanged: MaskChanged);


    public string Mask
    {
        get => (string)GetValue(MaskProperty);
        set => SetValue(MaskProperty, value);
    }

    private static void MaskChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMEntry)bindable).SetMask((string)newValue);
    }

    void IMEntry.SetMask(string value)
    {
        if (ImAnEntry.Behaviors.Count > 0)
        {
            ImAnEntry.Behaviors.RemoveAt(0);
        }

        if (!string.IsNullOrEmpty(value))
        {
            MaskedBehavior Mask = new MaskedBehavior();
            Mask.Mask = value;
            ImAnEntry.Behaviors.Add(Mask);
            ImAnEntry.MaxLength = value.Length;
        }
    }

    #endregion

    #region KEYBOARD PROPERTY

    public static readonly BindableProperty KeyBoardProperty = BindableProperty.Create(nameof(KeyBoard),
                                                                                      typeof(Keyboard),
                                                                                      typeof(MEntry),
                                                                                      Keyboard.Default,
                                                                                      BindingMode.OneWay,
                                                                                      propertyChanged: KeyBoardChanged);


    public Keyboard KeyBoard
    {
        get => (Keyboard)GetValue(KeyBoardProperty);
        set => SetValue(KeyBoardProperty, value);
    }

    private static void KeyBoardChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMEntry)bindable).SetKeyboard((Keyboard)newValue);
    }

    void IMEntry.SetKeyboard(Keyboard value)
    {
        ImAnEntry.Keyboard = value;
    }

    #endregion

    public MEntry()
    {
        InitializeComponent();
        this.ImAnEntry.TextChanged += new EventHandler<TextChangedEventArgs>(ImAnEntry_TextChanged);
        this.ImAnEntry.Focused += new EventHandler<FocusEventArgs>(ImAnEntry_Focused);
        this.ImAnEntry.Unfocused += new EventHandler<FocusEventArgs>(ImAnEntry_Unfocused);
    }

    private void ImAnEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(ImAnEntry.Text))
        {
            ScaleLabelUp();
        }
    }

    private void ImAnEntry_Focused(object sender, FocusEventArgs e)
    {
        ScaleLabelDown();
    }

    private void ImAnEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Text = e.NewTextValue;
    }

    private void ScaleLabelDown()
    {
        ImALabel.ScaleTo(0.8, 100, Easing.Linear);
        ImALabel.TranslateTo(0, -20, 100, Easing.Linear);

    }

    private void ScaleLabelUp()
    {
        ImALabel.ScaleTo(1, 100, Easing.Linear);
        ImALabel.TranslateTo(0, 0, 100, Easing.Linear);

    }
}

internal interface IMEntry
{
    void Text(string value);
    void Placeholder(string value);
    void SetIcoData(Microsoft.Maui.Controls.Shapes.Geometry value);
    void IsPassword(bool value);
    void SetMask(string value);
    void SetKeyboard(Keyboard value);
}