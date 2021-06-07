using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestCreator
{
    class CreateElementUI
    {
        public static TextBox getTextBox( double height, Thickness margin, string text, 
            HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            TextBox textBoxtitle = new TextBox(); 
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Text = text;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }


        public static CheckBox getCheckBox(double height, Thickness margin, bool check, Object o, 
           HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            CheckBox textBoxtitle = new CheckBox();
            //textBoxtitle.Width = width;
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Content = o;
            textBoxtitle.IsChecked = check;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }

        public static RadioButton getRadioButton(double height, Thickness margin, bool check, Object o, string groupname,
          HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            RadioButton textBoxtitle = new RadioButton();
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
            Button button = new Button();
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
