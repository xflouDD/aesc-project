namespace MyProject
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtBullet = new System.Windows.Forms.Label();
            this.txtKill = new System.Windows.Forms.Label();
            this.txtHealth = new System.Windows.Forms.Label();
            this.HealthBar = new System.Windows.Forms.ProgressBar();
            this.Player = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBullet
            // 
            this.txtBullet.AutoSize = true;
            this.txtBullet.Font = new System.Drawing.Font("MingLiU_HKSCS-ExtB", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBullet.Location = new System.Drawing.Point(12, 12);
            this.txtBullet.Name = "txtBullet";
            this.txtBullet.Size = new System.Drawing.Size(153, 23);
            this.txtBullet.TabIndex = 0;
            this.txtBullet.Text = "Bullets: 10";
            // 
            // txtKill
            // 
            this.txtKill.AutoSize = true;
            this.txtKill.Font = new System.Drawing.Font("MingLiU_HKSCS-ExtB", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKill.Location = new System.Drawing.Point(276, 12);
            this.txtKill.Name = "txtKill";
            this.txtKill.Size = new System.Drawing.Size(114, 23);
            this.txtKill.TabIndex = 1;
            this.txtKill.Text = "Kills: 0";
            // 
            // txtHealth
            // 
            this.txtHealth.AutoSize = true;
            this.txtHealth.Font = new System.Drawing.Font("MingLiU_HKSCS-ExtB", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHealth.Location = new System.Drawing.Point(507, 12);
            this.txtHealth.Name = "txtHealth";
            this.txtHealth.Size = new System.Drawing.Size(101, 23);
            this.txtHealth.TabIndex = 2;
            this.txtHealth.Text = "Health:";
            this.txtHealth.Click += new System.EventHandler(this.txtHealth_Click);
            // 
            // HealthBar
            // 
            this.HealthBar.Location = new System.Drawing.Point(627, 6);
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Size = new System.Drawing.Size(266, 28);
            this.HealthBar.TabIndex = 3;
            this.HealthBar.Value = 100;
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.Transparent;
            this.Player.Image = global::MyProject.Properties.Resources.up;
            this.Player.Location = new System.Drawing.Point(410, 491);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(71, 100);
            this.Player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Player.TabIndex = 4;
            this.Player.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.MainEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(922, 653);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.HealthBar);
            this.Controls.Add(this.txtHealth);
            this.Controls.Add(this.txtKill);
            this.Controls.Add(this.txtBullet);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Form1";
            this.Text = "Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtBullet;
        private System.Windows.Forms.Label txtKill;
        private System.Windows.Forms.Label txtHealth;
        private System.Windows.Forms.ProgressBar HealthBar;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Timer gameTimer;
    }
}

