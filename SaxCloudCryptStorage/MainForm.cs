/*
 * Created by SharpDevelop.
 * User: bohlrich
 * Date: 28.01.2014
 * Time: 10:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SQLite;

namespace SaxCloudCryptStorage
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db");
			connection.Open();
			
			SQLiteCommand command = new SQLiteCommand(connection);
			command.CommandText = "create table if not exists User (ID integer not null primary key autoincrement, UserName varchar(40) not null, EMail varchar(100) not null, bKey int not null)";
			command.ExecuteNonQuery();
			
			command.CommandText = "select * from User";
			SQLiteDataReader rUser = command.ExecuteReader();
			
			if(rUser.HasRows)
			{
				rUser.Read();
				tbName.Text = rUser.GetString(rUser.GetOrdinal("UserName"));
				tbEmail.Text = rUser.GetString(rUser.GetOrdinal("EMail"));
				tbName.ReadOnly = true;
				tbEmail.ReadOnly = true;
				button3.Enabled = false;
			}
			else
			{
				tbName.Text = Environment.UserName;	
			}
			
			rUser.Close();
			
			command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='CryptFolder'";
			SQLiteDataReader rCCFolder = command.ExecuteReader();
			
			if(rCCFolder.HasRows)
			{
				rCCFolder.Close();
				
				command.CommandText = "select * from CryptFolder";
				SQLiteDataReader rFCFolder  = command.ExecuteReader();
				
				if(rFCFolder.HasRows)
				{
					rFCFolder.Read();
					tbSyncFolder.Text = rFCFolder.GetString(rFCFolder.GetOrdinal("CryptFolderName"));					
				}
				rFCFolder.Close();
			}
			else
			{
				rCCFolder.Close();
				command.CommandText = "create table if not exists CryptFolder (ID integer not null primary key autoincrement, idUser integer not null, CryptFolderName varchar(300) not null)";
				command.ExecuteNonQuery();
			}
			
			command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='SourceFolder'";
			SQLiteDataReader rCSFolder = command.ExecuteReader();
			
			if(rCSFolder.HasRows)
			{
				rCSFolder.Close();
				
				command.CommandText = "select * from SourceFolder";
				SQLiteDataReader rFSFolder = command.ExecuteReader();
				
				while(rFSFolder.Read())
				{					
					listBox1.Items.Add(rFSFolder.GetString(rFSFolder.GetOrdinal("SourceFolderName")));
				}
			}
			else
			{
				rCSFolder.Close();

				command.CommandText = "create table if not exists SourceFolder (ID integer not null primary key autoincrement, idUser integer not null, SourceFolderName varchar(500) not null)";
				command.ExecuteNonQuery();
			}						
			
			connection.Close();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
			if(folderBrowserDialog1.SelectedPath.Length > 0)
			{
				listBox1.Items.Add(folderBrowserDialog1.SelectedPath);	
				btnSpeichern.Enabled = true;
			}			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			listBox1.Items.Remove(listBox1.SelectedItem);
			btnSpeichern.Enabled = true;
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			if(tbName.Text.Length > 4)
			{
			
				if(tbEmail.Text.Length > 0)
				{
					Process p = new Process();
					p.StartInfo.FileName = "gpg.exe";
					p.StartInfo.Arguments = "--gen-key";
					p.StartInfo.UseShellExecute = false;
					p.StartInfo.RedirectStandardOutput = true;
					p.Start();
					
					p.BeginOutputReadLine();
					
					SendKeys.SendWait("1{ENTER}");
					SendKeys.SendWait("{ENTER}");
					SendKeys.SendWait("{ENTER}");
					SendKeys.SendWait("j{ENTER}");
					SendKeys.Send(tbName.Text);
					SendKeys.SendWait("{ENTER}");
					SendKeys.Send(tbEmail.Text);
					SendKeys.SendWait("{ENTER}");
					SendKeys.SendWait("{ENTER}");
					SendKeys.SendWait("F{ENTER}");
					
					p.WaitForExit();
					int pExitCode = p.ExitCode;
					p.Close();
					
					if(pExitCode == 0)
					{
						SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db");
						connection.Open();
						
						SQLiteCommand command = new SQLiteCommand(connection);
						command.CommandText = String.Format("insert into User (UserName,EMail,bKey) values ('{0}','{1}',1)",tbName.Text,tbEmail.Text);
						command.ExecuteNonQuery();
						
						connection.Close();
						
						button3.Enabled = false;
					}
				}
				else
				{
					MessageBox.Show("E-Mail Adresse muss ausgefüllt sein!");
				}
				
			}
			else
			{
				MessageBox.Show("Ihr Name muss mindestens 5 Zeichen lang sein!");
			}
			
		}
		
		void BeendenToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			if(tbName.Text.Length > 4)
			{
				btnEnc.Enabled = false;
				for(int i = 0;i < listBox1.Items.Count;i++)
				{
					EncryptFolderOnce(listBox1.GetItemText(listBox1.Items[i]), listBox1.GetItemText(listBox1.Items[i]).Length);
					EncryptFolder(listBox1.GetItemText(listBox1.Items[i]), listBox1.GetItemText(listBox1.Items[i]).Length);
				}
				btnEnc.Enabled = true;
				label4.Text = "Erledigt!";
			}
			else
			{
				MessageBox.Show("Bitte einen Namen eingeben!");
			}
			
		}
		
		void EncryptFolder(string sFolder, int iFolderLength)
		{	
			label4.Text = sFolder;		
			label4.Refresh();
			try
			{
				foreach(string d in Directory.GetDirectories(sFolder))
				{
					label4.Text = d;
					label4.Refresh();
					
					try
					{
						foreach(string f in Directory.GetFiles(d))
						{
							label4.Text = f;
							label4.Refresh();
							tpFolder.Refresh();
							
							string sRootPath = Path.GetPathRoot(f);
							sRootPath = sRootPath.Replace(":","_");
							
							string sArgument = "--yes -r " + tbName.Text + " -o \"" + tbSyncFolder.Text + "\\" + sRootPath + f.Substring(3) + ".gpg\" -e " + "\"" + f + "\"";
							
							string sDirectory = tbSyncFolder.Text + "\\" + sRootPath + f.Substring(3);
							sDirectory = Path.GetDirectoryName(sDirectory);
							System.IO.Directory.CreateDirectory(sDirectory);
							
							Process gpgEnc = new Process();
							gpgEnc.StartInfo.FileName = "gpg.exe";
							gpgEnc.StartInfo.Arguments = sArgument;
							gpgEnc.StartInfo.UseShellExecute = false;
							gpgEnc.StartInfo.RedirectStandardOutput = true;
							gpgEnc.StartInfo.CreateNoWindow = true;
							gpgEnc.Start();
							gpgEnc.StandardOutput.ReadToEnd();
							gpgEnc.WaitForExit();						
							gpgEnc.Close();
						}
					}
					catch(System.Exception excpt)
					{
						MessageBox.Show(excpt.Message);
					}
					
					EncryptFolder(d,iFolderLength);
				}
			}
			catch(System.Exception excpt)
			{
				MessageBox.Show(excpt.Message);			
			}
		}
		
		void EncryptFolderOnce(string sFolder, int iFolderLength)
		{
			label4.Text = sFolder;
			label4.Refresh();
			
			try
			{
				foreach(string f in Directory.GetFiles(sFolder))
				{
					label4.Text = f;
					label4.Refresh();
					tpFolder.Refresh();
					
					string sRootPath = Path.GetPathRoot(f);
					sRootPath = sRootPath.Replace(":","_");					
					
					string sArgument = "--yes -r " + tbName.Text + " -o \"" + tbSyncFolder.Text + "\\" + sRootPath + f.Substring(3) + ".gpg\" -e " + "\"" + f + "\"";
					
					string sDirectory = tbSyncFolder.Text + "\\" + sRootPath + f.Substring(3);
					sDirectory = Path.GetDirectoryName(sDirectory);
					System.IO.Directory.CreateDirectory(sDirectory);
					
					Process gpgEnc = new Process();
					gpgEnc.StartInfo.FileName = "gpg.exe";
					gpgEnc.StartInfo.Arguments = sArgument;
					gpgEnc.StartInfo.UseShellExecute = false;
					gpgEnc.StartInfo.RedirectStandardOutput = true;
					gpgEnc.StartInfo.CreateNoWindow = true;
					gpgEnc.Start();
					gpgEnc.StandardOutput.ReadToEnd();
					gpgEnc.WaitForExit();						
					gpgEnc.Close();
				}
			}
			catch(System.Exception excpt)
			{
				MessageBox.Show(excpt.Message);
			}
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
			if(folderBrowserDialog1.SelectedPath.Length > 0)
			{
				tbSyncFolder.Text = folderBrowserDialog1.SelectedPath;
				btnSpeichern.Enabled = true;
			}
		}
		
		void BtnSpeichernClick(object sender, EventArgs e)
		{
			int iUser;
			
			SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db");
			connection.Open();
			
			SQLiteCommand command = new SQLiteCommand(connection);
			
			command.CommandText = "select * from User where UserName = '" + tbName.Text + "'";
			SQLiteDataReader reader = command.ExecuteReader();
			
			if(reader.HasRows)
			{
				reader.Read();
				iUser = reader.GetInt32(reader.GetOrdinal("ID"));
				
				reader.Close();
				
				command.CommandText = "select * from CryptFolder where idUser = '" + iUser + "'";
				reader = command.ExecuteReader();
				
				if(reader.HasRows)
				{
					reader.Read();
					if(reader.GetString(reader.GetOrdinal("CryptFolderName")) != tbSyncFolder.Text)
					{
						reader.Close();
						command.CommandText = "insert into CryptFolder (idUser, CryptFolderName) values (" + iUser + ", '" + tbSyncFolder.Text + "')";
						command.ExecuteNonQuery();
					}
				}
				else
				{
					reader.Close();
					command.CommandText = "insert into CryptFolder (idUser, CryptFolderName) values (" + iUser + ", '" + tbSyncFolder.Text + "')";
					command.ExecuteNonQuery();
				}
				
				reader.Close();
				
				for(int i = 0;i < listBox1.Items.Count;i++)
				{
					MessageBox.Show(listBox1.GetItemText(listBox1.Items[i]));
					command.CommandText = "insert into SourceFolder (idUser, SourceFolderName) values (" + iUser + ", '" + listBox1.GetItemText(listBox1.Items[i]) + "')";
					command.ExecuteNonQuery();
				}
			}
			else
			{
				MessageBox.Show("Kann den angebenen User nicht in der Datenbank finden!");
			}					
			
			connection.Close();
			btnSpeichern.Enabled = false;
		}
		
		void BtnSyncClick(object sender, EventArgs e)
		{
			// rsync.exe -ru --delete --progress /cygdrive/f/SYNC -e ssh "root@192.168.178.77:/root/sync/"			
		}
		
		void BtnSSHClick(object sender, EventArgs e)
		{
			int iUser;
			
			SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db");
			connection.Open();			
			
			SQLiteCommand command = new SQLiteCommand(connection);
			
			command.CommandText = "select * from User where UserName = '" + tbUserName.Text + "'";
			SQLiteDataReader reader = command.ExecuteReader();
			
			iUser = reader.GetInt32(reader.GetOrdinal("ID"));			
			reader.Close();
			
			command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='RemoteFolder'";
			SQLiteDataReader reader = command.ExecuteReader();
			
			
			if(reader.HasRows)
			{
				reader.Close();
			}
			else
			{
				reader.Close();
				
				command.CommandText = "create table if not exists RemoteFolder (ID integer not null primary key autoincrement, idUser integer not null, RemoteUserName varchar(40) not null, RemoteFolderName varchar(300) not null)";
				command.ExecuteNonQuery();
				
				command.CommandText = "insert into RemoteFolder (idUser, RemoteUserName, RemoteFolder) values (" + iUser + " , " + tbUserName.Text + ", /home/" + tbUserName.Text + "/sync)";
				
			}
		}
		
		void SSHCreate(string sRemoteUser)
		{
			string sRemotePW;
			
			MessageBox.Show("Bitte geben Sie ihr SaxCloud Account Passwort an");
			
			
		}
	}
}
