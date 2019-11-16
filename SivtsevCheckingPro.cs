﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Windows.Forms;
using System.IO;


namespace GoodVision
{
    public partial class SivtsevCheckingPro : Form
    {

		public SivtsevCheckingPro()
        {
            InitializeComponent();
          
        }
        int i = 6, temp;
		Letter NewLetter= new Letter();
		int rightAnswer = 0;
		int tests = 0;
		int left = 1;
		int right = 12;
		bool eye = true;
		UserClass User = new UserClass() ;
		GoodVisionClass MyVision= new GoodVisionClass();

		

        private void AnswerSivtsevButton_Click(object sender, EventArgs e)
        {
			
				if (AnswerTextBox.Text == NewLetter.Get_Letter())
				{
					rightAnswer++;
				}

				i++;

			if (tests < 3)
			{
				NewLetter.Set_Letter();
				LetterPictureBox.Image = NewLetter.ShowImage;
			}
			else if (rightAnswer >= 2)
			{
				left = NewLetter.ObjectRow;
				NewLetter.ObjectRow = (left + right) / 2;
				tests = 0;
				NewLetter.Set_Letter();

				LetterPictureBox.Image = NewLetter.ShowImage;
			}
			else
			{
				right = NewLetter.ObjectRow;
				if (left < right)
				{
					NewLetter.ObjectRow = (left + right) / 2;
					NewLetter.CalcSize();
					LetterPictureBox.Image = NewLetter.ShowImage;
					tests = 0;
				}
				else
				{
					if (eye)//какой глаз сейчас проверяем
					{
						User.right = NewLetter.Get_result(NewLetter.ObjectRow - 1);
						eye = false;
						// вставить предупреждение про проверку левого глаза
					}
					else
						User.left = NewLetter.Get_result(NewLetter.ObjectRow - 1);
					MyVision.Add_to_file(ref User);
					AfterTestingForm form = new AfterTestingForm();
					form.Show();
					this.Hide();
				}
			
			}


				SivtsevTimer.Value = 0;
				temp = i;
				timer1.Enabled = true;
			
			
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            SivtsevTimer.Value = 0;
            EyeTestPanel.Visible = false;             // предупреждение про проверку правого глаза уходит
            System.Threading.Thread.Sleep(100);
            timer1.Enabled = true;
            temp = i;
        }

        private void AnswerTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BackFromSivtsevButton_Click(object sender, EventArgs e)
        {
            VisionCheck Vch = new VisionCheck();
            Vch.Show();
            this.Hide();
        }


        private void timer1_Tick(object sender, EventArgs e) 
        {
                    
                temp--;
                SivtsevTimer.Text = Convert.ToString(temp);
            SivtsevTimer.PerformStep();
            if (temp != 0)
            timer1.Enabled = true;
            
            else
             timer1.Enabled = false;
            }

		private void SivtsevCheckingPro_Load(object sender, EventArgs e)
		{
			FileStream session = new FileStream("session.txt", FileMode.Open, FileAccess.Read);
			if (session != null)
			{
				StreamReader reader = new StreamReader(session);
				User.Nick = reader.ReadToEnd();
				session.Close();
			}
		}

		private void BackToVisionCheckButton_Click(object sender, EventArgs e)
        {
            VisionCheck Vch = new VisionCheck();
            Vch.Show();
            this.Hide();
        }



    
    }
}
