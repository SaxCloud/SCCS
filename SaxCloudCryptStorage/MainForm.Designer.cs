/*
 * Created by SharpDevelop.
 * User: bohlrich
 * Date: 28.01.2014
 * Time: 10:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SaxCloudCryptStorage
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.btnFolder = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tbName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.tbEmail = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnEnc = new System.Windows.Forms.Button();
			this.tbSyncFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSpeichern = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpKey = new System.Windows.Forms.TabPage();
			this.tpFolder = new System.Windows.Forms.TabPage();
			this.tpCloud = new System.Windows.Forms.TabPage();
			this.tbPassword = new System.Windows.Forms.MaskedTextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.btnSSH = new System.Windows.Forms.Button();
			this.lblUsername = new System.Windows.Forms.Label();
			this.tbUserName = new System.Windows.Forms.TextBox();
			this.btnSync = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpKey.SuspendLayout();
			this.tpFolder.SuspendLayout();
			this.tpCloud.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.dateiToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(298, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// dateiToolStripMenuItem
			// 
			this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.beendenToolStripMenuItem});
			this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
			this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.dateiToolStripMenuItem.Text = "Datei";
			// 
			// beendenToolStripMenuItem
			// 
			this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
			this.beendenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.beendenToolStripMenuItem.Text = "Beenden";
			this.beendenToolStripMenuItem.Click += new System.EventHandler(this.BeendenToolStripMenuItemClick);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(6, 72);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(244, 95);
			this.listBox1.TabIndex = 1;
			// 
			// btnFolder
			// 
			this.btnFolder.Location = new System.Drawing.Point(6, 43);
			this.btnFolder.Name = "btnFolder";
			this.btnFolder.Size = new System.Drawing.Size(97, 23);
			this.btnFolder.TabIndex = 2;
			this.btnFolder.Text = "Ordner wählen";
			this.btnFolder.UseVisualStyleBackColor = true;
			this.btnFolder.Click += new System.EventHandler(this.Button1Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(175, 43);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "löschen";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.Button2Click);
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(115, 38);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(142, 20);
			this.tbName.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 20);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(3, 6);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(112, 23);
			this.button3.TabIndex = 7;
			this.button3.Text = "Schlüssel erzeugen";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// tbEmail
			// 
			this.tbEmail.Location = new System.Drawing.Point(115, 75);
			this.tbEmail.Name = "tbEmail";
			this.tbEmail.Size = new System.Drawing.Size(142, 20);
			this.tbEmail.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "E-Mail Adresse:";
			// 
			// btnEnc
			// 
			this.btnEnc.Location = new System.Drawing.Point(6, 6);
			this.btnEnc.Name = "btnEnc";
			this.btnEnc.Size = new System.Drawing.Size(97, 23);
			this.btnEnc.TabIndex = 10;
			this.btnEnc.Text = "Verschlüsseln";
			this.btnEnc.UseVisualStyleBackColor = true;
			this.btnEnc.Click += new System.EventHandler(this.Button4Click);
			// 
			// tbSyncFolder
			// 
			this.tbSyncFolder.Location = new System.Drawing.Point(89, 170);
			this.tbSyncFolder.Name = "tbSyncFolder";
			this.tbSyncFolder.Size = new System.Drawing.Size(131, 20);
			this.tbSyncFolder.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 173);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 23);
			this.label3.TabIndex = 12;
			this.label3.Text = "Sync Ordner:";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(226, 169);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(24, 20);
			this.button5.TabIndex = 13;
			this.button5.Text = "...";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(14, 270);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(271, 56);
			this.label4.TabIndex = 15;
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// btnSpeichern
			// 
			this.btnSpeichern.Enabled = false;
			this.btnSpeichern.Location = new System.Drawing.Point(175, 6);
			this.btnSpeichern.Name = "btnSpeichern";
			this.btnSpeichern.Size = new System.Drawing.Size(75, 23);
			this.btnSpeichern.TabIndex = 16;
			this.btnSpeichern.Text = "Speichern";
			this.btnSpeichern.UseVisualStyleBackColor = true;
			this.btnSpeichern.Click += new System.EventHandler(this.BtnSpeichernClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpKey);
			this.tabControl1.Controls.Add(this.tpFolder);
			this.tabControl1.Controls.Add(this.tpCloud);
			this.tabControl1.Location = new System.Drawing.Point(14, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(271, 226);
			this.tabControl1.TabIndex = 17;
			// 
			// tpKey
			// 
			this.tpKey.Controls.Add(this.button3);
			this.tpKey.Controls.Add(this.tbEmail);
			this.tpKey.Controls.Add(this.tbName);
			this.tpKey.Controls.Add(this.label1);
			this.tpKey.Controls.Add(this.label2);
			this.tpKey.Location = new System.Drawing.Point(4, 22);
			this.tpKey.Name = "tpKey";
			this.tpKey.Padding = new System.Windows.Forms.Padding(3);
			this.tpKey.Size = new System.Drawing.Size(263, 200);
			this.tpKey.TabIndex = 0;
			this.tpKey.Text = "Schlüssel";
			this.tpKey.UseVisualStyleBackColor = true;
			// 
			// tpFolder
			// 
			this.tpFolder.Controls.Add(this.btnEnc);
			this.tpFolder.Controls.Add(this.btnSpeichern);
			this.tpFolder.Controls.Add(this.listBox1);
			this.tpFolder.Controls.Add(this.btnFolder);
			this.tpFolder.Controls.Add(this.button5);
			this.tpFolder.Controls.Add(this.btnDelete);
			this.tpFolder.Controls.Add(this.label3);
			this.tpFolder.Controls.Add(this.tbSyncFolder);
			this.tpFolder.Location = new System.Drawing.Point(4, 22);
			this.tpFolder.Name = "tpFolder";
			this.tpFolder.Padding = new System.Windows.Forms.Padding(3);
			this.tpFolder.Size = new System.Drawing.Size(263, 200);
			this.tpFolder.TabIndex = 1;
			this.tpFolder.Text = "Ordner";
			this.tpFolder.UseVisualStyleBackColor = true;
			// 
			// tpCloud
			// 
			this.tpCloud.Controls.Add(this.tbPassword);
			this.tpCloud.Controls.Add(this.lblPassword);
			this.tpCloud.Controls.Add(this.btnSSH);
			this.tpCloud.Controls.Add(this.lblUsername);
			this.tpCloud.Controls.Add(this.tbUserName);
			this.tpCloud.Controls.Add(this.btnSync);
			this.tpCloud.Location = new System.Drawing.Point(4, 22);
			this.tpCloud.Name = "tpCloud";
			this.tpCloud.Padding = new System.Windows.Forms.Padding(3);
			this.tpCloud.Size = new System.Drawing.Size(263, 200);
			this.tpCloud.TabIndex = 2;
			this.tpCloud.Text = "Cloud";
			this.tpCloud.UseVisualStyleBackColor = true;
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(157, 49);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.Size = new System.Drawing.Size(100, 20);
			this.tbPassword.TabIndex = 6;
			this.tbPassword.Visible = false;
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(7, 47);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(100, 23);
			this.lblPassword.TabIndex = 5;
			this.lblPassword.Text = "Passwort:";
			this.lblPassword.Visible = false;
			// 
			// btnSSH
			// 
			this.btnSSH.Location = new System.Drawing.Point(7, 144);
			this.btnSSH.Name = "btnSSH";
			this.btnSSH.Size = new System.Drawing.Size(100, 50);
			this.btnSSH.TabIndex = 4;
			this.btnSSH.Text = "Verbindung herstellen";
			this.btnSSH.UseVisualStyleBackColor = true;
			this.btnSSH.Click += new System.EventHandler(this.BtnSSHClick);
			// 
			// lblUsername
			// 
			this.lblUsername.Location = new System.Drawing.Point(7, 3);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(100, 23);
			this.lblUsername.TabIndex = 3;
			this.lblUsername.Text = "Benutzername:";
			this.lblUsername.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// tbUserName
			// 
			this.tbUserName.Location = new System.Drawing.Point(157, 7);
			this.tbUserName.Name = "tbUserName";
			this.tbUserName.Size = new System.Drawing.Size(100, 20);
			this.tbUserName.TabIndex = 1;
			// 
			// btnSync
			// 
			this.btnSync.Location = new System.Drawing.Point(157, 144);
			this.btnSync.Name = "btnSync";
			this.btnSync.Size = new System.Drawing.Size(100, 50);
			this.btnSync.TabIndex = 0;
			this.btnSync.Text = "Synchronisieren";
			this.btnSync.UseVisualStyleBackColor = true;
			this.btnSync.Click += new System.EventHandler(this.BtnSyncClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 335);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "SaxCloudCryptStorage";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tpKey.ResumeLayout(false);
			this.tpKey.PerformLayout();
			this.tpFolder.ResumeLayout(false);
			this.tpFolder.PerformLayout();
			this.tpCloud.ResumeLayout(false);
			this.tpCloud.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.MaskedTextBox tbPassword;
		private System.Windows.Forms.Button btnSSH;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Button btnSync;
		private System.Windows.Forms.TextBox tbUserName;
		private System.Windows.Forms.TabPage tpCloud;
		private System.Windows.Forms.TabPage tpFolder;
		private System.Windows.Forms.TabPage tpKey;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button btnSpeichern;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbSyncFolder;
		private System.Windows.Forms.Button btnEnc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbEmail;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnFolder;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		
	}
}
