using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCalc.Utils;

namespace WpfCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public static void PerformClick(Button btn)
            => btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //string DecimalSeparator = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        string DecimalSeparator = ",";
        InterfaceOperations? CurrentOperation;
        decimal FirstValue {  get; set; }
        decimal? SecondValue { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            btnSeparator.Content = DecimalSeparator;
            btnPlus.Tag = new Sum();
            btnMinus.Tag = new Substraction();
            btnMultiplication.Tag = new Multiplication();
            btnDivision.Tag = new Division();
        }

        

        public void SendToInput(string content)
        {
            if (input.Text == "0")
                input.Text = "";
            input.Text = $"{input.Text}{content}";
        }


        private void num_Click(object sender, RoutedEventArgs e)
        {
            SendToInput(((Button)sender).Content.ToString());
            //SendToInput($"{((Button)sender).Content.ToString()}");
            //void ValueSeparatorChange(decimal value)
            //{
            //    if (value.ToString().Contains(DecimalSeparator))
            //    {
            //        int DecimalSeparatorIndex = value.ToString().IndexOf(".");
            //        char[] valueArray = value.ToString().ToCharArray();
            //        valueArray[DecimalSeparatorIndex] = '.';
            //        valueArray.ToString();
            //    }
            //}
            //ValueSeparatorChange(SecondValue);
            //ValueSeparatorChange(FirstValue);
        }
        


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(input.Text == "0") return;
            input.Text = input.Text.Substring(0, input.Text.Length - 1);
            if(input.Text == "") input.Text = "0";
        }
        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOperation == null)
                FirstValue = Convert.ToDecimal(input.Text);
            CurrentOperation = (InterfaceOperations)((Button)sender).Tag;
            input.Text = "";
        }
        private void btnClear_Click(object sender, RoutedEventArgs e) => input.Text = "0";

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            FirstValue = 0;
            SecondValue = null;
            CurrentOperation = null;
            input.Text = "0";
        }

        private void btnSeparator_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text.Contains(DecimalSeparator)) return;

            num_Click(sender, e);
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
             
            if (CurrentOperation == null) return;
            if (input.Text == "") return;

            //decimal val2;
            //if (SecondValue == null)
            //    val2 = Convert.ToDecimal(input.Text);
            //else
            //    return;
            //decimal val2 = Convert.ToDecimal(input.Text);



            decimal val2 = SecondValue ?? Convert.ToDecimal(input.Text);
            //if (FirstValue.ToString().Contains(DecimalSeparator))
            //{
            //    int DecimalSeparatorIndex = FirstValue.ToString().IndexOf('.');
            //    char[] FirstValueArray = FirstValue.ToString().ToCharArray();
            //    FirstValueArray[DecimalSeparatorIndex] = ',';
            //}

            decimal result = CurrentOperation.DoOperation(FirstValue, val2);
            input.Text = result.ToString();
            FirstValue = result;
            SecondValue = val2;
        }


        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    SendToInput(e.Text);
                    break;
                case "*":
                    if (CurrentOperation != null)
                        input.Clear();
                    btnMultiplication.PerformClick();
                    break;
                case "/":
                    if (CurrentOperation != null)
                        input.Clear();
                    btnDivision.PerformClick();
                    break;
                case "+":
                    if (CurrentOperation != null)
                        input.Clear();
                    btnPlus.PerformClick();
                    break;
                case "-":
                    if (CurrentOperation != null)
                        input.Clear();
                    btnMinus.PerformClick();
                    break;
                case "=":
                    if (CurrentOperation != null)
                        input.Clear();
                    btnEqual.PerformClick();
                    break;

                default:
                    if (e.Text == DecimalSeparator)
                        btnSeparator.PerformClick();
                    else if (e.Text[0] == (char)8)
                        btnBack.PerformClick();
                    else if (e.Text[0] == (char)8)
                        btnEqual.PerformClick();
                    break;
            }
            btnEqual.Focus();
        }
    }
}