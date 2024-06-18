using System.Windows;
using System.Windows.Input;
namespace fkfk
{
    public partial class MainWindow : Window
    {
        private float height;
        private float weight;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void txtWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return float.TryParse(text, out _);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(txtWeight.Text, out weight) && float.TryParse(txtHeight.Text, out height))
            {
                float bmi = CalculateBMI(weight, height);
                txtBmi.Content = bmi;
                
                string status = GetBMIStatus(bmi);
                txtStatus.Content = status;
            }
        }

        private float CalculateBMI(float weight, float height)
        {
            return MathF.Round(weight / MathF.Pow(height / 100, 2), 2);
        }

         private string GetBmiStatus(float bmi)
        {
            (float MinValue, float, string, Brush)[] bmiStatusRanges = new (float MinValue, float, string, Brush)[]
            {
                (float.MinValue, 18.5f, "Недостаточный вес \n Имеется риск угрозы здоровья",
                    Foreground = Brushes.LightGreen),
                (18.5f, 24.9f, "Нормальный вес", Foreground = Brushes.Chartreuse),
                (25.0f, 27.9f, "Избыточный вес \n Желательно уменьшение массы тела", Foreground = Brushes.Orange),
                (28.0f, 30.9f, "Ожирение первой степени \n Желательно уменьшение массы тела",
                    Foreground = Brushes.IndianRed),
                (31.0f, 35.9f, "Ожирение второй степени \n Крайне желательно уменьшение массы тела",
                    Foreground = Brushes.OrangeRed),
                (36.0f, 40.9f, "Ожирение третьей степени \n Крайне желательно уменьшение массы тела",
                    Foreground = Brushes.Red),
                (41.0f, float.MaxValue,
                    "Ожирение четвертой степени \n Необходимы срочные меры по уменьшению массы тела",
                    Foreground = Brushes.DarkRed),
            };
            foreach (var (min, max, status) in bmiStatusRanges)
            {
                if (bmi >= min && bmi < max)
                {
                    return status;
                }
            }
            return "Неизвестно";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtHeight.Text = "0";
            txtWeight.Text = "0";
            txtBmi.Content = string.Empty;
            txtStatus.Content = string.Empty;
        }
    }
}
