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

namespace _2742117_2730729_2744585_2742117_UA1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string entreeactuelle = "0";
        private string operation = "";
        private double premierNbr = 0;
        private double deuxiemeNmb = 0;
        private bool nouvelleopr = true;
        private bool oprFonctionne = false;

        public MainWindow()
            {
                InitializeComponent();
                UpdateDisplay();
            }

            /// <summary>
            /// Met à jour l'affichage de la calculatrice
            /// </summary>
            private void UpdateDisplay()
            {
                txtResult.Text = entreeactuelle;
                txtOperation.Text = operation;
            }

            /// <summary>
            /// Gestion des clics sur les boutons chiffres (0-9)
            /// </summary>
            private void NumberButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string number = button.Content.ToString();

                if (entreeactuelle == "0" || nouvelleopr || oprFonctionne)
                {
                    entreeactuelle = number;
                    nouvelleopr = false;
                    oprFonctionne = false;
                }
                else
                {
                    entreeactuelle += number;
                }

                UpdateDisplay();
            }

            /// <summary>
            /// Gestion des clics sur les opérateurs (+, -, ×, ÷)
            /// </summary>
            private void OperatorButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string newOperator = button.Content.ToString();

                if (!nouvelleopr)
                {
                    if (!string.IsNullOrEmpty(operation))
                    {
                        CalculateResult();
                    }
                premierNbr = double.Parse(entreeactuelle);
                }

                operation = newOperator;
                nouvelleopr = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Gestion du clic sur le bouton égal (=)
            /// </summary>
            private void EqualsButton_Click(object sender, RoutedEventArgs e)
            {
                if (!string.IsNullOrEmpty(operation) && !nouvelleopr)
                {
                    CalculateResult();
                    operation = "";
                    nouvelleopr = true;
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Effectue le calcul selon l'opération en cours
            /// </summary>
            private void CalculateResult()
            {
                try
                {
                    deuxiemeNmb = double.Parse(entreeactuelle);
                    double result = 0;

                    switch (operation)
                    {
                        case "+":
                            result = premierNbr + deuxiemeNmb;
                            break;
                        case "-":
                            result = premierNbr - deuxiemeNmb;
                            break;
                        case "×":
                            result = premierNbr * deuxiemeNmb;
                            break;
                        case "÷":
                            if (deuxiemeNmb == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            result = premierNbr / deuxiemeNmb;
                            break;
                    }

                    entreeactuelle = result.ToString();
                    operation = $"{premierNbr} {operation} {deuxiemeNmb} =";
                }
                catch (DivideByZeroException)
                {
                    entreeactuelle = "Error";
                    operation = "Division par zéro!";
                }
                catch (Exception)
                {
                    entreeactuelle = "Error";
                    operation = "Erreur de calcul";
                }
            }

            /// <summary>
            /// Gestion des fonctions trigonométriques (Sin, Cos, Tan, Arcsin, Arccos, Arctan)
            /// </summary>
            private void FunctionButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string function = button.Content.ToString();
                double inputValue = double.Parse(entreeactuelle);
                double result = 0;
                double radians = inputValue * Math.PI / 180; // Conversion en radians

                try
                {
                    switch (function)
                    {
                        case "Sin":
                            result = Math.Sin(radians);
                            operation = $"Sin({inputValue})";
                            break;
                        case "Cos":
                            result = Math.Cos(radians);
                            operation = $"Cos({inputValue})";
                            break;
                        case "Tan":
                            result = Math.Tan(radians);
                            operation = $"Tan({inputValue})";
                            break;
                        case "Arcsin":
                            if (inputValue >= -1 && inputValue <= 1)
                            {
                                result = Math.Asin(inputValue) * 180 / Math.PI;
                                operation = $"Arcsin({inputValue})";
                            }
                            else
                            {
                                throw new ArgumentException("Valeur hors domaine");
                            }
                            break;
                        case "Arccos":
                            if (inputValue >= -1 && inputValue <= 1)
                            {
                                result = Math.Acos(inputValue) * 180 / Math.PI;
                                operation = $"Arccos({inputValue})";
                            }
                            else
                            {
                                throw new ArgumentException("Valeur hors domaine");
                            }
                            break;
                        case "Arctan":
                            result = Math.Atan(inputValue) * 180 / Math.PI;
                            operation = $"Arctan({inputValue})";
                            break;
                    }

                    entreeactuelle = result.ToString();
                    oprFonctionne = true;
                    UpdateDisplay();
                }
                catch (Exception ex)
                {
                    entreeactuelle = "Error";
                    operation = ex.Message;
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Gestion des constantes (π, e)
            /// </summary>
            private void ConstantButton_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                string constant = button.Content.ToString();

                switch (constant)
                {
                    case "π":
                        entreeactuelle = Math.PI.ToString();
                        operation = "π";
                        break;
                    case "e":
                        entreeactuelle = Math.E.ToString();
                        operation = "e";
                        break;
                }

                oprFonctionne = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Gestion du point décimal
            /// </summary>
            private void DecimalButton_Click(object sender, RoutedEventArgs e)
            {
                if (!entreeactuelle.Contains("."))
                {
                    entreeactuelle += ".";
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Gestion du changement de signe (+/-)
            /// </summary>
            private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
            {
                if (entreeactuelle != "0")
                {
                    if (entreeactuelle.StartsWith("-"))
                    {
                        entreeactuelle = entreeactuelle.Substring(1);
                    }
                    else
                    {
                        entreeactuelle = "-" + entreeactuelle;
                    }
                    UpdateDisplay();
                }
            }

            /// <summary>
            /// Efface tout (bouton C)
            /// </summary>
            private void CButton_Click(object sender, RoutedEventArgs e)
            {
                entreeactuelle = "0";
                operation = "";
                premierNbr = 0;
                deuxiemeNmb = 0;
                nouvelleopr = true;
                UpdateDisplay();
            }

            /// <summary>
            /// Efface l'entrée courante (bouton CE)
            /// </summary>
            private void CEButton_Click(object sender, RoutedEventArgs e)
            {
                entreeactuelle = "0";
                UpdateDisplay();
            }

            /// <summary>
            /// Efface le dernier caractère (bouton Back)
            /// </summary>
            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                if (entreeactuelle.Length > 1)
                {
                    entreeactuelle = entreeactuelle.Substring(0, entreeactuelle.Length - 1);
                }
                else
                {
                    entreeactuelle = "0";
                }
                UpdateDisplay();
            }
        }
    }
   