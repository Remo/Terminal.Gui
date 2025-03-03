﻿using Xunit.Abstractions;
using Color = Terminal.Gui.Color;

namespace Terminal.Gui.ViewsTests;

public class ColorPickerTests
{
    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_Construct_DefaultValue ()
    {
        var cp = GetColorPicker (ColorModel.HSV, false);

        // Should be only a single text field (Hex) because ShowTextFields is false
        Assert.Single (cp.Subviews.OfType<TextField> ());

        cp.Draw ();

        // All bars should be at 0 with the triangle at 0 (+2 because of "H:", "S:" etc)
        var h = GetColorBar (cp, ColorPickerPart.Bar1);
        Assert.Equal ("H:", h.Text);
        Assert.Equal (2, h.TrianglePosition);
        Assert.IsType<HueBar> (h);

        var s = GetColorBar (cp, ColorPickerPart.Bar2);
        Assert.Equal ("S:", s.Text);
        Assert.Equal (2, s.TrianglePosition);
        Assert.IsType<SaturationBar> (s);

        var v = GetColorBar (cp, ColorPickerPart.Bar3);
        Assert.Equal ("V:", v.Text);
        Assert.Equal (2, v.TrianglePosition);
        Assert.IsType<ValueBar> (v);

        var hex = GetTextField (cp, ColorPickerPart.Hex);
        Assert.Equal ("#000000", hex.Text);
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_RGB_KeyboardNavigation ()
    {
        var cp = GetColorPicker (ColorModel.RGB, false);
        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (2, r.TrianglePosition);
        Assert.IsType<RBar> (r);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.IsType<GBar> (g);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.IsType<BBar> (b);
        Assert.Equal ("#000000", hex.Text);

        Assert.IsAssignableFrom<IColorBar> (cp.Focused);

        cp.Draw ();

        Application.OnKeyDown (Key.CursorRight);

        cp.Draw ();

        Assert.Equal (3, r.TrianglePosition);
        Assert.Equal ("#0F0000", hex.Text);

        Application.OnKeyDown (Key.CursorRight);

        cp.Draw ();

        Assert.Equal (4, r.TrianglePosition);
        Assert.Equal ("#1E0000", hex.Text);

        // Use cursor to move the triangle all the way to the right
        for (int i = 0; i < 1000; i++)
        {
            Application.OnKeyDown (Key.CursorRight);
        }

        cp.Draw ();

        // 20 width and TrianglePosition is 0 indexed
        // Meaning we are asserting that triangle is at end
        Assert.Equal (19, r.TrianglePosition);
        Assert.Equal ("#FF0000", hex.Text);

        Application.Current.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_RGB_MouseNavigation ()
    {
        var cp = GetColorPicker (ColorModel.RGB,false);

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (2, r.TrianglePosition);
        Assert.IsType<RBar> (r);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.IsType<GBar> (g);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.IsType<BBar> (b);
        Assert.Equal ("#000000", hex.Text);

        Assert.IsAssignableFrom<IColorBar> (cp.Focused);

        cp.Focused.OnMouseEvent (
                                 new ()
                                 {
                                     Flags = MouseFlags.Button1Pressed,
                                     Position = new (3, 0)
                                 });

        cp.Draw ();

        Assert.Equal (3, r.TrianglePosition);
        Assert.Equal ("#0F0000", hex.Text);

        cp.Focused.OnMouseEvent (
                                  new ()
                                  {
                                      Flags = MouseFlags.Button1Pressed,
                                      Position = new (4, 0)
                                  });

        cp.Draw ();

        Assert.Equal (4, r.TrianglePosition);
        Assert.Equal ("#1E0000", hex.Text);

        Application.Current?.Dispose ();
    }


    public static IEnumerable<object []> ColorPickerTestData ()
    {
        yield return new object []
        {
            new Color(255, 0),
            "R:", 19, "G:", 2, "B:", 2, "#FF0000"
        };

        yield return new object []
        {
            new Color(0, 255),
            "R:", 2, "G:", 19, "B:", 2, "#00FF00"
        };

        yield return new object []
        {
            new Color(0, 0, 255),
            "R:", 2, "G:", 2, "B:", 19, "#0000FF"
        };

        yield return new object []
        {
            new Color(125, 125, 125),
            "R:", 11, "G:", 11, "B:", 11, "#7D7D7D"
        };
    }

    [Theory]
    [SetupFakeDriver]
    [MemberData (nameof (ColorPickerTestData))]
    public void ColorPicker_RGB_NoText (Color c, string expectedR, int expectedRTriangle, string expectedG, int expectedGTriangle, string expectedB, int expectedBTriangle, string expectedHex)
    {
        var cp = GetColorPicker (ColorModel.RGB, false);
        cp.SelectedColor = c;

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal (expectedR, r.Text);
        Assert.Equal (expectedRTriangle, r.TrianglePosition);
        Assert.Equal (expectedG, g.Text);
        Assert.Equal (expectedGTriangle, g.TrianglePosition);
        Assert.Equal (expectedB, b.Text);
        Assert.Equal (expectedBTriangle, b.TrianglePosition);
        Assert.Equal (expectedHex, hex.Text);

        Application.Current.Dispose ();
    }

    public static IEnumerable<object []> ColorPickerTestData_WithTextFields ()
    {
        yield return new object []
        {
            new Color(255, 0),
            "R:", 15, 255, "G:", 2, 0, "B:", 2, 0, "#FF0000"
        };

        yield return new object []
        {
            new Color(0, 255),
            "R:", 2, 0, "G:", 15, 255, "B:", 2, 0, "#00FF00"
        };

        yield return new object []
        {
            new Color(0, 0, 255),
            "R:", 2, 0, "G:", 2, 0, "B:", 15, 255, "#0000FF"
        };

        yield return new object []
        {
            new Color(125, 125, 125),
            "R:", 9, 125, "G:", 9, 125, "B:", 9, 125, "#7D7D7D"
        };
    }

    [Theory]
    [SetupFakeDriver]
    [MemberData (nameof (ColorPickerTestData_WithTextFields))]
    public void ColorPicker_RGB_NoText_WithTextFields (Color c, string expectedR, int expectedRTriangle, int expectedRValue, string expectedG, int expectedGTriangle, int expectedGValue, string expectedB, int expectedBTriangle, int expectedBValue, string expectedHex)
    {
        var cp = GetColorPicker (ColorModel.RGB, true);
        cp.SelectedColor = c;

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);
        var rTextField = GetTextField (cp, ColorPickerPart.Bar1);
        var gTextField = GetTextField (cp, ColorPickerPart.Bar2);
        var bTextField = GetTextField (cp, ColorPickerPart.Bar3);

        Assert.Equal (expectedR, r.Text);
        Assert.Equal (expectedRTriangle, r.TrianglePosition);
        Assert.Equal (expectedRValue.ToString (), rTextField.Text);
        Assert.Equal (expectedG, g.Text);
        Assert.Equal (expectedGTriangle, g.TrianglePosition);
        Assert.Equal (expectedGValue.ToString (), gTextField.Text);
        Assert.Equal (expectedB, b.Text);
        Assert.Equal (expectedBTriangle, b.TrianglePosition);
        Assert.Equal (expectedBValue.ToString (), bTextField.Text);
        Assert.Equal (expectedHex, hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_ClickingAtEndOfBar_SetsMaxValue ()
    {
        var cp = GetColorPicker (ColorModel.RGB, false);

        cp.Draw ();

        // Click at the end of the Red bar
        cp.Focused.OnMouseEvent (
                                 new ()
                                 {
                                     Flags = MouseFlags.Button1Pressed,
                                     Position = new (19, 0) // Assuming 0-based indexing
                                 });

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (19, r.TrianglePosition);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("#FF0000", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_ClickingBeyondBar_ChangesToMaxValue ()
    {
        var cp = GetColorPicker (ColorModel.RGB, false);

        cp.Draw ();

        // Click beyond the bar
        cp.Focused.OnMouseEvent (
                                 new ()
                                 {
                                     Flags = MouseFlags.Button1Pressed,
                                     Position = new (21, 0) // Beyond the bar
                                 });

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (19, r.TrianglePosition);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("#FF0000", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_ChangeValueOnUI_UpdatesAllUIElements ()
    {
        var cp = GetColorPicker (ColorModel.RGB, true);

        View otherView = new View () { CanFocus = true };

        Application.Current?.Add (otherView);

        cp.Draw ();

        // Change value using text field
        TextField rBarTextField = cp.Subviews.OfType<TextField> ().First (tf => tf.Text == "0");

        rBarTextField.Text = "128";
        //rBarTextField.OnLeave (cp); // OnLeave should be protected virtual. Don't call it.
        otherView.SetFocus (); // Remove focus from the color picker

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);
        var rTextField = GetTextField (cp, ColorPickerPart.Bar1);
        var gTextField = GetTextField (cp, ColorPickerPart.Bar2);
        var bTextField = GetTextField (cp, ColorPickerPart.Bar3);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (9, r.TrianglePosition);
        Assert.Equal ("128", rTextField.Text);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("0", gTextField.Text);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("0", bTextField.Text);
        Assert.Equal ("#800000", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_InvalidHexInput_DoesNotChangeColor ()
    {
        var cp = GetColorPicker (ColorModel.RGB, true);

        cp.Draw ();

        // Enter invalid hex value
        TextField hexField = cp.Subviews.OfType<TextField> ().First (tf => tf.Text == "#000000");
        hexField.Text = "#ZZZZZZ";
        hexField.OnLeave (cp);

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (2, r.TrianglePosition);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("#000000", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_ClickingDifferentBars_ChangesFocus ()
    {
        var cp = GetColorPicker (ColorModel.RGB, false);

        cp.Draw ();

        // Click on Green bar
        cp.Subviews.OfType<GBar> ()
          .Single ()
          .OnMouseEvent (
                         new ()
                         {
                             Flags = MouseFlags.Button1Pressed,
                             Position = new (0, 1)
                         });

        cp.Draw ();

        Assert.IsAssignableFrom<GBar> (cp.Focused);

        // Click on Blue bar
        cp.Subviews.OfType<BBar> ()
          .Single ()
          .OnMouseEvent (
                         new ()
                         {
                             Flags = MouseFlags.Button1Pressed,
                             Position = new (0, 2)
                         });

        cp.Draw ();

        Assert.IsAssignableFrom<BBar> (cp.Focused);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_SwitchingColorModels_ResetsBars ()
    {
        var cp = GetColorPicker (ColorModel.RGB, false);
        cp.SelectedColor = new (255, 0);

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (19, r.TrianglePosition);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("#FF0000", hex.Text);

        // Switch to HSV
        cp.Style.ColorModel = ColorModel.HSV;
        cp.ApplyStyleChanges ();

        cp.Draw ();

        var h = GetColorBar (cp, ColorPickerPart.Bar1);
        var s = GetColorBar (cp, ColorPickerPart.Bar2);
        var v = GetColorBar (cp, ColorPickerPart.Bar3);

        Assert.Equal ("H:", h.Text);
        Assert.Equal (2, h.TrianglePosition);
        Assert.Equal ("S:", s.Text);
        Assert.Equal (19, s.TrianglePosition);
        Assert.Equal ("V:", v.Text);
        Assert.Equal (19, v.TrianglePosition);
        Assert.Equal ("#FF0000", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_SyncBetweenTextFieldAndBars ()
    {
        var cp = GetColorPicker (ColorModel.RGB, true);

        cp.Draw ();

        // Change value using the bar
        RBar rBar = cp.Subviews.OfType<RBar> ().First ();
        rBar.Value = 128;

        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var hex = GetTextField (cp, ColorPickerPart.Hex);
        var rTextField = GetTextField (cp, ColorPickerPart.Bar1);
        var gTextField = GetTextField (cp, ColorPickerPart.Bar2);
        var bTextField = GetTextField (cp, ColorPickerPart.Bar3);

        Assert.Equal ("R:", r.Text);
        Assert.Equal (9, r.TrianglePosition);
        Assert.Equal ("128", rTextField.Text);
        Assert.Equal ("G:", g.Text);
        Assert.Equal (2, g.TrianglePosition);
        Assert.Equal ("0", gTextField.Text);
        Assert.Equal ("B:", b.Text);
        Assert.Equal (2, b.TrianglePosition);
        Assert.Equal ("0", bTextField.Text);
        Assert.Equal ("#800000", hex.Text);

        Application.Current?.Dispose ();
    }

    enum ColorPickerPart
    {
        Bar1 = 0,
        Bar2 = 1,
        Bar3 = 2,
        ColorName = 3,
        Hex = 4,
    }

    private TextField GetTextField (ColorPicker cp, ColorPickerPart toGet)
    {
        var hasBarValueTextFields = cp.Style.ShowTextFields;
        var hasColorNameTextField = cp.Style.ShowColorName;

        switch (toGet)
        {
            case ColorPickerPart.Bar1:
            case ColorPickerPart.Bar2:
            case ColorPickerPart.Bar3:
                if (!hasBarValueTextFields)
                {
                    throw new NotSupportedException ("Corresponding Style option is not enabled");
                }

                return cp.Subviews.OfType<TextField> ().ElementAt ((int)toGet);
            case ColorPickerPart.ColorName:
                if (!hasColorNameTextField)
                {
                    throw new NotSupportedException ("Corresponding Style option is not enabled");
                }

                return cp.Subviews.OfType<TextField> ().ElementAt (hasBarValueTextFields  ? (int)toGet : (int)toGet -3);
            case ColorPickerPart.Hex:

                int offset = hasBarValueTextFields ? 0 : 3;
                offset += hasColorNameTextField ? 0 : 1;

                return cp.Subviews.OfType<TextField> ().ElementAt ((int)toGet - offset);
            default:
                throw new ArgumentOutOfRangeException (nameof (toGet), toGet, null);
        }
    }

    private ColorBar GetColorBar (ColorPicker cp, ColorPickerPart toGet)
    {
        if (toGet <= ColorPickerPart.Bar3)
        {
            return cp.Subviews.OfType<ColorBar> ().ElementAt ((int)toGet);
        }
        throw new NotSupportedException ("ColorPickerPart must be a bar");
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_ChangedEvent_Fires ()
    {
        Color newColor = default;
        var count = 0;

        var cp = new ColorPicker ();

        cp.ColorChanged += (s, e) =>
        {
            count++;
            newColor = e.CurrentValue;

            Assert.Equal (cp.SelectedColor, e.CurrentValue);
        };

        cp.SelectedColor = new (1, 2, 3);
        Assert.Equal (1, count);
        Assert.Equal (new (1, 2, 3), newColor);

        cp.SelectedColor = new (2, 3, 4);

        Assert.Equal (2, count);
        Assert.Equal (new (2, 3, 4), newColor);

        // Set to same value
        cp.SelectedColor = new (2, 3, 4);

        // Should have no effect
        Assert.Equal (2, count);
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_DisposesOldViews_OnModelChange ()
    {
        var cp = GetColorPicker (ColorModel.HSL,true);

        var b1 = GetColorBar (cp, ColorPickerPart.Bar1);
        var b2 = GetColorBar (cp, ColorPickerPart.Bar2);
        var b3 = GetColorBar (cp, ColorPickerPart.Bar3);

        var tf1 = GetTextField (cp, ColorPickerPart.Bar1);
        var tf2 = GetTextField (cp, ColorPickerPart.Bar2);
        var tf3 = GetTextField (cp, ColorPickerPart.Bar3);

        var hex = GetTextField (cp, ColorPickerPart.Hex);

        Assert.All (new View [] { b1, b2, b3, tf1, tf2, tf3,hex }, b => Assert.False (b.WasDisposed));

        cp.Style.ColorModel = ColorModel.RGB;
        cp.ApplyStyleChanges ();

        var b1After = GetColorBar (cp, ColorPickerPart.Bar1);
        var b2After = GetColorBar (cp, ColorPickerPart.Bar2);
        var b3After = GetColorBar (cp, ColorPickerPart.Bar3);

        var tf1After = GetTextField (cp, ColorPickerPart.Bar1);
        var tf2After = GetTextField (cp, ColorPickerPart.Bar2);
        var tf3After = GetTextField (cp, ColorPickerPart.Bar3);

        var hexAfter = GetTextField (cp, ColorPickerPart.Hex);

        // Old bars should be disposed
        Assert.All (new View [] { b1, b2, b3, tf1, tf2, tf3,hex }, b => Assert.True (b.WasDisposed));
        Assert.NotSame (hex,hexAfter);

        Assert.NotSame (b1,b1After);
        Assert.NotSame (b2, b2After);
        Assert.NotSame (b3, b3After);

        Assert.NotSame (tf1, tf1After);
        Assert.NotSame (tf2, tf2After);
        Assert.NotSame (tf3, tf3After);

    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_TabCompleteColorName()
    {
        var cp = GetColorPicker (ColorModel.RGB, true,true);
        cp.Draw ();

        var r = GetColorBar (cp, ColorPickerPart.Bar1);
        var g = GetColorBar (cp, ColorPickerPart.Bar2);
        var b = GetColorBar (cp, ColorPickerPart.Bar3);
        var name = GetTextField (cp, ColorPickerPart.ColorName);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        name.FocusFirst (TabBehavior.TabStop);

        Assert.True (name.HasFocus);
        Assert.Same (name, cp.Focused);

        name.Text = "";
        Assert.Empty (name.Text);

        Application.OnKeyDown (Key.A);
        Application.OnKeyDown (Key.Q);

        Assert.Equal ("aq",name.Text);


        // Auto complete the color name
        Application.OnKeyDown (Key.Tab);

        Assert.Equal ("Aquamarine", name.Text);

        // Tab out of the text field
        Application.OnKeyDown (Key.Tab);

        Assert.False (name.HasFocus);
        Assert.NotSame (name, cp.Focused);

        Assert.Equal ("#7FFFD4", hex.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    [SetupFakeDriver]
    public void ColorPicker_EnterHexFor_ColorName ()
    {
        var cp = GetColorPicker (ColorModel.RGB, true, true);
        cp.Draw ();

        var name = GetTextField (cp, ColorPickerPart.ColorName);
        var hex = GetTextField (cp, ColorPickerPart.Hex);

        hex.FocusFirst (TabBehavior.TabStop);

        Assert.True (hex.HasFocus);
        Assert.Same (hex, cp.Focused);

        hex.Text = "";
        name.Text = "";

        Assert.Empty (hex.Text);
        Assert.Empty (name.Text);

        Application.OnKeyDown ('#');
        Assert.Empty (name.Text);
        //7FFFD4

        Assert.Equal ("#", hex.Text);
        Application.OnKeyDown ('7');
        Application.OnKeyDown ('F');
        Application.OnKeyDown ('F');
        Application.OnKeyDown ('F');
        Application.OnKeyDown ('D');
        Assert.Empty (name.Text);

        Application.OnKeyDown ('4');

        // Tab out of the text field
        Application.OnKeyDown (Key.Tab);
        Assert.False (hex.HasFocus);
        Assert.NotSame (hex, cp.Focused);

        // Color name should be recognised as a known string and populated
        Assert.Equal ("#7FFFD4", hex.Text);
        Assert.Equal("Aquamarine", name.Text);

        Application.Current?.Dispose ();
    }

    [Fact]
    public void TestColorNames ()
    {
        var colors = new W3CColors ();
        Assert.Contains ("Aquamarine", colors.GetColorNames ());
        Assert.DoesNotContain ("Save as",colors.GetColorNames ());
    }
    private ColorPicker GetColorPicker (ColorModel colorModel, bool showTextFields, bool showName = false)
    {
        var cp = new ColorPicker { Width = 20, SelectedColor = new (0, 0) };
        cp.Style.ColorModel = colorModel;
        cp.Style.ShowTextFields = showTextFields;
        cp.Style.ShowColorName = showName;
        cp.ApplyStyleChanges ();

        Application.Current = new Toplevel () { Width = 20 ,Height = 5};
        Application.Current.Add (cp);
        Application.Current.FocusFirst (null);

        Application.Current.LayoutSubviews ();

        Application.Current.FocusFirst (null);
        return cp;
    }
}
