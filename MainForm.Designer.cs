namespace ELC1013_T1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            inputTextBox = new TextBox();
            tableLayoutPanel = new TableLayoutPanel();
            inputGroupBox = new GroupBox();
            outputGroupBox = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            errorsGroupBox = new GroupBox();
            errorsTextBox = new TextBox();
            atomicsGroupBox = new GroupBox();
            atomicsTextBox = new TextBox();
            premissesGroupBox = new GroupBox();
            premissesTextBox = new TextBox();
            inferredGroupBox = new GroupBox();
            inferredTextBox = new TextBox();
            tableLayoutPanel.SuspendLayout();
            inputGroupBox.SuspendLayout();
            outputGroupBox.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            errorsGroupBox.SuspendLayout();
            atomicsGroupBox.SuspendLayout();
            premissesGroupBox.SuspendLayout();
            inferredGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // inputTextBox
            // 
            inputTextBox.Dock = DockStyle.Fill;
            inputTextBox.Location = new Point(3, 23);
            inputTextBox.Multiline = true;
            inputTextBox.Name = "inputTextBox";
            inputTextBox.ScrollBars = ScrollBars.Both;
            inputTextBox.Size = new Size(906, 162);
            inputTextBox.TabIndex = 0;
            inputTextBox.Text = resources.GetString("inputTextBox.Text");
            inputTextBox.TextChanged += InputTextBox_TextChanged;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(inputGroupBox, 0, 0);
            tableLayoutPanel.Controls.Add(outputGroupBox, 0, 1);
            tableLayoutPanel.Location = new Point(12, 12);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel.Size = new Size(918, 649);
            tableLayoutPanel.TabIndex = 4;
            // 
            // inputGroupBox
            // 
            inputGroupBox.Controls.Add(inputTextBox);
            inputGroupBox.Dock = DockStyle.Fill;
            inputGroupBox.Location = new Point(3, 3);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(912, 188);
            inputGroupBox.TabIndex = 0;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Entrada:";
            // 
            // outputGroupBox
            // 
            outputGroupBox.Controls.Add(tableLayoutPanel1);
            outputGroupBox.Dock = DockStyle.Fill;
            outputGroupBox.Location = new Point(3, 197);
            outputGroupBox.Name = "outputGroupBox";
            outputGroupBox.Size = new Size(912, 449);
            outputGroupBox.TabIndex = 1;
            outputGroupBox.TabStop = false;
            outputGroupBox.Text = "Saída:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(errorsGroupBox, 0, 0);
            tableLayoutPanel1.Controls.Add(atomicsGroupBox, 0, 1);
            tableLayoutPanel1.Controls.Add(premissesGroupBox, 0, 2);
            tableLayoutPanel1.Controls.Add(inferredGroupBox, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 23);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(906, 423);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // errorsGroupBox
            // 
            errorsGroupBox.Controls.Add(errorsTextBox);
            errorsGroupBox.Dock = DockStyle.Fill;
            errorsGroupBox.Location = new Point(3, 3);
            errorsGroupBox.Name = "errorsGroupBox";
            errorsGroupBox.Size = new Size(900, 145);
            errorsGroupBox.TabIndex = 3;
            errorsGroupBox.TabStop = false;
            errorsGroupBox.Text = "Erros:";
            // 
            // errorsTextBox
            // 
            errorsTextBox.Dock = DockStyle.Fill;
            errorsTextBox.Location = new Point(3, 23);
            errorsTextBox.Multiline = true;
            errorsTextBox.Name = "errorsTextBox";
            errorsTextBox.ReadOnly = true;
            errorsTextBox.ScrollBars = ScrollBars.Both;
            errorsTextBox.Size = new Size(894, 119);
            errorsTextBox.TabIndex = 0;
            // 
            // atomicsGroupBox
            // 
            atomicsGroupBox.Controls.Add(atomicsTextBox);
            atomicsGroupBox.Dock = DockStyle.Fill;
            atomicsGroupBox.Location = new Point(3, 154);
            atomicsGroupBox.Name = "atomicsGroupBox";
            atomicsGroupBox.Size = new Size(900, 54);
            atomicsGroupBox.TabIndex = 2;
            atomicsGroupBox.TabStop = false;
            atomicsGroupBox.Text = "Atomics:";
            // 
            // atomicsTextBox
            // 
            atomicsTextBox.Dock = DockStyle.Fill;
            atomicsTextBox.Location = new Point(3, 23);
            atomicsTextBox.Name = "atomicsTextBox";
            atomicsTextBox.ReadOnly = true;
            atomicsTextBox.ScrollBars = ScrollBars.Both;
            atomicsTextBox.Size = new Size(894, 27);
            atomicsTextBox.TabIndex = 1;
            // 
            // premissesGroupBox
            // 
            premissesGroupBox.Controls.Add(premissesTextBox);
            premissesGroupBox.Dock = DockStyle.Fill;
            premissesGroupBox.Location = new Point(3, 214);
            premissesGroupBox.Name = "premissesGroupBox";
            premissesGroupBox.Size = new Size(900, 54);
            premissesGroupBox.TabIndex = 1;
            premissesGroupBox.TabStop = false;
            premissesGroupBox.Text = "Premissas:";
            // 
            // premissesTextBox
            // 
            premissesTextBox.Dock = DockStyle.Fill;
            premissesTextBox.Location = new Point(3, 23);
            premissesTextBox.Name = "premissesTextBox";
            premissesTextBox.ReadOnly = true;
            premissesTextBox.ScrollBars = ScrollBars.Both;
            premissesTextBox.Size = new Size(894, 27);
            premissesTextBox.TabIndex = 2;
            // 
            // inferredGroupBox
            // 
            inferredGroupBox.Controls.Add(inferredTextBox);
            inferredGroupBox.Dock = DockStyle.Fill;
            inferredGroupBox.Location = new Point(3, 274);
            inferredGroupBox.Name = "inferredGroupBox";
            inferredGroupBox.Size = new Size(900, 146);
            inferredGroupBox.TabIndex = 0;
            inferredGroupBox.TabStop = false;
            inferredGroupBox.Text = "Inferido:";
            // 
            // inferredTextBox
            // 
            inferredTextBox.Dock = DockStyle.Fill;
            inferredTextBox.Location = new Point(3, 23);
            inferredTextBox.Multiline = true;
            inferredTextBox.Name = "inferredTextBox";
            inferredTextBox.ReadOnly = true;
            inferredTextBox.ScrollBars = ScrollBars.Both;
            inferredTextBox.Size = new Size(894, 120);
            inferredTextBox.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 673);
            Controls.Add(tableLayoutPanel);
            Name = "MainForm";
            Text = "Lógica Proposicional";
            tableLayoutPanel.ResumeLayout(false);
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            outputGroupBox.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            errorsGroupBox.ResumeLayout(false);
            errorsGroupBox.PerformLayout();
            atomicsGroupBox.ResumeLayout(false);
            atomicsGroupBox.PerformLayout();
            premissesGroupBox.ResumeLayout(false);
            premissesGroupBox.PerformLayout();
            inferredGroupBox.ResumeLayout(false);
            inferredGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox inputTextBox;
        private TableLayoutPanel tableLayoutPanel;
        private GroupBox inputGroupBox;
        private GroupBox outputGroupBox;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox inferredGroupBox;
        private GroupBox premissesGroupBox;
        private GroupBox atomicsGroupBox;
        private GroupBox errorsGroupBox;
        private TextBox inferredTextBox;
        private TextBox premissesTextBox;
        private TextBox atomicsTextBox;
        private TextBox errorsTextBox;
    }
}
