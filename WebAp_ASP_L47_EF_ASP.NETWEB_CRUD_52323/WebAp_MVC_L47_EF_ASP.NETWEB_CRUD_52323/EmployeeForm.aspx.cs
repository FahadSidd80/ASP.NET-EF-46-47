using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Data.Entity;

namespace WebAp_MVC_L47_EF_ASP.NETWEB_CRUD_52323
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        DatabaseContext _db = new DatabaseContext();
        tblEmployee _emp = new tblEmployee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        public void BindGrid()
        {
            //var data = _db.tblEmployees.ToList();
            //grd.DataSource = _db.tblEmployees.ToList();
            var data = (from a in  _db.tblEmployees select a).ToList();//By using LINQ we get data from table and lINQ only used for select query.
            //var data = (from a in  _db.tblEmployees where a.empid == 2 select a).ToList();//By using LINQ we get data from table and lINQ only used for select query.
            grd.DataSource = data;
            grd.DataBind();
        }

        public void Clear()
        {
            txtname.Text = "";
            txtaddress.Text = "";
            txtage.Text = "";
            btnsave.Text = "Save";
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text== "Save")
            {
                _emp.name = txtname.Text;
                _emp.address = txtaddress.Text;
                _emp.age = Convert.ToInt32(txtage.Text);
                _db.tblEmployees.Add(_emp);
                _db.SaveChanges();
                BindGrid();
                Clear();
            }
            else
            {
                _emp.empid = Convert.ToInt32(ViewState["IDD"]);
                _emp.name = txtname.Text;
                _emp.address = txtaddress.Text;
                _emp.age = Convert.ToInt32(txtage.Text);
                //_db.tblEmployees.Add(_emp);
                _db.Entry(_emp).State = System.Data.Entity.EntityState.Modified;// _emp us row ki id ko update karega jiski id ke recored ko viewstate se li gayi ho. and wo id hamesha primary key hogi and us id ko hata ke uske saare record modifiy karega
                //_db.Entry(_emp).State = EntityState.Modified;// _emp us row ki id ko update karega jiski id ke recored ko viewstate se li gayi ho. and wo id hamesha primary key hogi and us id ko hata ke uske saare record modifiy karega
                _db.SaveChanges();
                BindGrid();
                Clear();

                //Entity type
                //     A person, organization, object type, or concept about which information
                //     is stored.Describes the type of the information that is being mastered.
                //     An entity type typically corresponds to one or several related tables in database.

                //Entity
                //    A single unique object in the real world that is being mastered.
                //    Examples of an entity are a single person, 
                //    single product, or single organization.
            }
          
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int IDD = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Del")
            {
                //int IDD = Convert.ToInt32(e.CommandArgument);
                var data = _db.tblEmployees.Find(IDD);// Find() function is used to find all record by using iD
                //var data = from a in _db.tblEmployees where a.empid == IDD select a; // LINQ but not worked
                //var data = (from a in _db.tblEmployees where a.empid == IDD select a).ToList();
                //_ = _db.tblEmployees.Remove((tblEmployee)data);
                // _db.tblEmployees.Remove((tblEmployee)data);
                _db.tblEmployees.Remove(data);
                _db.SaveChanges();
                BindGrid();
            }
            else if(e.CommandName == "Edt")
            {
                var data = (from a in _db.tblEmployees where a.empid == IDD select a).ToList();
                txtname.Text = data[0].name.ToString();
                txtaddress.Text = data[0].address.ToString();
                txtage.Text = data[0].age.ToString();
                btnsave.Text = "Update";
                ViewState["IDD"] = e.CommandArgument;
            }
        }
    }
}