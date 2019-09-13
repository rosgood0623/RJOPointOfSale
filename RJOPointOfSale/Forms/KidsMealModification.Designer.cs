namespace RJOPointOfSale
{
    partial class KidsMealModification
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
            this.lbModifiedKidsMeal = new System.Windows.Forms.ListBox();
            this.btnRegFry = new System.Windows.Forms.Button();
            this.btnTots = new System.Windows.Forms.Button();
            this.btnSweetFry = new System.Windows.Forms.Button();
            this.btnRegSmashFry = new System.Windows.Forms.Button();
            this.btnBrussels = new System.Windows.Forms.Button();
            this.btnSmashTots = new System.Windows.Forms.Button();
            this.btnRegSweetSmashFry = new System.Windows.Forms.Button();
            this.btnSideSalad = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.btnKidsSoda = new System.Windows.Forms.Button();
            this.btnKidsMilk = new System.Windows.Forms.Button();
            this.btnKidsChocMilk = new System.Windows.Forms.Button();
            this.btnKidsApple = new System.Windows.Forms.Button();
            this.btnPButterShake = new System.Windows.Forms.Button();
            this.btnVanillaShake = new System.Windows.Forms.Button();
            this.btnChocolateShake = new System.Windows.Forms.Button();
            this.btnStrawberryShake = new System.Windows.Forms.Button();
            this.btnOreoShake = new System.Windows.Forms.Button();
            this.btnSaltedCaramelShake = new System.Windows.Forms.Button();
            this.btnConfirmMods = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbModifiedKidsMeal
            // 
            this.lbModifiedKidsMeal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbModifiedKidsMeal.FormattingEnabled = true;
            this.lbModifiedKidsMeal.ItemHeight = 15;
            this.lbModifiedKidsMeal.Location = new System.Drawing.Point(3, 60);
            this.lbModifiedKidsMeal.Name = "lbModifiedKidsMeal";
            this.lbModifiedKidsMeal.Size = new System.Drawing.Size(285, 484);
            this.lbModifiedKidsMeal.TabIndex = 5;
            this.lbModifiedKidsMeal.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LbCustomerCheck_DrawItem);
            // 
            // btnRegFry
            // 
            this.btnRegFry.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRegFry.Location = new System.Drawing.Point(303, 102);
            this.btnRegFry.Name = "btnRegFry";
            this.btnRegFry.Size = new System.Drawing.Size(109, 103);
            this.btnRegFry.TabIndex = 6;
            this.btnRegFry.Tag = "RgFrenchFry";
            this.btnRegFry.Text = "Reg Fry";
            this.btnRegFry.UseVisualStyleBackColor = false;
            this.btnRegFry.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnTots
            // 
            this.btnTots.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTots.Location = new System.Drawing.Point(418, 102);
            this.btnTots.Name = "btnTots";
            this.btnTots.Size = new System.Drawing.Size(109, 103);
            this.btnTots.TabIndex = 6;
            this.btnTots.Tag = "Tots";
            this.btnTots.Text = "Tots";
            this.btnTots.UseVisualStyleBackColor = false;
            this.btnTots.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnSweetFry
            // 
            this.btnSweetFry.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSweetFry.Location = new System.Drawing.Point(533, 102);
            this.btnSweetFry.Name = "btnSweetFry";
            this.btnSweetFry.Size = new System.Drawing.Size(109, 103);
            this.btnSweetFry.TabIndex = 6;
            this.btnSweetFry.Tag = "RgSweetFry";
            this.btnSweetFry.Text = "Reg Sweet Fry";
            this.btnSweetFry.UseVisualStyleBackColor = false;
            this.btnSweetFry.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnRegSmashFry
            // 
            this.btnRegSmashFry.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRegSmashFry.Location = new System.Drawing.Point(303, 211);
            this.btnRegSmashFry.Name = "btnRegSmashFry";
            this.btnRegSmashFry.Size = new System.Drawing.Size(109, 103);
            this.btnRegSmashFry.TabIndex = 6;
            this.btnRegSmashFry.Tag = "RgSmashFry";
            this.btnRegSmashFry.Text = "Reg Smash Fry";
            this.btnRegSmashFry.UseVisualStyleBackColor = false;
            this.btnRegSmashFry.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnBrussels
            // 
            this.btnBrussels.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBrussels.Location = new System.Drawing.Point(303, 320);
            this.btnBrussels.Name = "btnBrussels";
            this.btnBrussels.Size = new System.Drawing.Size(109, 96);
            this.btnBrussels.TabIndex = 6;
            this.btnBrussels.Tag = "Brussels";
            this.btnBrussels.Text = "Brussel Sprouts";
            this.btnBrussels.UseVisualStyleBackColor = false;
            this.btnBrussels.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnSmashTots
            // 
            this.btnSmashTots.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSmashTots.Location = new System.Drawing.Point(418, 211);
            this.btnSmashTots.Name = "btnSmashTots";
            this.btnSmashTots.Size = new System.Drawing.Size(109, 103);
            this.btnSmashTots.TabIndex = 6;
            this.btnSmashTots.Tag = "SmashTots";
            this.btnSmashTots.Text = "Smash Tots";
            this.btnSmashTots.UseVisualStyleBackColor = false;
            this.btnSmashTots.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnRegSweetSmashFry
            // 
            this.btnRegSweetSmashFry.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRegSweetSmashFry.Location = new System.Drawing.Point(533, 211);
            this.btnRegSweetSmashFry.Name = "btnRegSweetSmashFry";
            this.btnRegSweetSmashFry.Size = new System.Drawing.Size(109, 103);
            this.btnRegSweetSmashFry.TabIndex = 6;
            this.btnRegSweetSmashFry.Tag = "RgSmashSweetFry";
            this.btnRegSweetSmashFry.Text = "Reg Sweet Smash Fry";
            this.btnRegSweetSmashFry.UseVisualStyleBackColor = false;
            this.btnRegSweetSmashFry.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // btnSideSalad
            // 
            this.btnSideSalad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSideSalad.Location = new System.Drawing.Point(418, 320);
            this.btnSideSalad.Name = "btnSideSalad";
            this.btnSideSalad.Size = new System.Drawing.Size(109, 96);
            this.btnSideSalad.TabIndex = 6;
            this.btnSideSalad.Tag = "SideSalad";
            this.btnSideSalad.Text = "Side Salad";
            this.btnSideSalad.UseVisualStyleBackColor = false;
            this.btnSideSalad.Click += new System.EventHandler(this.BtnKidsSide_Click);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button18.Location = new System.Drawing.Point(533, 320);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(109, 96);
            this.button18.TabIndex = 6;
            this.button18.Text = "No Side";
            this.button18.UseVisualStyleBackColor = false;
            // 
            // btnKidsSoda
            // 
            this.btnKidsSoda.BackColor = System.Drawing.Color.Turquoise;
            this.btnKidsSoda.Location = new System.Drawing.Point(648, 102);
            this.btnKidsSoda.Name = "btnKidsSoda";
            this.btnKidsSoda.Size = new System.Drawing.Size(99, 58);
            this.btnKidsSoda.TabIndex = 6;
            this.btnKidsSoda.Tag = "KidsSoda";
            this.btnKidsSoda.Text = "Kids Soda";
            this.btnKidsSoda.UseVisualStyleBackColor = false;
            this.btnKidsSoda.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnKidsMilk
            // 
            this.btnKidsMilk.BackColor = System.Drawing.Color.Turquoise;
            this.btnKidsMilk.Location = new System.Drawing.Point(648, 166);
            this.btnKidsMilk.Name = "btnKidsMilk";
            this.btnKidsMilk.Size = new System.Drawing.Size(99, 58);
            this.btnKidsMilk.TabIndex = 6;
            this.btnKidsMilk.Tag = "KidsMilk";
            this.btnKidsMilk.Text = "Kids Milk";
            this.btnKidsMilk.UseVisualStyleBackColor = false;
            this.btnKidsMilk.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnKidsChocMilk
            // 
            this.btnKidsChocMilk.BackColor = System.Drawing.Color.Turquoise;
            this.btnKidsChocMilk.Location = new System.Drawing.Point(648, 230);
            this.btnKidsChocMilk.Name = "btnKidsChocMilk";
            this.btnKidsChocMilk.Size = new System.Drawing.Size(99, 58);
            this.btnKidsChocMilk.TabIndex = 6;
            this.btnKidsChocMilk.Tag = "KidsChocMilk";
            this.btnKidsChocMilk.Text = "Kids Choc Milk";
            this.btnKidsChocMilk.UseVisualStyleBackColor = false;
            this.btnKidsChocMilk.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnKidsApple
            // 
            this.btnKidsApple.BackColor = System.Drawing.Color.Turquoise;
            this.btnKidsApple.Location = new System.Drawing.Point(648, 294);
            this.btnKidsApple.Name = "btnKidsApple";
            this.btnKidsApple.Size = new System.Drawing.Size(99, 58);
            this.btnKidsApple.TabIndex = 6;
            this.btnKidsApple.Tag = "KidsApple";
            this.btnKidsApple.Text = "Kids Apple";
            this.btnKidsApple.UseVisualStyleBackColor = false;
            this.btnKidsApple.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnPButterShake
            // 
            this.btnPButterShake.BackColor = System.Drawing.Color.Moccasin;
            this.btnPButterShake.Location = new System.Drawing.Point(648, 358);
            this.btnPButterShake.Name = "btnPButterShake";
            this.btnPButterShake.Size = new System.Drawing.Size(99, 58);
            this.btnPButterShake.TabIndex = 6;
            this.btnPButterShake.Tag = "PButterShake";
            this.btnPButterShake.Text = "Sub PButter Shake";
            this.btnPButterShake.UseVisualStyleBackColor = false;
            this.btnPButterShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnVanillaShake
            // 
            this.btnVanillaShake.BackColor = System.Drawing.Color.White;
            this.btnVanillaShake.Location = new System.Drawing.Point(753, 102);
            this.btnVanillaShake.Name = "btnVanillaShake";
            this.btnVanillaShake.Size = new System.Drawing.Size(99, 58);
            this.btnVanillaShake.TabIndex = 6;
            this.btnVanillaShake.Tag = "VanillaShake";
            this.btnVanillaShake.Text = "Sub Vanilla Shake";
            this.btnVanillaShake.UseVisualStyleBackColor = false;
            this.btnVanillaShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnChocolateShake
            // 
            this.btnChocolateShake.BackColor = System.Drawing.Color.Chocolate;
            this.btnChocolateShake.Location = new System.Drawing.Point(753, 166);
            this.btnChocolateShake.Name = "btnChocolateShake";
            this.btnChocolateShake.Size = new System.Drawing.Size(99, 58);
            this.btnChocolateShake.TabIndex = 6;
            this.btnChocolateShake.Tag = "ChocolateShake";
            this.btnChocolateShake.Text = "Sub Chocolate Shake";
            this.btnChocolateShake.UseVisualStyleBackColor = false;
            this.btnChocolateShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnStrawberryShake
            // 
            this.btnStrawberryShake.BackColor = System.Drawing.Color.LightCoral;
            this.btnStrawberryShake.Location = new System.Drawing.Point(753, 230);
            this.btnStrawberryShake.Name = "btnStrawberryShake";
            this.btnStrawberryShake.Size = new System.Drawing.Size(99, 58);
            this.btnStrawberryShake.TabIndex = 6;
            this.btnStrawberryShake.Tag = "StrawberryShake";
            this.btnStrawberryShake.Text = "Sub Strawberry Shake";
            this.btnStrawberryShake.UseVisualStyleBackColor = false;
            this.btnStrawberryShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnOreoShake
            // 
            this.btnOreoShake.BackColor = System.Drawing.Color.Black;
            this.btnOreoShake.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOreoShake.Location = new System.Drawing.Point(753, 294);
            this.btnOreoShake.Name = "btnOreoShake";
            this.btnOreoShake.Size = new System.Drawing.Size(99, 58);
            this.btnOreoShake.TabIndex = 6;
            this.btnOreoShake.Tag = "OreoShake";
            this.btnOreoShake.Text = "Sub Oreo Shake";
            this.btnOreoShake.UseVisualStyleBackColor = false;
            this.btnOreoShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnSaltedCaramelShake
            // 
            this.btnSaltedCaramelShake.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSaltedCaramelShake.Location = new System.Drawing.Point(753, 358);
            this.btnSaltedCaramelShake.Name = "btnSaltedCaramelShake";
            this.btnSaltedCaramelShake.Size = new System.Drawing.Size(99, 58);
            this.btnSaltedCaramelShake.TabIndex = 6;
            this.btnSaltedCaramelShake.Tag = "SaltedCaramelShake";
            this.btnSaltedCaramelShake.Text = "Sub Salted Caramel Shake";
            this.btnSaltedCaramelShake.UseVisualStyleBackColor = false;
            this.btnSaltedCaramelShake.Click += new System.EventHandler(this.BtnKidsBeverage_Click);
            // 
            // btnConfirmMods
            // 
            this.btnConfirmMods.BackColor = System.Drawing.SystemColors.Info;
            this.btnConfirmMods.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnConfirmMods.Location = new System.Drawing.Point(303, 422);
            this.btnConfirmMods.Name = "btnConfirmMods";
            this.btnConfirmMods.Size = new System.Drawing.Size(549, 122);
            this.btnConfirmMods.TabIndex = 6;
            this.btnConfirmMods.Tag = "";
            this.btnConfirmMods.Text = "Apply Mods";
            this.btnConfirmMods.UseVisualStyleBackColor = false;
            this.btnConfirmMods.Click += new System.EventHandler(this.BtnConfirmMods_Click);
            // 
            // KidsMealModification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 592);
            this.ControlBox = false;
            this.Controls.Add(this.btnPButterShake);
            this.Controls.Add(this.btnKidsApple);
            this.Controls.Add(this.btnKidsChocMilk);
            this.Controls.Add(this.btnSaltedCaramelShake);
            this.Controls.Add(this.btnOreoShake);
            this.Controls.Add(this.btnStrawberryShake);
            this.Controls.Add(this.btnChocolateShake);
            this.Controls.Add(this.btnKidsMilk);
            this.Controls.Add(this.btnVanillaShake);
            this.Controls.Add(this.btnKidsSoda);
            this.Controls.Add(this.btnSweetFry);
            this.Controls.Add(this.btnTots);
            this.Controls.Add(this.btnBrussels);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.btnSideSalad);
            this.Controls.Add(this.btnRegSweetSmashFry);
            this.Controls.Add(this.btnSmashTots);
            this.Controls.Add(this.btnRegSmashFry);
            this.Controls.Add(this.btnRegFry);
            this.Controls.Add(this.btnConfirmMods);
            this.Controls.Add(this.lbModifiedKidsMeal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KidsMealModification";
            this.Text = "KidsMealModification";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbModifiedKidsMeal;
        private System.Windows.Forms.Button btnRegFry;
        private System.Windows.Forms.Button btnTots;
        private System.Windows.Forms.Button btnSweetFry;
        private System.Windows.Forms.Button btnRegSmashFry;
        private System.Windows.Forms.Button btnBrussels;
        private System.Windows.Forms.Button btnSmashTots;
        private System.Windows.Forms.Button btnRegSweetSmashFry;
        private System.Windows.Forms.Button btnSideSalad;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button btnKidsSoda;
        private System.Windows.Forms.Button btnKidsMilk;
        private System.Windows.Forms.Button btnKidsChocMilk;
        private System.Windows.Forms.Button btnKidsApple;
        private System.Windows.Forms.Button btnPButterShake;
        private System.Windows.Forms.Button btnVanillaShake;
        private System.Windows.Forms.Button btnChocolateShake;
        private System.Windows.Forms.Button btnStrawberryShake;
        private System.Windows.Forms.Button btnOreoShake;
        private System.Windows.Forms.Button btnSaltedCaramelShake;
        private System.Windows.Forms.Button btnConfirmMods;
    }
}