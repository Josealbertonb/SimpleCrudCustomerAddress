@ModelType IEnumerable(Of SimpleCrud.Customer)
@Code
ViewData("Title") = "Index"
End Code

<h2>Customers</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BrithDate)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FirstName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.LastName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BrithDate)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Customer_Id }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Customer_Id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Customer_Id })
        </td>
    </tr>
Next

</table>
