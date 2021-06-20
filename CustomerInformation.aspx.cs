using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CustomerInformation : System.Web.UI.Page
    {
        private string connstr = string.Empty;
        private string buttoncommand = string.Empty;
        private long   msrlno = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            connstr = ConfigurationManager.ConnectionStrings["custcondb"].ConnectionString;

            if (!IsPostBack)
            {
                setControlState(false);
                LoadFirstCustomerData();
                LoadCustomerGrid();
            }
        }

        private bool CheckCustomerData()
        {
            string lSQL = "select count(*) from customer where [FirstName] = '" + txtFirstName.Text.Replace("'", "''") + "' and [LastName] = '" + txtLastName.Text.Replace("'", "''") + "';";

            SqlCommand cmd = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    cmd = new SqlCommand(lSQL, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    int lcustcount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (lcustcount > 0)
                    {
                        lblMessage.Text = "Duplicate customer information exists.";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        private void setControlState(bool vEnable)
        {
            if (!string.IsNullOrEmpty(buttoncommand))
                if (buttoncommand == "Add")
                {
                    txtFirstName.Enabled = true;
                    txtLastName.Enabled = true;
                }
                else
                {
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                }
            else
            {
                txtFirstName.Enabled = vEnable;
                txtLastName.Enabled = vEnable;
            }
            // txtCustomer.Enabled = (buttoncommand == "Add" || buttoncommand == "Update") ? !vEnable : vEnable;
            txtPanNo.Enabled = vEnable;
            txtAddress.Enabled = vEnable;
            txtPinCode.Enabled = vEnable;
            txtCity.Enabled = vEnable;
            txtState.Enabled = vEnable;
            rbFeeConfirm.Enabled = vEnable;
            nbxAmount.Enabled = vEnable;
            dtbPayment.Enabled = vEnable;
            btnAdd.Enabled = !vEnable;
            btnEdit.Enabled = !vEnable;
            btnDelete.Enabled = !vEnable;
            btnSubmit.Enabled = vEnable;

            if (!btnSubmit.Enabled)
                btnSubmit.Visible = false;
            else
                btnSubmit.Visible = true;
        }

        private long GetMaxSrlNo()
        {
            string lSQL = "select isnull(max([SrlNo]),0) + 1 from customer;";

            SqlCommand cmd = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    cmd = new SqlCommand(lSQL, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
                return 0;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CheckCustomerData())
                return;

            SqlCommand cmd = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    if (ViewState["OP"].ToString() == "Add")
                        msrlno = GetMaxSrlNo();
                    else
                        msrlno = Convert.ToInt64(txtSrlNo.Text);
                    string custno = txtFirstName.Text.Substring(0,1) + txtPanNo.Text;

                    cmd = new SqlCommand("sp_customers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operation", ViewState["OP"].ToString());
                    cmd.Parameters.AddWithValue("@srlno", msrlno);
                    cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@lastname", txtLastName.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@panno", txtPanNo.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@pincode", txtPinCode.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@city", txtCity.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@state", txtState.Text.ToUpper().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@paidfee", rbFeeConfirm.SelectedValue);
                    cmd.Parameters.AddWithValue("@amount", nbxAmount.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@dateofpayment", dtbPayment.Text.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@custno", custno);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    LoadCustomerGrid();

                    lblMessage.Text = "Customer Information has been added successfully and Customer Id is "+custno;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
            }
            finally
            {
                cmd.Dispose();
            }
            setControlState(false);
        }

        private DataSet LoadCustomerGrid()
        {
            string lSQL = string.Empty;
            DataSet lDS = new DataSet();
            SqlDataAdapter da = null;

            lSQL = "Select * from customer;";

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    con.Open();
                    da = new SqlDataAdapter(lSQL, con);
                    da.Fill(lDS);
                    grdCustomer.DataSource = lDS;
                    grdCustomer.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
            }
            finally
            {
                da.Dispose();
            }
            return lDS;
        }

        private void LoadFirstCustomerData()
        {
            string lSQL = string.Empty;
            SqlDataReader rdr = null;
            SqlCommand cmd = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    con.Open();
                    cmd = new SqlCommand("select top 1 * from customer;", con);

                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            txtFirstName.Text = (string)rdr["FirstName"];
                            txtLastName.Text = (string)rdr["LastName"];
                            txtPanNo.Text = (string)rdr["PanNo"];
                            txtAddress.Text = (string)rdr["Address"];
                            txtPinCode.Text = rdr["PinCode"].ToString();
                            txtCity.Text = (string)rdr["City"];
                            txtState.Text = (string)rdr["State"];
                            if (Convert.ToBoolean(rdr["PaidFee"]).ToString().ToUpper() == "TRUE")
                                rbFeeConfirm.SelectedValue = "1";
                            else
                                rbFeeConfirm.SelectedValue = "0";
                            nbxAmount.Text = Convert.ToDouble(rdr["Amount"]).ToString();
                            dtbPayment.Text = Convert.ToDateTime(rdr["DateofPayment"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
            }
            finally
            {
                rdr.Close();
                cmd.Dispose();
            }
        }

        private void GetData(long vsrlno)
        {
            string lSQL = string.Empty;
            SqlDataReader rdr = null;
            SqlCommand cmd = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    con.Open();
                    cmd = new SqlCommand("select * from customer where [SrlNo] = " + vsrlno + ";", con);

                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            txtSrlNo.Text = vsrlno.ToString();
                            txtFirstName.Text = (string)rdr["FirstName"];
                            txtLastName.Text = (string)rdr["LastName"];
                            txtPanNo.Text = (string)rdr["PanNo"];
                            txtAddress.Text = (string)rdr["Address"];
                            txtPinCode.Text = rdr["PinCode"].ToString();
                            txtCity.Text = (string)rdr["City"];
                            txtState.Text = (string)rdr["State"];
                            if (Convert.ToBoolean(rdr["PaidFee"]).ToString().ToUpper() == "TRUE")
                                rbFeeConfirm.SelectedValue = "1";
                            else
                                rbFeeConfirm.SelectedValue = "0";
                            nbxAmount.Text = Convert.ToDouble(rdr["Amount"]).ToString();
                            dtbPayment.Text = Convert.ToDateTime(rdr["DateofPayment"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error in Connection " + "[" + ex.Message + "]";
            }
            finally
            {
                rdr.Close();
                cmd.Dispose();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            buttoncommand = (string)(sender as Button).Text;
            ViewState["OP"] = buttoncommand;
            setControlState(true);
        }

        public void func_clearform()
        {

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPanNo.Text = "";
            txtAddress.Text = "";
            txtPinCode.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtSrlNo.Text = "0";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            func_clearform();
            buttoncommand = (string)(sender as Button).Text;
            ViewState["OP"] = buttoncommand;
            setControlState(true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            buttoncommand = (string)(sender as Button).Text;
            ViewState["OP"] = buttoncommand;
            setControlState(false);
        }

        protected void grdCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lsrlno = Convert.ToInt64(grdCustomer.SelectedRow.Cells[1].Text);
            GetData(lsrlno);
        }

        protected void grdCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
}