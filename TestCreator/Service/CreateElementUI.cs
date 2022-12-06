using System;
using System.Windows;
using System.Windows.Controls;

namespace TestCreator.Service
{
    class CreateElementUI
    {
        public static TextBox getTextBox(double height, Thickness margin, string text,
            HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var textBoxtitle = new TextBox();
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Text = text;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }


        public static CheckBox getCheckBox(double height, Thickness margin, bool check, object o,
           HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var textBoxtitle = new CheckBox();
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Content = o;
            textBoxtitle.IsChecked = check;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }

        public static RadioButton getRadioButton(double height, Thickness margin, bool check, object o, string groupname,
          HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var textBoxtitle = new RadioButton();
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Content = o;
            textBoxtitle.GroupName = groupname;
            textBoxtitle.IsChecked = check;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }


        public static Button getButton(double width, double height, Thickness margin, string text,
         HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var button = new Button();
            button.Width = width;
            button.Height = height;
            button.Margin = margin;
            button.Content = text;
            button.HorizontalAlignment = horizontalAlignment;
            button.VerticalAlignment = verticalAlignment;
            return button;
        }
    }
}
