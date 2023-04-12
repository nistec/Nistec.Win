using System;
using System.Drawing;
using System.Windows.Forms;
using Nistec.Collections;

using System.Drawing.Drawing2D;

namespace Nistec.WinForms
{
    public interface IStyleLayout
    {
        Styles StylePlan { get;set;}
        void SetStyleLayout(StyleLayout value);
        void SetStyleLayout(Styles value);
        StyleLayout Layout { get;}
        void DrawButton(Graphics g, Rectangle rect, ButtonStates state, bool isDefault, string Text, ContentAlignment TextAlign, Image image, ContentAlignment ImageAlign, RightToLeft rtl, bool Enabled);
        void DrawButton(Graphics g, Rectangle rect, ButtonStates state, bool isDefault, bool Enabled);
        void DrawButtonRect(Graphics g, Rectangle rect, ButtonStates state, float radius);
        void DrawButton(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout, float radius, bool isDefault);
        void DrawCheckBox(Graphics g, Rectangle rect, ICheckBox ctl);
        void DrawCheckBox(Graphics g, Rectangle CheckRect, CheckBoxTypes type, Appearance appearance, ButtonStates state, bool Checked, bool Enabled);
        void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font);
        void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font, bool Enabled);
        void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font, RightToLeft rtl, bool Enabled);
        void DrawItem(Graphics graphics, Rectangle bounds, IMcList ctl, DrawItemState state, string text, int imageIndex);
        void DrawItem(Graphics graphics, Rectangle bounds, Control ctl, DrawItemState state, string text);
        void DrawString(Graphics g,Brush bs, Rectangle rect, ContentAlignment alignment, string Text, Font font);

        void DrawMenuItem(MenuItem sender, DrawItemEventArgs e, Image image, bool rightToLeft, bool drawBar);
        void DrawButtonRect(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout);
        void DrawBorder(Graphics g, Rectangle rect, bool readOnly, bool Enable, bool Focused, bool hot);
        void DrawControl(Graphics g, Rectangle rect, System.Windows.Forms.BorderStyle borderStyle, bool Enable, bool Focused, bool hot);
        void DrawMcEdit(Graphics g, Rectangle ctlRect, Control ctl, System.Windows.Forms.BorderStyle borderStyle, bool Enable, bool Focused, bool hot, bool readOnly);
        void DrawImage(Graphics g, Rectangle rect, Image image, ContentAlignment alignment, bool Enabled);
        void DrawShadow(Graphics g, Rectangle rect, int width, bool top);

        void DrawGradientRoundedRect(Graphics g, Rectangle rect, float radius, float angle);
        //void FillGradientRoundedRect(Graphics g, Rectangle rect, float radius, float angle);
        void FillGradientRect(Graphics g, Rectangle rect, float angle);
        void FillGradientPath(Graphics g, Rectangle rect, GraphicsPath path, float angle);
        void FillRect(Graphics g, Rectangle rect, Color color);
        //void FillRect(Graphics g, Rectangle rect, Brush sb);
        void DrawRect(Graphics g, Rectangle rect, Color color);
        //void DrawRect(Graphics g, Rectangle rect, Pen pen);
        void DrawPath(Graphics g, GraphicsPath path, Color color);
        //void DrawPath(Graphics g, GraphicsPath path, Pen pen);

        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        StringFormat GetStringFormat(ContentAlignment alignment, bool wordWrap, RightToLeft rtl);
        Brush GetBrushGradientDark(Rectangle rectangle, float angle);
        Brush GetBrushGradientDark(Rectangle rectangle, float angle, bool revers);
       
        Brush GetBrushGradient(Rectangle rectangle, float angle);
        Brush GetBrushGradient(Rectangle rectangle, float angle, bool revers);
        Brush GetBrushCaptionGradient(Rectangle rectangle, float angle, bool revers);
        Brush GetBrushGradient(Rectangle rectangle, Color start, Color end, float angle);

        Brush GetBrushFlat();
        Brush GetBrushFlat(FlatLayout value);
        Brush GetBrushClick();
        Brush GetBrushText();
        Brush GetBrushText(bool Enabled);
        Brush GetBrushText(bool Enabled, bool ReadOnly);
        Brush GetBrushBack();
        Brush GetBrushBack(bool Enabled);
        Brush GetBrushBack(bool Enabled, bool ReadOnly);
        Brush GetBrushSelected();
        Brush GetBrushCaption();
        Brush GetBrushFlatLayout();
        Brush GetBrushDisabled();
        Brush GetBrushContent();
        Brush GetBrushMenuBar(Rectangle rectangle, float angle);
        Brush GetBrushTextDisable();
        Brush GetBrushBar();
        Brush GetBrushHot();
        Brush GetBrushAlternating();
        Brush GetBrushSelectedText();
        Brush GetBrushCaptionText();
        Brush GetBrushHeader();
        Brush GetBrushHeaderText();

        Pen GetPenBorder();
        Pen GetPenHot();
        Pen GetPenFocused();
        Pen GetPenDisable();
        Pen GetPenDark();
        Pen GetPenLight();
        Pen GetPenInActive();
        Pen GetPenBorder(bool readOnly, bool Enable, bool Focused, bool hot);
        Pen GetPenButton();

        Font TextFont { get;}
        Font CaptionFont { get;}

    }

}
