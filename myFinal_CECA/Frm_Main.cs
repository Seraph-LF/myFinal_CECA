using FontAwesome.Sharp;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myFinal_CECA
{
    public partial class Frm_Main : Form
    {

        public Frm_Main()
        {
            InitializeComponent();
            bunifuFormDock1.SubscribeControlToDragEvents(panel1);
            bunifuFormDock1.SubscribeControlToDragEvents(panel2);
            bunifuFormDock1.SubscribeControlToDragEvents(panel3);
            ShowData();
        }

        private void bunifuFormDock1_FormDragging(object sender, Bunifu.UI.WinForms.BunifuFormDock.FormDraggingEventArgs e)
        {

        }

        public void ShowData()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\myFinal_CECA\\myFinal_CECA\\AuthDB.mdf;Integrated Security=True");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Activity", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvAct.DataSource = dt;
        }

        //ChildForms - pages
        private void btnProject_Click(object sender, EventArgs e)
        {
            btnProfile.Top = ((Control)sender).Top;
            pages.SetPage("Projects");
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            btnProfile.Top = ((Control)sender).Top;
            pages.SetPage("Task");
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            btnProfile.Top = ((Control)sender).Top;
            pages.SetPage("Users");
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            btnProfile.Top = ((Control)sender).Top;
            pages.SetPage("Settings.Company");
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            btnProfile.Top = ((Control)sender).Top;
            pages.SetPage("Profile.ChangePass");
        }
        //Insert
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\myFinal_CECA\\myFinal_CECA\\AuthDB.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Activity values(@Act_ID,@Act_Title,@Act_Des,@Act_Date,@Act_Loc,@Act_Point)", con);
            cmd.Parameters.AddWithValue("@Act_ID", int.Parse(txtID.Text));
            cmd.Parameters.AddWithValue("@Act_Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@Act_Des", txtDes.Text);
            cmd.Parameters.AddWithValue("@Act_Date", DtpDate.Value.Date);
            cmd.Parameters.AddWithValue("@Act_Loc", txtLoc.Text);
            cmd.Parameters.AddWithValue("@Act_Point", double.Parse(txtPoint.Text));
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Succesfully Inserted");
        }
        //Update
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\myFinal_CECA\\myFinal_CECA\\AuthDB.mdf;Integrated Security=True");
            SqlConnection con = sqlConnection;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update Activity set Act_Title=@Act_Title,Act_Des=@Act_Des,Act_Date=@Act_Date,Act_Loc=@Act_Loc,Act_Point=@Act_Point where Act_ID = @Act_ID",con);
            cmd.Parameters.AddWithValue("@Act_ID", int.Parse(txtID.Text));
            cmd.Parameters.AddWithValue("@Act_Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@Act_Des", txtDes.Text);
            cmd.Parameters.AddWithValue("@Act_Date", DtpDate.Value.Date);
            cmd.Parameters.AddWithValue("@Act_Loc", txtLoc.Text);
            cmd.Parameters.AddWithValue("@Act_Point", double.Parse(txtPoint.Text));
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Succesfully Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\myFinal_CECA\\myFinal_CECA\\AuthDB.mdf;Integrated Security=True");
            SqlConnection con = sqlConnection;
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete Activity where Act_ID = @Act_ID", con);
            cmd.Parameters.AddWithValue("@Act_ID", int.Parse(txtID.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Succesfully Deleted");
        }
        //working search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\myFinal_CECA\\myFinal_CECA\\AuthDB.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(sqlConnection);
            con.Open();
            string SearchData = txtSearch.Text;
            string Query = "SELECT * FROM Activity WHERE Act_Id LIKE '%" + SearchData + "%' OR Act_Title LIKE '%" + SearchData + "%'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvAct.DataSource = dt;

            var reader = cmd.ExecuteReader();
            dt.Rows.Clear();

            while (reader.Read())
            {
                dt.Rows.Add(reader["Act_ID"], reader["Act_Title"], reader["Act_Des"], reader["Act_Date"], reader["Act_Loc"], reader["Act_Point"]);
            }
            con.Close();
            

            //SqlCommand cmd = new SqlCommand("Select * from Activity where Act_ID = @Act_ID or Act_Title = @Act_Title", con);
            //cmd.Parameters.AddWithValue("@Act_ID", txtSearch.Text);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dgvAct.DataSource = dt;
        }
    }
}
