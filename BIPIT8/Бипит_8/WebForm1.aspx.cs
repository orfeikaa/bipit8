using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Бипит_8
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Get();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                Get();
            }
            else if (TextBox1.Text != "" || TextBox2.Text != "")
            {
                GetData(DateTime.Parse(TextBox1.Text), DateTime.Parse(TextBox2.Text));
            }
        }

        public void Get()
        {
            var Tabl_arenda = new DataTable();
            if (Tabl_arenda.Columns.Count == 0)
            {
                Tabl_arenda.Columns.Add("Код", typeof(int));
                Tabl_arenda.Columns.Add("Авто", typeof(string));
                Tabl_arenda.Columns.Add("ФИО", typeof(string));
                Tabl_arenda.Columns.Add("Дата взятия", typeof(DateTime));
                Tabl_arenda.Columns.Add("Дата возрата", typeof(DateTime));
            }

            using (ArendaContext db = new ArendaContext())
            {
                foreach (Arenda app in db.Arenda)
                {
                    var Tabl_rows = Tabl_arenda.NewRow();
                    Tabl_rows[0] = app.Id;

                    using (AvtoContext db1 = new AvtoContext())
                    {
                        var avto = db1.Avto.Where(s => s.Id == app.Id_avto).FirstOrDefault();
                        Tabl_rows[1] = avto.Model;
                    }

                    using (FIOContext db2 = new FIOContext())
                    {
                        var fio = db2.FIO.Where(p => p.Id == app.Id_fio).FirstOrDefault();
                        Tabl_rows[2] = fio.Fio;
                    }
                    string[] Data_take = app.Data_take.ToString().Split(' ');
                    string[] Data_re = app.Data_re.ToString().Split(' ');
                    Tabl_rows[3] = Data_take[0];
                    Tabl_rows[4] = Data_re[0];
                    Tabl_arenda.Rows.Add(Tabl_rows);
                }
                GridView1.DataSource = Tabl_arenda;
                GridView1.DataBind();
            }
        }

        public void GetData(DateTime d1, DateTime d2)
        {
            var Tabl_arenda = new DataTable();
            if (Tabl_arenda.Columns.Count == 0)
            {
                Tabl_arenda.Columns.Add("Код", typeof(int));
                Tabl_arenda.Columns.Add("Авто", typeof(string));
                Tabl_arenda.Columns.Add("ФИО", typeof(string));
                Tabl_arenda.Columns.Add("Дата взятия", typeof(DateTime));
                Tabl_arenda.Columns.Add("Дата возрата", typeof(DateTime));
            }

            using (ArendaContext db = new ArendaContext())
            {
                foreach (Arenda app in db.Arenda)
                {
                    if ((app.Data_take >= d1 && app.Data_take <= d2) || (app.Data_re >= d1 && app.Data_re <= d2))
                    {
                        var Tabl_rows = Tabl_arenda.NewRow();
                        Tabl_rows[0] = app.Id;

                        using (AvtoContext db1 = new AvtoContext())
                        {
                            var avto = db1.Avto.Where(s => s.Id == app.Id_avto).FirstOrDefault();
                            Tabl_rows[1] = avto.Model;
                        }

                        using (FIOContext db2 = new FIOContext())
                        {
                            var fio = db2.FIO.Where(p => p.Id == app.Id_fio).FirstOrDefault();
                            Tabl_rows[2] = fio.Fio;
                        }
                        string[] Data_take = app.Data_take.ToString().Split(' ');
                        string[] Data_re = app.Data_re.ToString().Split(' ');
                        Tabl_rows[3] = Data_take[0];
                        Tabl_rows[4] = Data_re[0];
                        Tabl_arenda.Rows.Add(Tabl_rows);
                    }
                    
                }
                GridView1.DataSource = Tabl_arenda;
                GridView1.DataBind();
            }
        }
        

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (ArendaContext db = new ArendaContext())
            {
                foreach (GridViewRow rowInGrid in GridView1.Rows)
                {
                    CheckBox checkBox = (CheckBox)rowInGrid.FindControl("CheckBox1");
                    if (checkBox != null && checkBox.Checked)
                    {
                        int id = int.Parse(GridView1.Rows[rowInGrid.RowIndex].Cells[1].Text);
                        Arenda delId = db.Arenda.Where(s => s.Id == id).FirstOrDefault();
                        if (delId != null)
                        {
                            db.Arenda.Remove(delId);
                            db.SaveChanges();
                        }
                    }
                }
                Response.Redirect("WebForm1.aspx");
            }
                
        }
    }
}