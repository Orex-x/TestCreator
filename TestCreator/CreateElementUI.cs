using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestCreator
{
    class CreateElementUI
    {
        public static TextBox getTextBox(double width, double height, Thickness margin, string text,
            HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            TextBox textBoxtitle = new TextBox();
            textBoxtitle.Width = width;
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Text = text;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }


        public static CheckBox getCheckBox(double width, double height, Thickness margin, string text,
           HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            CheckBox textBoxtitle = new CheckBox();
            TextBox box = new TextBox();
            box.Text = text;
            textBoxtitle.Width = width;
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Content = box;
            textBoxtitle.HorizontalAlignment = horizontalAlignment;
            textBoxtitle.VerticalAlignment = verticalAlignment;
            return textBoxtitle;
        }

        public static RadioButton getRadioButton(double width, double height, Thickness margin, string text,
          HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            RadioButton textBoxtitle = new RadioButton();
            TextBox box = new TextBox();
            box.Text = text;
            textBoxtitle.Width = width;
            textBoxtitle.Height = height;
            textBoxtitle.Margin = margin;
            textBoxtitle.Content = box;
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


        public static List<RadioButton> toRadioButton(List<CheckBox> checkBoxes)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            for(int i = 0; i < checkBoxes.Count; i++)
            {
                CheckBox check = checkBoxes[i];
                TextBox b = check.Content as TextBox;
                radioButtons.Add(getRadioButton(check.Width, check.Height, check.Margin,
                   b.Text, check.HorizontalAlignment, check.VerticalAlignment));
            }
            return radioButtons;
        }

        public static List<CheckBox> toCheckBox(List<RadioButton> radioButtons)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            for (int i = 0; i < radioButtons.Count; i++)
            {
                RadioButton radioButton = radioButtons[i];
                TextBox b = radioButton.Content as TextBox;
                checkBoxes.Add(getCheckBox(radioButton.Width, radioButton.Height, radioButton.Margin,
                   b.Text, radioButton.HorizontalAlignment, radioButton.VerticalAlignment));
            }
            return checkBoxes;
        }
    }
}
