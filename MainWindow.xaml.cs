using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.SharpDevelop.Editor;
using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Media;

namespace ELC1013_T1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextMarkerService textMarkerService;
        private Color errorColor = Color.FromRgb(0xa3, 0x15, 0x15);

        public MainWindow()
        {
            InitializeComponent();
            InitializeTextMarkerService();
        }

        void InitializeTextMarkerService()
        {
            textMarkerService = new TextMarkerService(inputTextBox.Document);
            inputTextBox.TextArea.TextView.BackgroundRenderers.Add(textMarkerService);
            inputTextBox.TextArea.TextView.LineTransformers.Add(textMarkerService);
            if (inputTextBox.Document.ServiceProvider.GetService(typeof(IServiceContainer)) is IServiceContainer services)
                services.AddService(typeof(ITextMarkerService), textMarkerService);
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            Parser parser = new Parser(inputTextBox.Text);
            parser.Parse();

            textMarkerService.RemoveAll(p => true);

            errorsTextBox.Inlines.Clear();
            foreach (Parser.Error error in parser.Errors)
            {
                ITextMarker marker = textMarkerService.Create(error.start, error.length);
                marker.MarkerTypes = TextMarkerTypes.SquigglyUnderline;
                marker.MarkerColor = errorColor;

                errorsTextBox.Inlines.Add(error.message);
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
