using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAp_MVC_L46_EF_52323
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        DB_L46_MVC_EF_52323Entities1 dbo = new DB_L46_MVC_EF_52323Entities1();
        tblEmployee obj = new tblEmployee(); // tblEmployee is table class name
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGrid();
            }
        }

        public void GetGrid()
        {
            grd.DataSource =dbo.tblEmployees.ToList();// we bind gridview with table always
            grd.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
           
            obj.name = txtname.Text; // obj is tableclass object
            obj.city = txtcity.Text;
            obj.age = Convert.ToInt32(txtage.Text);
            //DB_L46_MVC_EF_52323Entities1 dbo = new DB_L46_MVC_EF_52323Entities1();// DB_L46_MVC_EF_52323Entities1 database class name
            dbo.tblEmployees.Add(obj);// tblEmployees is table name
            dbo.SaveChanges();
            GetGrid();

        }
    }
}