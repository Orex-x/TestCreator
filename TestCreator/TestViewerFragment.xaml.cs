using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace TestCreator
{
    public partial class TestViewer : UserControl
    {
        public List<RadioButton> radioButtonsAnswers = new List<RadioButton>();
        public List<CheckBox> checkBoxesAnswers = new List<CheckBox>();
        public TestViewer()
        {
            InitializeComponent();
        }
    }
}
