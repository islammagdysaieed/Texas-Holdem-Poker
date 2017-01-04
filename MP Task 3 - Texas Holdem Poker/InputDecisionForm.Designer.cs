namespace MP_Task_3___Texas_Holdem_Poker
{
    partial class InputDecisionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.decisionComboBox = new System.Windows.Forms.ComboBox();
            this.betTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.suggestedStatusTB = new System.Windows.Forms.TextBox();
            this.suggestedBetTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.hintButton = new System.Windows.Forms.Button();
            this.hint_RTB = new System.Windows.Forms.RichTextBox();
            this.ProbOfWinningLabel = new System.Windows.Forms.Label();
            this.probOfWinningTB = new System.Windows.Forms.TextBox();
            this.neededCards_RTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // decisionComboBox
            // 
            this.decisionComboBox.FormattingEnabled = true;
            this.decisionComboBox.Location = new System.Drawing.Point(33, 248);
            this.decisionComboBox.Name = "decisionComboBox";
            this.decisionComboBox.Size = new System.Drawing.Size(80, 21);
            this.decisionComboBox.TabIndex = 0;
            this.decisionComboBox.SelectedIndexChanged += new System.EventHandler(this.decisionComboBox_SelectedIndexChanged);
            // 
            // betTextBox
            // 
            this.betTextBox.Location = new System.Drawing.Point(139, 249);
            this.betTextBox.Name = "betTextBox";
            this.betTextBox.Size = new System.Drawing.Size(100, 20);
            this.betTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bet Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Status";
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(308, 249);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 4;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Suggested Decision";
            // 
            // suggestedStatusTB
            // 
            this.suggestedStatusTB.Location = new System.Drawing.Point(66, 195);
            this.suggestedStatusTB.Name = "suggestedStatusTB";
            this.suggestedStatusTB.Size = new System.Drawing.Size(67, 20);
            this.suggestedStatusTB.TabIndex = 7;
            // 
            // suggestedBetTB
            // 
            this.suggestedBetTB.Location = new System.Drawing.Point(139, 195);
            this.suggestedBetTB.Name = "suggestedBetTB";
            this.suggestedBetTB.Size = new System.Drawing.Size(67, 20);
            this.suggestedBetTB.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(483, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Quit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hintButton
            // 
            this.hintButton.Location = new System.Drawing.Point(342, 12);
            this.hintButton.Name = "hintButton";
            this.hintButton.Size = new System.Drawing.Size(54, 23);
            this.hintButton.TabIndex = 10;
            this.hintButton.Text = "hint?";
            this.hintButton.UseVisualStyleBackColor = true;
            this.hintButton.Click += new System.EventHandler(this.hintButton_Click);
            // 
            // hint_RTB
            // 
            this.hint_RTB.Location = new System.Drawing.Point(295, 41);
            this.hint_RTB.Name = "hint_RTB";
            this.hint_RTB.Size = new System.Drawing.Size(229, 110);
            this.hint_RTB.TabIndex = 11;
            this.hint_RTB.Text = "";
            // 
            // ProbOfWinningLabel
            // 
            this.ProbOfWinningLabel.AutoSize = true;
            this.ProbOfWinningLabel.Location = new System.Drawing.Point(267, 167);
            this.ProbOfWinningLabel.Name = "ProbOfWinningLabel";
            this.ProbOfWinningLabel.Size = new System.Drawing.Size(116, 13);
            this.ProbOfWinningLabel.TabIndex = 13;
            this.ProbOfWinningLabel.Text = "Probability of winning :";
            // 
            // probOfWinningTB
            // 
            this.probOfWinningTB.Location = new System.Drawing.Point(392, 164);
            this.probOfWinningTB.Name = "probOfWinningTB";
            this.probOfWinningTB.Size = new System.Drawing.Size(100, 20);
            this.probOfWinningTB.TabIndex = 12;
            // 
            // neededCards_RTB
            // 
            this.neededCards_RTB.Location = new System.Drawing.Point(33, 41);
            this.neededCards_RTB.Name = "neededCards_RTB";
            this.neededCards_RTB.Size = new System.Drawing.Size(229, 110);
            this.neededCards_RTB.TabIndex = 14;
            this.neededCards_RTB.Text = "";
            // 
            // InputDecisionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 284);
            this.Controls.Add(this.neededCards_RTB);
            this.Controls.Add(this.ProbOfWinningLabel);
            this.Controls.Add(this.probOfWinningTB);
            this.Controls.Add(this.hint_RTB);
            this.Controls.Add(this.hintButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.suggestedBetTB);
            this.Controls.Add(this.suggestedStatusTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.betTextBox);
            this.Controls.Add(this.decisionComboBox);
            this.Name = "InputDecisionForm";
            this.Text = "InputDecisionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox decisionComboBox;
        private System.Windows.Forms.TextBox betTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox suggestedStatusTB;
        private System.Windows.Forms.TextBox suggestedBetTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button hintButton;
        private System.Windows.Forms.RichTextBox hint_RTB;
        private System.Windows.Forms.Label ProbOfWinningLabel;
        private System.Windows.Forms.TextBox probOfWinningTB;
        private System.Windows.Forms.RichTextBox neededCards_RTB;

    }
}