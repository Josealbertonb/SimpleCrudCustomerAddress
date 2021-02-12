@ModelType SimpleCrud.Address_Type
@Code
    ViewData("Title") = "Create"
End Code

    <h2>Create Type of Address</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-group">
        @Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
    <div class="form-row">
        <div Class="form-group">
            <div Class="col-md-10">
                <input type="submit" value="Create" class="btn btn-success" />
                <input type="button" value="Cancelar" class="btn btn-primary" onclick="location.href='@Url.Action("Index", "Address_Type")'" />
            </div>
        </div>
    </div>
</div>
End Using

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
