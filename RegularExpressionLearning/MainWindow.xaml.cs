using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module01VisualCsSyntaxReview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //LblFrenchBrackets.Content = @"{}";
        }

        private void BtnCheckExpression_Click(object sender, RoutedEventArgs e)
        {
            string enteredRegularExpression = TboRegularExpression.Text;
            string testString = TboStringToTest.Text;
            var result = Regex.IsMatch(testString, enteredRegularExpression, RegexOptions.None);
            if (result)
            {
                LblExpressionResult.Foreground = new SolidColorBrush(Colors.Green);
                LblExpressionResult.Content = "The Test String matches the Expression";
            }
            else
            {
                LblExpressionResult.Foreground = Brushes.Red;
                LblExpressionResult.Content = "The Test String Fails to match the expression";
            }

            // Working on email ^(\w+)(\.{0,1})(\w+)@{1,1}(\w+)\.{1,1}(\w{2,4})$
        }
    }
}
