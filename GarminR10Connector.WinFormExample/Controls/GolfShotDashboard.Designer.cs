namespace GarminR10Connector.WinFormExample.Controls
{
    partial class GolfShotDashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel _pnlCards;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._pnlCards = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // _pnlCards
            // 
            this._pnlCards.AutoScroll = true;
            this._pnlCards.BackColor = System.Drawing.Color.Transparent;
            this._pnlCards.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pnlCards.Location = new System.Drawing.Point(0, 160);
            this._pnlCards.Name = "_pnlCards";
            this._pnlCards.Padding = new System.Windows.Forms.Padding(10);
            this._pnlCards.Size = new System.Drawing.Size(650, 240);
            this._pnlCards.TabIndex = 0;
            // 
            // GolfShotDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(12)))), ((int)(((byte)(18)))));
            this.Controls.Add(this._pnlCards);
            this.Name = "GolfShotDashboard";
            this.Size = new System.Drawing.Size(650, 400);
            this.ResumeLayout(false);
        }

        #endregion
    }
}