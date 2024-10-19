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

namespace ELC1013_T1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string src = inputTextBox.Text;// new TextRange(inputTextBox.Document.ContentStart, inputTextBox.Document.ContentEnd).Text;
            Parser parser = new Parser(src);
            parser.Parse();

            errorsTextBox.Inlines.Clear();
            foreach (string error in parser.Errors)
            {
                errorsTextBox.Inlines.Add(error);
                errorsTextBox.Inlines.Add(Environment.NewLine);
            }

            atomicsTextBox.Inlines.Clear();
            for (int i = 0; i < parser.Atomics.Count; i++)
            {
                atomicsTextBox.Inlines.Add($"{i + 1}. ");
                atomicsTextBox.Inlines.Add(parser.Atomics[i]);
                atomicsTextBox.Inlines.Add(Environment.NewLine);
            }

            premissesTextBox.Inlines.Clear();
            for (int i = 0; i < parser.Premises.Count; i++)
            {
                premissesTextBox.Inlines.Add($"{i + 1}. ");
                premissesTextBox.Inlines.Add(string.Join(string.Empty, parser.Premises[i].PrintProposition()));
                premissesTextBox.Inlines.Add(Environment.NewLine);
            }

            Inferencer inferencer = new Inferencer();
            foreach (PremiseNode node in parser.Premises)
            {
                inferencer.Infer(node.node);
            }

            inferredTextBox.Inlines.Clear();
            for (int i = 0; i < inferencer.Propositions.Count; i++)
            {
                inferredTextBox.Inlines.Add($"{i + 1}. ");
                inferredTextBox.Inlines.Add(string.Join(string.Empty, inferencer.Propositions[i].PrintProposition()));
                inferredTextBox.Inlines.Add(" | ");
                inferredTextBox.Inlines.Add(string.Join(string.Empty, inferencer.Propositions[i].PrintGuess()));
                inferredTextBox.Inlines.Add(Environment.NewLine);
            }
        }
    }
}
