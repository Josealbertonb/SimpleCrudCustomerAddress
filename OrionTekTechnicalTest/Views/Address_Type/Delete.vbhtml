@ModelType OrionTekTechnicalTest.Address_Type
@Code
    ViewData("Title") = "Delete"
End Code

    <h2>Delete Address Type</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Description)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Yes" class="btn btn-success" /> |
            <input type="button" value="No" class="btn btn-danger" onclick="location.href='@Url.Action("Index", "Address_Type")'" />
        </div>
    End Using
</div>
