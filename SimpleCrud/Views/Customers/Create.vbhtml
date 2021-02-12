@ModelType SimpleCrud.Customer
@Code
    ViewData("Title") = "Create"
End Code

<h2>Create Customer</h2>

@Using (Html.BeginForm())

    @<div class="form-horizontal">
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-row">
        <div class="form-group col-md-6">
            @Html.LabelFor(Function(model) model.FirstName, "First Name", htmlAttributes:=New With {.class = "control-label"})
            <div>
                @Html.EditorFor(Function(model) model.FirstName, New With {.htmlAttributes = New With {.class = "form-control", .placeHolder = "Required"}})
                @Html.ValidationMessageFor(Function(model) model.FirstName, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="form-group col-md-6">
            @Html.LabelFor(Function(model) model.LastName, "Last Name", htmlAttributes:=New With {.class = "control-label"})
            <div>
                @Html.EditorFor(Function(model) model.LastName, New With {.htmlAttributes = New With {.class = "form-control", .placeHolder = "Required"}})
                @Html.ValidationMessageFor(Function(model) model.LastName, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-12">
            @Html.LabelFor(Function(model) model.BrithDate, "Birth Date", htmlAttributes:=New With {.class = "control-label"})
            <div>
                @Html.EditorFor(Function(model) model.BrithDate, New With {.htmlAttributes = New With {.class = "form-control", .placeHolder = "MM-DD-YYYY"}})
                @Html.ValidationMessageFor(Function(model) model.BrithDate, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>
    <div class="form-row">
        <b>Address:</b>
        <table id="tblAddresses" Class="table" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th>
                        Line 1
                    </th>
                    <th>
                        Line 2
                    </th>
                    <th>
                        City
                    </th>
                    <th>
                        Province
                    </th>
                    <th>
                        Country
                    </th>
                    <th>
                        Zip Code
                    </th>
                    <th>
                        Type
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
            </tbody>
            <tfoot>
                <tr>
                    <td><input type="text" id="txtLine1" class="form-control" placeholder="Required" /></td>
                    <td><input type="text" id="txtLine2" class="form-control" /></td>
                    <td><input type="text" id="txtCity" class="form-control" placeholder="Required" /></td>
                    <td><input type="text" id="txtProvince" class="form-control" placeholder="Required" /></td>
                    <td><input type="text" id="txtCountry" class="form-control" placeholder="Required" /></td>
                    <td><input type="text" id="txtZipCode" class="form-control" placeholder="Required" /></td>
                    <td>@Html.DropDownList("Address_Type_Id", CType(ViewBag.AddressTypes, IEnumerable(Of SelectListItem)), New With {.class = "form-control", .style = "width: auto;"})</td>
                    <td></td>
                </tr>
            </tfoot>

        </table>
        <hr />
    </div>

    <div class="form-row">
        <div Class="form-group">
            <div Class="col-md-10">
                <input type="button" value="Add Address" Class="btn btn-default" onclick="btnAddAdress();" />
                <input type="button" value="Create Customer" Class="btn btn-success" onclick="btnSave();" />
                <input type="button" value="Cancelar" class="btn btn-primary" onclick="location.href='@Url.Action("Index", "Customers")'" />
            </div>
        </div>
    </div>

</div>

End Using

@Section Scripts

    @Scripts.Render("~/bundles/jqueryval")
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.cdnjs.com/ajax/libs/json2/20110223/json2.js"></script>

    <script type="text/javascript">
        function btnAddAdress() {
            //Reference of the cell.
            var txtLine1 = $("#txtLine1");
            var txtLine2 = $("#txtLine2");
            var txtCity = $("#txtCity");
            var txtProvince = $("#txtProvince");
            var txtCountry = $("#txtCountry");
            var txtZipCode = $("#txtZipCode");
            var txtAddressType = $("#Address_Type_Id");

            if (txtLine1.val() == "" || txtCity.val() == "" || txtProvince.val() == "" || txtCountry.val() == "" || txtZipCode.val() == "") {
                alert("All required fields must be filled out");
                return false;
            }

            //Temp code | REFACTOR HERE
            var AddressType = getNameById(txtAddressType.val())


            //Reference of TBODY element.
            var tBody = $("#tblAddresses > TBODY")[0];

            //Add Row.
            var row = tBody.insertRow(-1);

            //Add cells.
            var cell = $(row.insertCell(-1));
            cell.html(txtLine1.val());
            cell = $(row.insertCell(-1));
            cell.html(txtLine2.val());
            cell = $(row.insertCell(-1));
            cell.html(txtCity.val());
            cell = $(row.insertCell(-1));
            cell.html(txtProvince.val());
            cell = $(row.insertCell(-1));
            cell.html(txtCountry.val());
            cell = $(row.insertCell(-1));
            cell.html(txtZipCode.val());
            cell = $(row.insertCell(-1));
            cell.html(AddressType);

            //Add Button.
            cell = $(row.insertCell(-1));
            var btnRemove = $("<input />");
            btnRemove.attr("type", "button");
            btnRemove.attr("class", "form-control");
            btnRemove.attr("onclick", "Remove(this);");
            btnRemove.val("Remove");
            cell.append(btnRemove);

            //Clear the TextBoxes.
            txtLine1.val("");
            txtLine2.val("");
            txtCity.val("");
            txtProvince.val("");
            txtCountry.val("");
            txtZipCode.val("");
        };

        function Remove(button) {
            //Reference of the Row.
            var row = $(button).closest("TR");
            var name = $("TD", row).eq(0).html();
            if (confirm("Do you really want to delete this address: " + name)) {
                var table = $("#tblAddresses")[0];
                table.deleteRow(row[0].rowIndex);
            }
        };

        function btnSave() {
            //Loop through the Table rows and build a JSON array.
            var addresses = new Array();
            $("#tblAddresses TBODY TR").each(function () {
                var row = $(this);
                var address = {};
                address.Line1 = row.find("TD").eq(0).html();
                address.Line2 = row.find("TD").eq(1).html();
                address.City = row.find("TD").eq(2).html();
                address.Province = row.find("TD").eq(3).html();
                address.Country = row.find("TD").eq(4).html();
                address.ZipCode = row.find("TD").eq(5).html();
                //Temp code | REFACTOR HERE
                address.Address_Type_Id = getIdByName(row.find("TD").eq(6).html());
                addresses.push(address);
            });

            if (!addresses.length > 0) {
                alert("At least one address must be added");
                return false;
            }

            var customer = {};
            customer.FirstName = document.getElementById('FirstName').value;
            customer.LastName = document.getElementById('LastName').value;
            customer.BrithDate = document.getElementById('BrithDate').value;
            customer.Addresses = addresses;
            //Send the JSON array to Controller.
            $.ajax({
                type: "POST",
                url: "/Customers/Create",
                data: JSON.stringify(customer),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                crossDomain: true,
                success: function (url) {
                    window.location.href = url;
                }
            });
        };

        function getNameById(txtAddressType) {
            var AddressType = "";
            if (txtAddressType == "1") {
                AddressType = "Work";
            }
            else {
                AddressType = "Home";
            }
            return AddressType;
        }

        function getIdByName(AddressTypeName) {
            var AddressTypeId;
            if (AddressTypeName == "Work") {
                AddressTypeId = "1";
            }
            else {
                AddressTypeId = "2";
            }
            return AddressTypeId;
        }
    </script>
End Section
