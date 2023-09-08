namespace GtaosPing
{
    partial class Main
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
            components = new System.ComponentModel.Container();
            IPList = new ListBox();
            IPBox = new TextBox();
            Add = new Button();
            Remove = new Button();
            StartStopTest = new Button();
            Output = new RichTextBox();
            PingViewer = new ScottPlot.FormsPlot();
            PingTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // IPList
            // 
            IPList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            IPList.FormattingEnabled = true;
            IPList.ItemHeight = 17;
            IPList.Location = new Point(12, 12);
            IPList.Name = "IPList";
            IPList.Size = new Size(189, 378);
            IPList.TabIndex = 0;
            // 
            // IPBox
            // 
            IPBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            IPBox.Location = new Point(12, 399);
            IPBox.Name = "IPBox";
            IPBox.Size = new Size(189, 23);
            IPBox.TabIndex = 1;
            // 
            // Add
            // 
            Add.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Add.Location = new Point(12, 428);
            Add.Name = "Add";
            Add.Size = new Size(86, 40);
            Add.TabIndex = 2;
            Add.Text = "添加";
            Add.UseVisualStyleBackColor = true;
            Add.Click += Add_Click;
            // 
            // Remove
            // 
            Remove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Remove.Location = new Point(104, 428);
            Remove.Name = "Remove";
            Remove.Size = new Size(97, 40);
            Remove.TabIndex = 3;
            Remove.Text = "删除";
            Remove.UseVisualStyleBackColor = true;
            Remove.Click += Remove_Click;
            // 
            // StartStopTest
            // 
            StartStopTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            StartStopTest.Location = new Point(12, 474);
            StartStopTest.Name = "StartStopTest";
            StartStopTest.Size = new Size(189, 36);
            StartStopTest.TabIndex = 4;
            StartStopTest.Text = "开始测试";
            StartStopTest.UseVisualStyleBackColor = true;
            StartStopTest.Click += StartStopTest_Click;
            // 
            // Output
            // 
            Output.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Output.Location = new Point(207, 399);
            Output.Name = "Output";
            Output.ReadOnly = true;
            Output.Size = new Size(701, 111);
            Output.TabIndex = 7;
            Output.Text = "";
            // 
            // PingViewer
            // 
            PingViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PingViewer.AutoSize = true;
            PingViewer.Location = new Point(208, 12);
            PingViewer.Margin = new Padding(4, 3, 4, 3);
            PingViewer.Name = "PingViewer";
            PingViewer.Size = new Size(700, 378);
            PingViewer.TabIndex = 8;
            // 
            // PingTimer
            // 
            PingTimer.Interval = 1000;
            PingTimer.Tick += PingTimer_Tick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 517);
            Controls.Add(PingViewer);
            Controls.Add(Output);
            Controls.Add(StartStopTest);
            Controls.Add(Remove);
            Controls.Add(Add);
            Controls.Add(IPBox);
            Controls.Add(IPList);
            Name = "Main";
            Text = "GtaosPing";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox IPList;
        private TextBox IPBox;
        private Button Add;
        private Button Remove;
        private Button StartStopTest;
        private RichTextBox Output;
        private ScottPlot.FormsPlot PingViewer;
        private System.Windows.Forms.Timer PingTimer;
    }
}