/*
 * Created by SharpDevelop.
 * User: Salman-pc
 * Date: 05/09/2020
 * Time: 8:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyNotes
{
	/// <summary>
	/// MyNotes is a ligthweigth text writer/notepad appliaction
	/// i made using c# windows form
	/// 	
	/// </summary>
	public partial class MainForm : Form
	{
		private bool mouseDown;
		private Point lastLocation;
		public MainForm()
		{
			InitializeComponent();
			cutToolStripMenuItem.Enabled = false;
		}
		
		//control in file menustrip
		
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(label1.Text == "Untitled")
			{
				saveFile();
			}else{
				richTextBox1.SaveFile(label1.Text,RichTextBoxStreamType.PlainText);
			}
		}
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(richTextBox1.Text == string.Empty)
			{
				OpenFile();
			}else{
				DialogResult dr =  MessageBox.Show("Do you want to save this file first?","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
				if(dr == DialogResult.Yes)
				{
					saveFile();
					OpenFile();
				}if(dr == DialogResult.No){
					OpenFile();
				}else
					richTextBox1.Focus();
			}
		}
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			NewFile();
		}
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			saveFile();
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.SelectedText = "";
		}
		
		void SelectAllToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.SelectAll();
		}
		//end file menustrip
		
		
		//edit menustrip
		void UndoToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (richTextBox1.CanUndo){
				richTextBox1.Undo();
			}
			redoToolStripMenuItem.Enabled = true;//when undo button is click,it will set redo button to enable to true/on
		}
		
		void RedoToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(richTextBox1.CanRedo){
				richTextBox1.Redo();
			}
		}
		
		void CutToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.Cut();
			richTextBox1.Copy();
		}
		
		void PasteToolStripMenuItemClick(object sender, EventArgs e)
		{	
			richTextBox1.Paste();
		}
		
		void CopyToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.Copy();
		}
		
		void DateToolStripMenuItemClick(object sender, EventArgs e)
		{
			string Date =  DateTime.Now.ToString("yyyy MMMMM dd");
			richTextBox1.Text =  richTextBox1.Text  + Date;
		}
		
		void FontToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(fontDialog1.ShowDialog() == DialogResult.OK)
			{
				richTextBox1.Font = fontDialog1.Font;
			}else{
				richTextBox1.Focus();
			}
		}
		
		//others
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
			undoToolStripMenuItem.Enabled = true;
		}
		
		//this event handler will enabled all button inside when you select/blok the text in rich textbox
		void RichTextBox1SelectionChanged(object sender, EventArgs e)
		{
			copyToolStripMenuItem.Enabled  =  (richTextBox1.SelectedText.Length > 0)?true:false;
			deleteToolStripMenuItem.Enabled =  (richTextBox1.SelectedText.Length > 0)?true:false;
			cutToolStripMenuItem.Enabled = (richTextBox1.SelectedText.Length > 0)?true:false;
		}
		//end of others
		
		//save file method
		private void saveFile()
		{
			saveFileDialog1.Title = "Save";
			saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			if(saveFileDialog1.ShowDialog() ==  DialogResult.OK)
			{
				richTextBox1.SaveFile(saveFileDialog1.FileName,RichTextBoxStreamType.PlainText);
				label1.Text = saveFileDialog1.FileName;
			}
			else
			{
				richTextBox1.Focus();
			}
		}
		
		//Open file method
		private void OpenFile()
		{
			openFileDialog1.FileName = string.Empty;
			openFileDialog1.Filter = "Text Files|*.txt";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if(openFileDialog1.FileName ==  string.Empty){
					return;
				}
				else{
					richTextBox1.Clear();
					richTextBox1.LoadFile(openFileDialog1.FileName,RichTextBoxStreamType.PlainText);
					label1.Text =  openFileDialog1.FileName;
				}
			}
		}
		
		//new file method
		private void NewFile()
		{
			if(richTextBox1.Text == string.Empty){
				richTextBox1.Focus();
			}else{
				DialogResult dr =  MessageBox.Show("Do you want to save this file first?","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
				if(dr == DialogResult.Yes){
					if(label1.Text == "Untitled"){
						saveFile();
						richTextBox1.Clear();
						label1.Text = "Untitled";
					}
					else{
						richTextBox1.SaveFile(label1.Text,RichTextBoxStreamType.PlainText);
						richTextBox1.Clear();
						label1.Text = "Untitled";
					}
				}
				if(dr == DialogResult.No){
					richTextBox1.Clear();
					label1.Text = "Untitled";
				}else
					this.Focus();
			}
		}
		
		//exit method
		private void Exit()
		{
			if(richTextBox1.Text !=  string.Empty || label1.Text != "Untitled")
			{
				DialogResult dr =  MessageBox.Show("Do you want to save this file first?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
				if(dr == DialogResult.Yes){
					if(label1.Text == "Untitled"){
						saveFile();
						this.Close();
					}
					else{
						richTextBox1.SaveFile(label1.Text,RichTextBoxStreamType.PlainText);
						this.Close();
					}
				}
				else{
					this.Close();
				}
			}else{
				this.Close();
			}
		}
		
		//	this event handler will executed when the "X" button was click and close the application
		void Button1Click(object sender, EventArgs e)
		{
			Exit();
		}
		//minimize/"-" button event handler
		void Button2Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		
		//all event handler below is functioned to move tha application window
		private void MainFormMouseDown(object sender, MouseEventArgs e)
    	{
        	mouseDown = true;
        	lastLocation = e.Location;
    	}

   		 private void MainFormMouseMove(object sender, MouseEventArgs e)
    	{
        	if(mouseDown)
        	{
            	this.Location = new Point(
              	  	(this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

            	this.Update();
        	}
    	}

    	private void MainFormMouseUp(object sender, MouseEventArgs e)
    	{
        	mouseDown = false;
    	}//
	}
}// end of code


