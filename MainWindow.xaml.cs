using System.Windows;

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
            //0xa31515
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            Parser parser = new Parser(inputTextBox.Text);
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
