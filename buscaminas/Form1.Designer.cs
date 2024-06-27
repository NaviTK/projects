using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Xml.Serialization;

namespace buscaminas
{
    partial class Form1
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
            checkBox1 = new CheckBox();
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
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(87, 124);
            checkBox1.Margin = new Padding(3, 2, 3, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(134, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "enquadrar imagenes";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 791);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(GENERAR);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbdecolumnas);
            Controls.Add(tbdefilas);
            Controls.Add(tablerocon);
            Name = "Form1";
            Text = "Form1";
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
        private CheckBox checkBox1;
    }
}