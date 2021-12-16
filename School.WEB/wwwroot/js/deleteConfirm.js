var deleteConfirm = function (name,id) {
    $('#deletedValueName').text(name);
    $('#delete-conformation').modal('show');
    $('#deletedValueId').text(id);
}
//call this function after click on confirm delete button
var deleteData = function () {
    $('#divLoading').show();
    var id = $('#deletedValueId').text();
    $.ajax({
        type: "GET",
        url: `${action}/${id}`,
        data:{id: id},
        success: function (result) {
            $("#delete-conformation").modal('hide');
            document.location.reload();
        },
        error: function () {
            $("#delete-conformation").modal('hide');
        }
    });
}