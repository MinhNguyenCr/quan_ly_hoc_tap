﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        // button cancel ( close the form )
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        

        bool onlyLetter(string s)
        {
            foreach(char c in s)
            {
                if (char.IsDigit(c)) return false; 
            }   
            return true;
        }
        // button insert a new student
        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            try
            {


                STUDENT student = new STUDENT();
                int id = Convert.ToInt32(txtStudentID.Text);
                string fname = TextBoxFname.Text;
                string lname = TextBoxLname.Text;
                DateTime bdate = (DateTimePicker1.Value);
                string phone = TextBoxPhone.Text;
                string adrs = TextBoxAddress.Text;
                string gender = "Male";
                string Email = txtStudentID.Text + "@student.hcmute.edu.vn";
                if (RadioButtonFemale.Checked)
                {
                    gender = "Female";
                }
                if (!onlyLetter(fname) || !onlyLetter(lname)) throw new Exception();
                   


                MemoryStream pic = new MemoryStream();
                int born_year = DateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;
                //  sv tu 10-100,  co the thay doi
                if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
                {
                    MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
                else if (verif())
                {
                    PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                    if (student.insertStudent(id, fname, lname, bdate, gender,Email, phone, adrs, pic))
                    {
                        MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch( Exception ex)
            {
                MessageBox.Show("Lỗi "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }


}
        //  chuc nang kiem tra du lieu input
        bool verif()
        {
            if ((TextBoxFname.Text.Trim() == "")
                        || (TextBoxLname.Text.Trim() == "")
                        || (TextBoxAddress.Text.Trim() == "")
                        || (TextBoxPhone.Text.Trim() == "")
                        || (PictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif; *.jfif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }
        
        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
