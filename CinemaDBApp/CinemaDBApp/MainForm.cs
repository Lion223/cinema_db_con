using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CinemaDBApp
{
    public partial class MainForm : Form
    {
        DbDataReader dr;
        ListViewItem item;
        static string dis = ConfigurationManager.ConnectionStrings["disEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(dis);

        public MainForm()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //con.Open();
            //if (con.State == ConnectionState.Open) { MessageBox.Show("Connection with \"lab\" was successfully established!"); }
            //else
            //{
            //    MessageBox.Show("Connection to \"dis\" failed!");
            //    Console.ReadKey();
            //    return;
            //}
            //this.Text = "DB: " + ConfigurationManager.ConnectionStrings["disEntities"].Name;
            //DataTable dt = con.GetSchema("Tables");
            //foreach (DataRow row in dt.Rows)
            //{
            //    string tablename = (string)row[2];
            //    if (tablename != "sysdiagrams")
            //        listBox1.Items.Add(tablename);
            //}
            //button2.Enabled = false;
            //button3.Enabled = false;
            //button4.Enabled = false;
            //con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cnString = dis;
            MessageBox.Show("From App.Config: " + cnString);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string seltable = listBox1.GetItemText(listBox1.SelectedItem);
            if (seltable == "")
            {
                MessageBox.Show("Please choose a table!");
                return;
            }

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM " + seltable;
            label1.Text = seltable;

            listView1.Clear();

            if (seltable == "Films")
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Id", 45);
                listView1.Columns.Add("Name", 185);
                listView1.Columns.Add("Year", 65);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    item = new ListViewItem(dr["Id"].ToString());
                    item.SubItems.Add(dr["Name"].ToString());
                    item.SubItems.Add(dr["Year"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();
                con.Close();
            }
            else if (seltable == "Actors")
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Id", 45);
                listView1.Columns.Add("First Name", 125);
                listView1.Columns.Add("Last Name", 125);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    item = new ListViewItem(dr["Id"].ToString());
                    item.SubItems.Add(dr["firstName"].ToString());
                    item.SubItems.Add(dr["lastName"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();
                con.Close();
            }
            else if (seltable == "Genres")
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Id", 45);
                listView1.Columns.Add("Name", 125);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    item = new ListViewItem(dr["Id"].ToString());
                    item.SubItems.Add(dr["Name"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();
                con.Close();
            }
            else if (seltable == "FGA")
            {
                listView1.Columns.Clear();
                listView1.Columns.Add("Film Id", 55);
                listView1.Columns.Add("Genre Id", 55);
                listView1.Columns.Add("Actor Id", 55);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    item = new ListViewItem(dr["Film_Id"].ToString());
                    item.SubItems.Add(dr["Genre_Id"].ToString());
                    item.SubItems.Add(dr["Actor_Id"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Films")
            {
                int id, year;
                string name;
                Console.WriteLine("Adding a new record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.Write("Year: ");
                year = Convert.ToInt32(Console.ReadLine());

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Films (Id, Name, Year) VALUES ( " + id + " ,\'" + name + "\' , " + year + " )";
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            else if (label1.Text == "Actors")
            {
                int id;
                string firstName, lastName;
                Console.WriteLine("Adding a new record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("First name: ");
                firstName = Console.ReadLine();
                Console.Write("Last name: ");
                lastName = Console.ReadLine();

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Actors (Id, firstName, lastName) VALUES ( " + id + " ,\'" + firstName + "\' , \'" + lastName + "\')";
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            else if (label1.Text == "Genres")
            {
                int id;
                string name;
                Console.WriteLine("Adding a new record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                name = Console.ReadLine();

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Genres (Id, Name) VALUES ( " + id + " ,\'" + name + "\' )";
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            else if (label1.Text == "FGA")
            {
                int idf, ida, idg;
                Console.WriteLine("Adding a new record...");
                Console.Write("Film id: ");
                idf = Convert.ToInt32(Console.ReadLine());
                Console.Write("Actor id: ");
                ida = Convert.ToInt32(Console.ReadLine());
                Console.Write("Genre id: ");
                idg = Convert.ToInt32(Console.ReadLine());

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO FGA (Film_Id, Genre_Id, Actor_Id) VALUES ( " + idf + ", " + ida + ", " + idg + " )";
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex1)
                {
                    MessageBox.Show(ex1.ToString());
                }
                Console.WriteLine("\nDone!");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Films")
            {
                int id, year;
                string name;
                Console.WriteLine("Updating a record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.Write("Year: ");
                year = Convert.ToInt32(Console.ReadLine());

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Films SET Name=\'" + name + "\', Year=" + year + " WHERE Id=" + id;
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            if (label1.Text == "Actors")
            {
                int id;
                string firstName, lastName;
                Console.WriteLine("Updating a record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("First name: ");
                firstName = Console.ReadLine();
                Console.Write("Last name: ");
                lastName = Console.ReadLine();

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Actors SET firstName=\'" + firstName + "\', lastName=\'" + lastName + "\' WHERE Id=" + id;
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            if (label1.Text == "Genres")
            {
                int id;
                string name;
                Console.WriteLine("Updating a record...");
                Console.Write("Id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Name: ");
                name = Console.ReadLine();

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Genres SET Name=\'" + name + "\' WHERE Id=" + id;
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nDone!");
                con.Close();
            }
            if (label1.Text == "FGA")
            {
                int idf, ida, idg;
                Console.WriteLine("Updating a new record...");
                Console.Write("Film id: ");
                idf = Convert.ToInt32(Console.ReadLine());
                Console.Write("Actor id: ");
                ida = Convert.ToInt32(Console.ReadLine());
                Console.Write("Genre id: ");
                idg = Convert.ToInt32(Console.ReadLine());

                var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE FGA SET Genre_Id=" + idg + ", Actor_Id=" + ida + " WHERE Id=" + idf;
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex1)
                {
                    MessageBox.Show(ex1.ToString());
                }
                Console.WriteLine("\nDone!");
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id;
            Console.WriteLine("Deleting a record...");
            Console.Write("Id: ");
            id = Convert.ToInt32(Console.ReadLine());

            var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            if (label1.Text != "FGA")
            {
                cmd.CommandText = "DELETE FROM " + label1.Text + " WHERE Id=" + id;
            }
            else if (label1.Text == "FGA")
            {
                cmd.CommandText = "DELETE FROM " + label1.Text + " WHERE Film_Id=" + id;
            }

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex1)
            {
                MessageBox.Show(ex1.ToString());
            }
            Console.WriteLine("\nDone!");
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var nwSet = new DataSet("DS1");

            var fcmd = (SqlCommand)con.CreateCommand();
            fcmd.CommandType = CommandType.Text;
            fcmd.CommandText = "Select Id, Name, Year From Films";
            var fda = new SqlDataAdapter(fcmd);
            var fbldr = new SqlCommandBuilder(fda);
            fda.Fill(nwSet, "Films");
            var fTable = nwSet.Tables["Films"];

            var gcmd = (SqlCommand)con.CreateCommand();
            gcmd.CommandText = "Select Id, Name From Genres";
            var gda = new SqlDataAdapter(gcmd);
            var gbldr = new SqlCommandBuilder(gda);
            gda.Fill(nwSet, "Genres");
            var gTable = nwSet.Tables["Genres"];

            var acmd = (SqlCommand)con.CreateCommand();
            acmd.CommandText = "Select Id, firstName, lastName From Actors";
            var ada = new SqlDataAdapter(acmd);
            var abldr = new SqlCommandBuilder(ada);
            ada.Fill(nwSet, "Actors");
            var aTable = nwSet.Tables["Actors"];

            var fgacmd = (SqlCommand)con.CreateCommand();
            fgacmd.CommandText = "Select Film_Id, Genre_Id, Actor_Id From FGA";
            var fgada = new SqlDataAdapter(fgacmd);
            var fgabldr = new SqlCommandBuilder(fgada);
            fgada.Fill(nwSet, "FGA");
            var fgaTable = nwSet.Tables["FGA"];

            nwSet.Relations.Add("films_throughttable", fTable.Columns["Id"], fgaTable.Columns["Film_Id"]);
            nwSet.Relations.Add("genres_throughttable", gTable.Columns["Id"], fgaTable.Columns["Genre_Id"]);
            nwSet.Relations.Add("actors_throughttable", aTable.Columns["Id"], fgaTable.Columns["Actor_Id"]);

            fTable.Rows.Add(12, "Gone Sovereign", 2017);
            fTable.Rows.Add(13, "Stone Sour", 2019);

            var updRow = aTable.Select("Id=3")[0];
            updRow["firstName"] = "Updated";
            updRow["lastName"] = "byDataSet";

            var delRow = gTable.Select("Name=\'Realistic\'")[0];
            delRow.Delete();

            fda.Update(nwSet, "Films");
            gda.Update(nwSet, "Genres");

            MessageBox.Show("DbDataAdapter's have been successfully created!");
            MessageBox.Show("Dataset has been successfully created!");
            MessageBox.Show("Dataset has been successfully filled!");
            MessageBox.Show("Changes have been successfully applied to the DB!");
        }
    }
}
