/*
 * Created by SharpDevelop.
 * User: Salman-pc
 * Date: 26/10/2020
 * Time: 11:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyNotes
{
	/// <summary>
	/// Description of About.
	/// </summary>
	public partial class About : Form
	{
		ToolTip Mytooltip = new ToolTip();
		public About()
		{
			InitializeComponent();
		}
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/AlphaBeta1906/MyNotes");
		}
		void LinkLabel1MouseHover(object sender, EventArgs e)
		{
			Mytooltip.Show("Go to Github",linkLabel1);
		}
	}
}
