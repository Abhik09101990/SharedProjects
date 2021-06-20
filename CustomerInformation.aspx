<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerInformation.aspx.cs" Inherits="WebApplication1.CustomerInformation" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script>
<script src="js/scripts.js"></script>


</head>
    <script>
        $(document).ready(function () {
            alert();

            $("#btnSubmit").click(function (e) {
                e.preventDefault();

                if ($("#txtFirstName").val() == "") {
                    alert('First Name mandatory');
                }
            });

        });

    </script>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="border: 1px solid black; width: 100%; height: 100%; font-size: 20px; font-family: Calibri">
                <tr>
                    <td colspan="6" style="text-align: center; font-weight: bold; font-size: 50px; width: 100%; padding-bottom: 50px">Customer Information Form</td>
                </tr>
                <tr>
                    <td style="width: 20%">Name of Customer</td>
                    <td style="width: 20%">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="padding-right: 5px">
                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="20"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10%">PAN No.</td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtPanNo" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                    <td style="width: 20%">Address</td>
                    <td style="width: 10%">
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Pin Code</td>
                    <td>
                        <asp:TextBox ID="txtPinCode" runat="server" TextMode="Number" MaxLength="6"></asp:TextBox></td>
                    <td>City</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>State</td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Paid Fee</td>
                    <td>
                        <asp:RadioButtonList ID="rbFeeConfirm" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>Amount</td>
                    <td>
                        <asp:TextBox ID="nbxAmount" runat="server" TextMode="Number" MaxLength="10"></asp:TextBox></td>
                    <td>Date of Payment</td>
                    <td>
                        <asp:TextBox ID="dtbPayment" runat="server" TextMode="Date"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSrlNo" runat="server" TextMode="Number" Enabled="false" Visible="false" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right; padding-top: 30px">
                        <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text="Add" Style="font-weight: bold; font-size: medium; color: blue; border: solid" />
                        <asp:Button ID="btnEdit" OnClick="btnEdit_Click" runat="server" Text="Update" Style="font-weight: bold; font-size: medium; color: orange; border: solid" />
                        <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" Style="font-weight: bold; font-size: medium; color: red; border: solid" />
                    </td>
                    <td colspan="3" style="text-align: left; padding-top: 30px">
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Style="font-weight: bold; font-size: medium; color: green; border: solid" />
                        <%--<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" Style="font-weight: bold; font-size: medium; color: red; border: solid" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="grdCustomer" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged" AutoGenerateColumns="false" OnRowDataBound="grdCustomer_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="SrlNo" HeaderText="SrlNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# string.Format("{0} {1}", Eval("FirstName") ,Eval("LastName"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PanNo" HeaderText="Pan No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PinCode" HeaderText="Pin Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Paid Fee" InsertVisible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Visible="true" Checked='<%# Eval("PaidFee") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="DateofPayment" HeaderText="Date of Payment" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
