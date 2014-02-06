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
			
			SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db"); // Verbindung zur Datenbank herstellen
			connection.Open();
			
			SQLiteCommand command = new SQLiteCommand(connection);
			// Falls die Tabelle "User" noch nicht existiert wird sie hier angelegt
			command.CommandText = "create table if not exists User (ID integer not null primary key autoincrement, UserName varchar(40) not null, EMail varchar(100) not null, bKey int not null)";
			command.ExecuteNonQuery();
			
			// Hole alle Daten aus User
			command.CommandText = "select * from User";
			SQLiteDataReader rUser = command.ExecuteReader();
						
			if(rUser.HasRows) // Wenn etwas in User drin steht
			{
				rUser.Read();
				tbName.Text = rUser.GetString(rUser.GetOrdinal("UserName")); // Hole den Usernamen und schreibe ihn in die Textbox für den Namen
				tbEmail.Text = rUser.GetString(rUser.GetOrdinal("EMail")); // Hole die Emailadresse und schreibe sie in die Textbox für die Email Adresse
				tbName.ReadOnly = true; // Setze die Textbox für den Namen auf ReadOnly, damit dieser nicht mehr geändert werden kann
				tbEmail.ReadOnly = true; // Setze die Textbox für die Emailadresse auf ReadOnly, damit diese nicht mehr geändert werden kann
				button3.Enabled = false; // Der Button "Schlüssel erzeugen" wird deaktiviert, damit kein neuer Schlüssel erzeugt werden kann
			}
			else
			{
				tbName.Text = Environment.UserName;	// Falls noch keine Daten vorhanden sind, schreibe den aktuellen Windows Benutzernamen in die TextBox für den Namen
			}
			
			rUser.Close();
			
			command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='CryptFolder'"; // Check ob die Tabelle CryptFolder schon existiert
			SQLiteDataReader rCCFolder = command.ExecuteReader();
			
			if(rCCFolder.HasRows) // Tabelle CryptFolder existiert
			{
				rCCFolder.Close();
				
				command.CommandText = "select * from CryptFolder"; // Hole alle Daten aus CryptFolder
				SQLiteDataReader rFCFolder  = command.ExecuteReader();
				
				if(rFCFolder.HasRows) // Falls Daten in CryptFolder vorhanden sind
				{
					rFCFolder.Read();
					tbSyncFolder.Text = rFCFolder.GetString(rFCFolder.GetOrdinal("CryptFolderName")); // Schreibe den in der DB eingetragenen Ordnernamen für den Sync Ordner in die Textbox SyncFolder					
				}
				rFCFolder.Close();
			}
			else // Die Tabelle CryptFolder existiert noch nicht
			{
				rCCFolder.Close();
				// Tabelle CryptFolder erstellen
				command.CommandText = "create table if not exists CryptFolder (ID integer not null primary key autoincrement, idUser integer not null, CryptFolderName varchar(300) not null)";
				command.ExecuteNonQuery();
			}
			
			command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='SourceFolder'"; // Check ob die Tabelle SourceFolder schon existiert
			SQLiteDataReader rCSFolder = command.ExecuteReader();
			
			if(rCSFolder.HasRows) // Tabelle SourceFolder existiert
			{
				rCSFolder.Close();
				
				command.CommandText = "select * from SourceFolder"; // Hole alle Daten aus SourceFolder
				SQLiteDataReader rFSFolder = command.ExecuteReader();
				
				while(rFSFolder.Read()) // Schleife um alle Ergebnissätze herauszuschreiben
				{					
					listBox1.Items.Add(rFSFolder.GetString(rFSFolder.GetOrdinal("SourceFolderName"))); // Füge alle in der DB gespeicherten SourceFolder der ListBox für die SourceFolder hinzu
				}
			}
			else // Tabelle SourceFolder existiert noch nicht
			{
				rCSFolder.Close();
				// Tabelle SourceFolder erstellen
				command.CommandText = "create table if not exists SourceFolder (ID integer not null primary key autoincrement, idUser integer not null, SourceFolderName varchar(500) not null)";
				command.ExecuteNonQuery();
			}						
			
			connection.Close();
		}
		
		void Button1Click(object sender, EventArgs e) // Der Button zum SourceFolder wählen wird geklickt
		{
			folderBrowserDialog1.ShowDialog(); // Dateiauswahldialog zeigen
			if(folderBrowserDialog1.SelectedPath.Length > 0) // Wenn ein Ordner gewählt wurde
			{
				listBox1.Items.Add(folderBrowserDialog1.SelectedPath);	// Ordner der Liste in ListBox1 hinzufügen
				btnSpeichern.Enabled = true; // den Speichern Button wieder aktivieren, damit die neue Konfiguration in der DB gespeichert werden kann
			}			
		}
		
		void Button2Click(object sender, EventArgs e) // Wenn der Button Löschen geklickt wird
		{
			listBox1.Items.Remove(listBox1.SelectedItem); // den markierten Ordner in der ListBox löschen
			btnSpeichern.Enabled = true; // den Speichern Button wieder aktivieren, damit die neue Konfiguration in der DB gespeichert werden kann
		}
		
		void Button3Click(object sender, EventArgs e) // der Button Verschlüsseln wird geklickt
		{
			if(tbName.Text.Length > 4) // Wenn der in der Texbox für den Namen eingetragene Name mindestens 5 Zeichen lang ist (Bedingung von GPG für Schlüsselerzeugung)
			{
			
				if(tbEmail.Text.Length > 5) // Check ob die Emailadresse in zugehörigen Textbox mindestens 6 Zeichen lang ist (kürzer geht keine Emailadresse)
				{
					Process p = new Process(); // neuen Prozess erstellen
					p.StartInfo.FileName = "gpg.exe"; // gpg wird verwendet (ist in der Path Variable im System eingetragen)
					p.StartInfo.Arguments = "--gen-key"; // mit dem Parameter gen-key, um einen neuen Schlüssel zu erzeugen
					p.StartInfo.UseShellExecute = false;
					p.StartInfo.RedirectStandardOutput = true;
					p.Start(); // Prozess starten
					
					p.BeginOutputReadLine(); // Prozess anzeigen
					
					SendKeys.SendWait("1{ENTER}"); // Wir senden eine "1" um die Schlüsselart auf RSA/RSA zu stellen
					SendKeys.SendWait("{ENTER}"); // Wir senden ein einfaches ENTER um die Schlüssellänge von 2048 bits zu bestätigen
					SendKeys.SendWait("{ENTER}"); // Wir senden wieder ein einfaches ENTER um eine unendliche Schlüssel Gültigkeit zu bestätigen					
					SendKeys.SendWait("j{ENTER}"); // Wir senden ein "j" um die Eingaben ob ihrer Richtigkeit zu bestätigen
					SendKeys.Send(tbName.Text); // Wir senden den in der Textbox eingetragenen Usernamen für den gefragten Namen 
					SendKeys.SendWait("{ENTER}"); // Danach müssen wir den mit einem ENTER absenden
					SendKeys.Send(tbEmail.Text); // Wir senden die in der Textbox eingetragene Emailadresse
					SendKeys.SendWait("{ENTER}"); // die wir auch mit einem ENTER absenden müssen
					SendKeys.SendWait("{ENTER}"); // Nochmal ENTER um einen leeren Kommentar zu erstellen
					SendKeys.SendWait("F{ENTER}"); // Wir senden ein "F" um den Vorgang mit "Fertig" zu beenden
					
					p.WaitForExit(); // Warten bis der Prozess beendet ist
					int pExitCode = p.ExitCode; // den vom Prozess erzeugten Exitcode in die Variable pExitCode eintragen
					p.Close(); // Prozesshandle schließen
					
					if(pExitCode == 0) // Wenn der Prozess ohne Fehler durchgelaufen ist
					{
						SQLiteConnection connection = new SQLiteConnection("Data Source=sccs.db"); // Verbindung zu unserer Haupt DB herstellen
						connection.Open();
						
						SQLiteCommand command = new SQLiteCommand(connection);
						// den im GPG Schlüssel verwendeten Namen und Emailadresse in die Datenbank eintragen, zusätzlich den bKey Wert auf 1 setzen, da wir den Schlüssel erfolgreich erzeugt haben
						command.CommandText = String.Format("insert into User (UserName,EMail,bKey) values ('{0}','{1}',1)",tbName.Text,tbEmail.Text);
						command.ExecuteNonQuery();
						
						connection.Close();
						
						button3.Enabled = false; // Den Button "Schlüssel erzeugen" deaktivieren, damit kein weiterer Schlüssel erzeugt werden kann
					}
				}
				else // Die Emailadresse ist nicht lang genug
				{
					MessageBox.Show("E-Mail Adresse muss ausgefüllt sein!");
				}
				
			}
			else // der Name ist nicht lang genug
			{
				MessageBox.Show("Ihr Name muss mindestens 5 Zeichen lang sein!");
			}
			
		}
		
		void BeendenToolStripMenuItemClick(object sender, EventArgs e) // Wenn Beenden im Menü Datei geklickt wird
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
