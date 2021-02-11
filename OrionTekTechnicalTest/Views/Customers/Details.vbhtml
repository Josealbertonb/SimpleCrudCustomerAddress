@ModelType OrionTekTechnicalTest.Customer
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FirstName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LastName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BrithDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BrithDate)
        </dd>

    </dl>
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
            @For Each item In Model.Customer_Address
                @<tr>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.Line1)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.Line2)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.City)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.Province)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.Country)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address.ZipCode)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Address_Type.Description)
                    </td>
                    <td style="visibility:hidden">
                        @Html.DisplayFor(Function(modelItem) item.Address_Id)
                    </td>
                    <td>
                        <input type="button" value="Remove" Class="btn btn-default" onclick="btnRemove(@item.Customer_Address_Id);" />
                    </td>
                </tr>
            Next
        </tbody>
    </table>
    <hr />
</div>
<div class="form-actions no-color">
    <input type="button" value="Cancelar" class="btn btn-default" onclick="location.href='@Url.Action("Index", "Customers")'" /> |
    <input type="button" value="Modificar" class="btn btn-primary" onclick="location.href='@Url.Action("Edit", "Customers", New With {.id = Model.Customer_Id})'" /> |
    <input type="button" value="Eliminar" class="btn btn-danger" onclick="location.href='@Url.Action("Delete", "Customers", New With {.id = Model.Customer_Id})'" /> 
</div>
