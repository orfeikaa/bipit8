using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Бипит_8
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                using (AvtoContext db = new AvtoContext())
                {
                    var Tabl_avto = new DataTable();
                    if (Tabl_avto.Columns.Count == 0)
                    {
                        Tabl_avto.Columns.Add("Id", typeof(int));
                        Tabl_avto.Columns.Add("Model", typeof(string));
                    }
                    foreach (Avto s in db.Avto)
                    {
                        var Tabl_rows = Tabl_avto.NewRow();
                        Tabl_rows[0] = s.Id;
                        Tabl_rows[1] = s.Model;
                        Tabl_avto.Rows.Add(Tabl_rows);
                    }
                    DropDownList1.DataSource = Tabl_avto;
                    DropDownList1.DataTextField = "Model";
                    DropDownList1.DataValueField = "Id";
                    DropDownList1.DataBind();
                }

                using (FIOContext db = new FIOContext())
                {
                    var Tabl_fio = new DataTable();
                    if (Tabl_fio.Columns.Count == 0)
                    {
                        Tabl_fio.Columns.Add("Id", typeof(int));
                        Tabl_fio.Columns.Add("Fio", typeof(string));
                    }
                    foreach (FIO p in db.FIO)
                    {
                        var Tabl_rows = Tabl_fio.NewRow();
                        Tabl_rows[0] = p.Id;
                        Tabl_rows[1] = p.Fio;
                        Tabl_fio.Rows.Add(Tabl_rows);
                    }
                    DropDownList2.DataSource = Tabl_fio;
                    DropDownList2.DataTextField = "Fio";
                    DropDownList2.DataValueField = "Id";
                    DropDownList2.DataBind();
                }
            }

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (ArendaContext db = new ArendaContext())
            {
                Arenda app = new Arenda() 
                {
                    Id_avto = int.Parse(DropDownList1.Text),
                    Id_fio = int.Parse(DropDownList2.Text), 
                    Data_take = DateTime.Parse(TextBox1.Text),
                    Data_re = DateTime.Parse(TextBox2.Text)
                };
                db.Arenda.Add(app);
                db.SaveChanges();
            }
            Response.Redirect("WebForm1.aspx");
        }
    }
}