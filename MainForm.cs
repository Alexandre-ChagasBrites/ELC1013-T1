using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ELC1013_T1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ParseInput();
        }

        private void ParseInput()
        {
            Parser parser = new Parser(inputTextBox.Text);
            parser.Parse();

            errorsTextBox.Clear();
            foreach (string error in parser.Errors)
            {
                errorsTextBox.AppendText(error);
                errorsTextBox.AppendText(Environment.NewLine);
            }

            atomicsTextBox.Clear();
            for (int i = 0; i < parser.Atomics.Count; i++)
            {
                if (i > 0)
                    atomicsTextBox.AppendText(" | ");
                atomicsTextBox.AppendText(parser.Atomics[i]);
            }

            premissesTextBox.Clear();
            for (int i = 0; i < parser.Premises.Count; i++)
            {
                if (i > 0)
                    premissesTextBox.AppendText(" | ");
                premissesTextBox.AppendText(string.Join(string.Empty, parser.Premises[i].PrintProposition()));
            }

            Inferencer inferencer = new Inferencer();
            foreach (PremiseNode node in parser.Premises)
            {
                inferencer.Infer(node.node);
            }

            inferredTextBox.Clear();
            for (int i = 0; i < inferencer.Propositions.Count; i++)
            {
                inferredTextBox.AppendText($"{i + 1}. ");
                inferredTextBox.AppendText(string.Join(string.Empty, inferencer.Propositions[i].PrintProposition()));
                inferredTextBox.AppendText(" | ");
                inferredTextBox.AppendText(string.Join(string.Empty, inferencer.Propositions[i].PrintGuess()));
                inferredTextBox.AppendText(Environment.NewLine);
            }
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            ParseInput();
        }
    }
}
