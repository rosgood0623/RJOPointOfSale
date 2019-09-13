namespace KitchenScreenClient
{
    partial class Screen
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
            this.flpScreenFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBump = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnSecond = new System.Windows.Forms.Button();
            this.btnThird = new System.Windows.Forms.Button();
            this.btnFourth = new System.Windows.Forms.Button();
            this.btnFifth = new System.Windows.Forms.Button();
            this.btnSixth = new System.Windows.Forms.Button();
            this.lbConnectionEstablished = new System.Windows.Forms.Label();
            this.btnEstablishConn = new System.Windows.Forms.Button();
            this.btnUpArrow = new System.Windows.Forms.Button();
            this.btnDownArrow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flpScreenFlow
            // 
            this.flpScreenFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpScreenFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpScreenFlow.Location = new System.Drawing.Point(13, 13);
            this.flpScreenFlow.Name = "flpScreenFlow";
            this.flpScreenFlow.Size = new System.Drawing.Size(1028, 551);
            this.flpScreenFlow.TabIndex = 0;
            // 
            // btnBump
            // 
            this.btnBump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnBump.Location = new System.Drawing.Point(13, 591);
            this.btnBump.Name = "btnBump";
            this.btnBump.Size = new System.Drawing.Size(75, 85);
            this.btnBump.TabIndex = 1;
            this.btnBump.Text = "Bump";
            this.btnBump.UseVisualStyleBackColor = true;
            this.btnBump.Click += new System.EventHandler(this.Btn_BumpViaBump);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnFirst.Location = new System.Drawing.Point(94, 591);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 40);
            this.btnFirst.TabIndex = 1;
            this.btnFirst.Tag = "0";
            this.btnFirst.Text = "1";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // btnSecond
            // 
            this.btnSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSecond.Location = new System.Drawing.Point(175, 591);
            this.btnSecond.Name = "btnSecond";
            this.btnSecond.Size = new System.Drawing.Size(75, 40);
            this.btnSecond.TabIndex = 1;
            this.btnSecond.Tag = "1";
            this.btnSecond.Text = "2";
            this.btnSecond.UseVisualStyleBackColor = true;
            this.btnSecond.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // btnThird
            // 
            this.btnThird.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnThird.Location = new System.Drawing.Point(256, 591);
            this.btnThird.Name = "btnThird";
            this.btnThird.Size = new System.Drawing.Size(75, 40);
            this.btnThird.TabIndex = 1;
            this.btnThird.Tag = "2";
            this.btnThird.Text = "3";
            this.btnThird.UseVisualStyleBackColor = true;
            this.btnThird.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // btnFourth
            // 
            this.btnFourth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnFourth.Location = new System.Drawing.Point(94, 636);
            this.btnFourth.Name = "btnFourth";
            this.btnFourth.Size = new System.Drawing.Size(75, 40);
            this.btnFourth.TabIndex = 1;
            this.btnFourth.Tag = "3";
            this.btnFourth.Text = "4";
            this.btnFourth.UseVisualStyleBackColor = true;
            this.btnFourth.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // btnFifth
            // 
            this.btnFifth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnFifth.Location = new System.Drawing.Point(175, 636);
            this.btnFifth.Name = "btnFifth";
            this.btnFifth.Size = new System.Drawing.Size(75, 40);
            this.btnFifth.TabIndex = 1;
            this.btnFifth.Tag = "4";
            this.btnFifth.Text = "5";
            this.btnFifth.UseVisualStyleBackColor = true;
            this.btnFifth.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // btnSixth
            // 
            this.btnSixth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSixth.Location = new System.Drawing.Point(256, 636);
            this.btnSixth.Name = "btnSixth";
            this.btnSixth.Size = new System.Drawing.Size(75, 40);
            this.btnSixth.TabIndex = 1;
            this.btnSixth.Tag = "5";
            this.btnSixth.Text = "6";
            this.btnSixth.UseVisualStyleBackColor = true;
            this.btnSixth.Click += new System.EventHandler(this.Btn_BumpViaNumbers);
            // 
            // lbConnectionEstablished
            // 
            this.lbConnectionEstablished.AutoSize = true;
            this.lbConnectionEstablished.Location = new System.Drawing.Point(807, 663);
            this.lbConnectionEstablished.Name = "lbConnectionEstablished";
            this.lbConnectionEstablished.Size = new System.Drawing.Size(0, 13);
            this.lbConnectionEstablished.TabIndex = 2;
            // 
            // btnEstablishConn
            // 
            this.btnEstablishConn.Location = new System.Drawing.Point(774, 605);
            this.btnEstablishConn.Name = "btnEstablishConn";
            this.btnEstablishConn.Size = new System.Drawing.Size(165, 57);
            this.btnEstablishConn.TabIndex = 3;
            this.btnEstablishConn.Text = "Establish Connection With PoS";
            this.btnEstablishConn.UseVisualStyleBackColor = true;
            this.btnEstablishConn.Click += new System.EventHandler(this.Btn_EstablishConnectionWithPoS_Click);
            // 
            // btnUpArrow
            // 
            this.btnUpArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpArrow.Location = new System.Drawing.Point(419, 591);
            this.btnUpArrow.Name = "btnUpArrow";
            this.btnUpArrow.Size = new System.Drawing.Size(75, 40);
            this.btnUpArrow.TabIndex = 6;
            this.btnUpArrow.Text = "↑";
            this.btnUpArrow.UseVisualStyleBackColor = true;
            this.btnUpArrow.Click += new System.EventHandler(this.BtnUpArrow_Click);
            // 
            // btnDownArrow
            // 
            this.btnDownArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownArrow.Location = new System.Drawing.Point(419, 636);
            this.btnDownArrow.Name = "btnDownArrow";
            this.btnDownArrow.Size = new System.Drawing.Size(75, 40);
            this.btnDownArrow.TabIndex = 6;
            this.btnDownArrow.Text = "↓";
            this.btnDownArrow.UseVisualStyleBackColor = true;
            this.btnDownArrow.Click += new System.EventHandler(this.BtnDownArrow_Click);
            // 
            // Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 688);
            this.Controls.Add(this.btnDownArrow);
            this.Controls.Add(this.btnUpArrow);
            this.Controls.Add(this.btnEstablishConn);
            this.Controls.Add(this.lbConnectionEstablished);
            this.Controls.Add(this.btnSixth);
            this.Controls.Add(this.btnFifth);
            this.Controls.Add(this.btnFourth);
            this.Controls.Add(this.btnThird);
            this.Controls.Add(this.btnSecond);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnBump);
            this.Controls.Add(this.flpScreenFlow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Screen";
            this.Text = "Kitchen Screen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpScreenFlow;
        private System.Windows.Forms.Button btnBump;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnSecond;
        private System.Windows.Forms.Button btnThird;
        private System.Windows.Forms.Button btnFourth;
        private System.Windows.Forms.Button btnFifth;
        private System.Windows.Forms.Button btnSixth;
        private System.Windows.Forms.Label lbConnectionEstablished;
        private System.Windows.Forms.Button btnEstablishConn;
        private System.Windows.Forms.Button btnUpArrow;
        private System.Windows.Forms.Button btnDownArrow;
    }
}

