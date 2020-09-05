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
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
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
		//end file menustrip
		
		
		//edit menustrip
		void UndoToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (richTextBox1.CanUndo){
				richTextBox1.Undo();
			}
			redoToolStripMenuItem.Enabled = true;
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
		
		void DateToolStripMenuItemClick(object sender, EventArgs e)
		{
			string Date =  DateTime.Now.ToString("yy-MM-dd");
			richTextBox1.Text =  richTextBox1.Text  + Date;
		}
		
		//others
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
			undoToolStripMenuItem.Enabled = true;
		}
		void RichTextBox1SelectionChanged(object sender, EventArgs e)
		{
			copyToolStripMenuItem.Enabled  =  (richTextBox1.SelectedText.Length > 0)?true:false;
			deleteToolStripMenuItem.Enabled =  (richTextBox1.SelectedText.Length > 0)?true:false;
			cutToolStripMenuItem.Enabled = (richTextBox1.SelectedText.Length > 0)?true:false;
		}

				private void saveFile()
		{
			saveFileDialog1.Title = "Save";
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

	}
}
