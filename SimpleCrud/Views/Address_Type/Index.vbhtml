@ModelType IEnumerable(Of SimpleCrud.Address_Type)
@Code
ViewData("Title") = "Index"
End Code

<h2>Address Types</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Description)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Address_Type_Id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Address_Type_Id})
        </td>
    </tr>
Next

</table>
