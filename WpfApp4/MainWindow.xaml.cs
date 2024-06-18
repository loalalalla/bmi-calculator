using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace _4040
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
            if (float.TryParse(TxtWeight.Text, out weight) && float.TryParse(TxtHeight.Text, out height))
            {
                float bmi = CalculateBMI(weight, height);
                TxtBmi.Content = bmi;
                
                string status = GetBmiStatus(bmi);
                TxtStatus.Content = status;
            }
        }

        private float CalculateBMI(float weight, float height)
        {
            return MathF.Round(weight / MathF.Pow(height / 100, 2), 2);
        }

        private string GetBmiStatus(float bmi)
        {
            (float MinValue, float MaxValue, string Status, Brush Foreground)[] bmiStatusRanges = new (float MinValue, float MaxValue, string Status, Brush Foreground)[]
            {
                (float.MinValue, 18.5f, "Недостаточный вес \n Имеется риск угрозы здоровья", Brushes.GreenYellow),
                (18.5f, 24.9f, "Нормальный вес", Brushes.Green),
                (25.0f, 27.9f, "Избыточный вес \n Желательно уменьшение массы тела", Brushes.Orange),
                (28.0f, 30.9f, "Ожирение первой степени \n Желательно уменьшение массы тела", Brushes.IndianRed),
                (31.0f, 35.9f, "Ожирение второй степени \n Крайне желательно уменьшение массы тела", Brushes.OrangeRed),
                (36.0f, 40.9f, "Ожирение третьей степени \n Крайне желательно уменьшение массы тела", Brushes.Red),
                (41.0f, float.MaxValue, "Ожирение четвертой степени \n Необходимы срочные меры по уменьшению массы тела", Brushes.DarkRed),
            };

            foreach (var (min, max, status, foreground) in bmiStatusRanges)
            {
                if (bmi >= min && bmi < max)
                {
                    TxtStatus.Foreground = foreground;
                    return status;
                }
            }

            return "Неизвестно";
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtHeight.Text = "0";
            TxtWeight.Text = "0";
            TxtBmi.Content = string.Empty;
            TxtStatus.Content = string.Empty;
        }
    }
}
