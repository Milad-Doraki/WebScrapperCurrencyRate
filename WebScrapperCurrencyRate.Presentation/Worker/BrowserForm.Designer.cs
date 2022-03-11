
using System;

namespace WebScrapperCurrencyRate.Presentation.Worker
{
    partial class BrowserForm
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
            this.webView2Control = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView2Control)).BeginInit();
            this.SuspendLayout();
            // 
            // webView2Control
            // 
            this.webView2Control.CreationProperties = null;
            this.webView2Control.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2Control.Location = new System.Drawing.Point(30, 64);
            this.webView2Control.Margin = new System.Windows.Forms.Padding(2);
            this.webView2Control.Name = "webView2Control";
            this.webView2Control.Size = new System.Drawing.Size(237, 135);
            this.webView2Control.Source = new System.Uri("https://www.bing.com/", System.UriKind.Absolute);
            this.webView2Control.TabIndex = 7;
            this.webView2Control.Visible = false;
            this.webView2Control.ZoomFactor = 1D;
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(304, 210);
            this.Controls.Add(this.webView2Control);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BrowserForm";
            this.Text = "BrowserForm";
            this.Load += new System.EventHandler(this.BrowserForm_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.webView2Control)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Drawing.Bitmap webViewLogoBitmap;
        public Microsoft.Web.WebView2.WinForms.WebView2 webView2Control;
    }
}