namespace webview
{
    partial class Form
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
            this.split = new System.Windows.Forms.SplitContainer();
            this.panelDebug = new System.Windows.Forms.TableLayoutPanel();
            this.textboxDebug = new System.Windows.Forms.RichTextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonToggle = new System.Windows.Forms.Button();
            this.buttonDebug = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.panelDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.BackColor = System.Drawing.Color.Black;
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Margin = new System.Windows.Forms.Padding(0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.panelDebug);
            this.split.Panel2MinSize = 29;
            this.split.Size = new System.Drawing.Size(756, 449);
            this.split.SplitterDistance = 279;
            this.split.SplitterWidth = 1;
            this.split.TabIndex = 5;
            // 
            // panelDebug
            // 
            this.panelDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.panelDebug.ColumnCount = 3;
            this.panelDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDebug.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelDebug.Controls.Add(this.textboxDebug, 0, 1);
            this.panelDebug.Controls.Add(this.buttonRefresh, 0, 0);
            this.panelDebug.Controls.Add(this.buttonToggle, 2, 0);
            this.panelDebug.Controls.Add(this.buttonDebug, 1, 0);
            this.panelDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDebug.Location = new System.Drawing.Point(0, 0);
            this.panelDebug.Margin = new System.Windows.Forms.Padding(0);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.RowCount = 2;
            this.panelDebug.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelDebug.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelDebug.Size = new System.Drawing.Size(756, 169);
            this.panelDebug.TabIndex = 6;
            // 
            // textboxDebug
            // 
            this.textboxDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(40)))), ((int)(((byte)(34)))));
            this.textboxDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelDebug.SetColumnSpan(this.textboxDebug, 3);
            this.textboxDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxDebug.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxDebug.ForeColor = System.Drawing.Color.White;
            this.textboxDebug.Location = new System.Drawing.Point(0, 29);
            this.textboxDebug.Margin = new System.Windows.Forms.Padding(0);
            this.textboxDebug.Name = "textboxDebug";
            this.textboxDebug.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textboxDebug.Size = new System.Drawing.Size(756, 140);
            this.textboxDebug.TabIndex = 6;
            this.textboxDebug.Text = "";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.buttonRefresh.BackgroundImage = global::webview.Properties.Resources.border;
            this.buttonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRefresh.Location = new System.Drawing.Point(3, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonToggle
            // 
            this.buttonToggle.BackgroundImage = global::webview.Properties.Resources.button;
            this.buttonToggle.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToggle.ForeColor = System.Drawing.Color.White;
            this.buttonToggle.Location = new System.Drawing.Point(165, 3);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(23, 23);
            this.buttonToggle.TabIndex = 8;
            this.buttonToggle.Text = "▼";
            this.buttonToggle.UseVisualStyleBackColor = true;
            this.buttonToggle.Click += new System.EventHandler(this.buttonToggle_Click);
            // 
            // buttonDebug
            // 
            this.buttonDebug.BackgroundImage = global::webview.Properties.Resources.button;
            this.buttonDebug.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDebug.ForeColor = System.Drawing.Color.White;
            this.buttonDebug.Location = new System.Drawing.Point(84, 3);
            this.buttonDebug.Name = "buttonDebug";
            this.buttonDebug.Size = new System.Drawing.Size(75, 23);
            this.buttonDebug.TabIndex = 9;
            this.buttonDebug.Text = "Debug";
            this.buttonDebug.UseVisualStyleBackColor = true;
            this.buttonDebug.Click += new System.EventHandler(this.buttonDebug_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 449);
            this.Controls.Add(this.split);
            this.Name = "Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.panelDebug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.TableLayoutPanel panelDebug;
        private System.Windows.Forms.RichTextBox textboxDebug;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonToggle;
        private System.Windows.Forms.Button buttonDebug;
    }
}

