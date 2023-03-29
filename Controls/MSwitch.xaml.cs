namespace MControls;

public partial class MSwitch : IMSwitch
{
    public event EventHandler<ToggledEventArgs> Toggled;

    #region Check First handler to toggle

    public static readonly BindableProperty IsInitialProperty = BindableProperty.Create(nameof(IsInitial),
                                                                                        typeof(bool),
                                                                                        typeof(MSwitch),
                                                                                        true, BindingMode.TwoWay,
                                                                                        propertyChanged: IsInicialChanged);

    private static void IsInicialChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMSwitch)bindable).SetInitial((bool)newValue);
    }
    void IMSwitch.SetInitial(bool value)
    {
        IsInitial = value;
    }
    public bool IsInitial
    {
        get => (bool)GetValue(IsInitialProperty);
        set => SetValue(IsInitialProperty, value);
    }
    void IMSwitch.IsToggledClicked(bool value)
    {
        if (!IsInitial)
            Toggled?.Invoke((object)this, new ToggledEventArgs(value));
    }

    #endregion

    #region positions

    Rect checketPosition = new Rect
    {
        Left = 20,
        Top = 1.5,
        Width = 16,
        Height = 16
    };

    Rect uncheckedPosition = new Rect
    {
        Left = 2,
        Top = 1.5,
        Width = 16,
        Height = 16
    };

    #endregion

    #region Toggled
    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled),
                                                                                typeof(bool),
                                                                                typeof(MSwitch),
                                                                                false, BindingMode.TwoWay,
                                                                                propertyChanged: IsToggledChanged);
    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }

    private static void IsToggledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((IMSwitch)bindable).IsToggledClicked((bool)newValue);
        ((IMSwitch)bindable).SetToggled((bool)newValue);
    }

    async void IMSwitch.SetToggled(bool value)
    {
        await ImAnElipse.TranslateTo(value ? 18 : 0, 0, 200, Easing.BounceOut);
        ImAnElipse.Fill = value ? new SolidColorBrush(Color.FromArgb("#04B404")) : new SolidColorBrush(Color.FromArgb("#FA5858"));
    }

    #endregion

    #region Visible

    #endregion

    public MSwitch()
    {
        InitializeComponent();
        this.SizeChanged += new EventHandler(MSwitch_SizeChanged);

    }

    private void MSwitch_SizeChanged(object sender, EventArgs e)
    {
        ((IMSwitch)this).SetToggled(IsToggled);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        IsToggled = !IsToggled;
    }
}

internal interface IMSwitch
{
    void IsToggledClicked(bool value);
    void SetToggled(bool value);
    void SetInitial(bool value);
}