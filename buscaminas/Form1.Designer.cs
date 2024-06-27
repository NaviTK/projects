using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Xml.Serialization;

namespace buscaminas
{
    partial class Buscaminitas
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
            tablerocon = new Panel();
            tbdefilas = new TextBox();
            tbdecolumnas = new TextBox();
            label1 = new Label();
            label2 = new Label();
            GENERAR = new Button();
            label3 = new Label();
            bar = new HScrollBar();
            label4 = new Label();
            DifficultyBox = new TextBox();
            SuspendLayout();
            // 
            // tablerocon
            // 
            tablerocon.Location = new Point(90, 188);
            tablerocon.Name = "tablerocon";
            tablerocon.Size = new Size(350, 300);
            tablerocon.TabIndex = 0;
            // 
            // tbdefilas
            // 
            tbdefilas.Location = new Point(90, 44);
            tbdefilas.Name = "tbdefilas";
            tbdefilas.Size = new Size(40, 23);
            tbdefilas.TabIndex = 1;
            tbdefilas.Text = "10";
            // 
            // tbdecolumnas
            // 
            tbdecolumnas.Location = new Point(181, 44);
            tbdecolumnas.Name = "tbdecolumnas";
            tbdecolumnas.Size = new Size(40, 23);
            tbdecolumnas.TabIndex = 2;
            tbdecolumnas.Text = "10";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(56, 39);
            label1.Name = "label1";
            label1.Size = new Size(28, 28);
            label1.TabIndex = 3;
            label1.Text = "X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(148, 39);
            label2.Name = "label2";
            label2.Size = new Size(27, 28);
            label2.TabIndex = 4;
            label2.Text = "Y:";
            // 
            // GENERAR
            // 
            GENERAR.Location = new Point(264, 39);
            GENERAR.Name = "GENERAR";
            GENERAR.Size = new Size(224, 65);
            GENERAR.TabIndex = 5;
            GENERAR.Text = "GENERAR";
            GENERAR.UseVisualStyleBackColor = true;
            GENERAR.Click += gen_func;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 81);
            label3.Name = "label3";
            label3.Size = new Size(201, 15);
            label3.TabIndex = 6;
            label3.Text = "x and y must be less or equal than 40";
            // 
            // bar
            // 
            bar.LargeChange = 1;
            bar.Location = new Point(252, 148);
            bar.Maximum = 12;
            bar.Minimum = 4;
            bar.Name = "bar";
            bar.Size = new Size(135, 28);
            bar.TabIndex = 8;
            bar.Value = 4;
            bar.Scroll += bar_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(239, 120);
            label4.Name = "label4";
            label4.Size = new Size(75, 21);
            label4.TabIndex = 9;
            label4.Text = "Difficulty:";
            // 
            // DifficultyBox
            // 
            DifficultyBox.BorderStyle = BorderStyle.FixedSingle;
            DifficultyBox.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            DifficultyBox.Location = new Point(320, 118);
            DifficultyBox.Name = "DifficultyBox";
            DifficultyBox.ReadOnly = true;
            DifficultyBox.Size = new Size(13, 27);
            DifficultyBox.TabIndex = 10;
            DifficultyBox.Text = "1";
            // 
            // Buscaminitas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(984, 791);
            Controls.Add(DifficultyBox);
            Controls.Add(label4);
            Controls.Add(bar);
            Controls.Add(label3);
            Controls.Add(GENERAR);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbdecolumnas);
            Controls.Add(tbdefilas);
            Controls.Add(tablerocon);
            Name = "Buscaminitas";
            Text = "Buscaminitas";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel tablerocon;
        private TextBox tbdefilas;
        private TextBox tbdecolumnas;
        private Label label1;
        private Label label2;
        private Button GENERAR;
        private Label label3;
        private HScrollBar bar;
        private Label label4;
        private TextBox DifficultyBox;
    }
}