var activeConfirm = function (controllerName,id) {
    $('#active-conformation').modal('show');
    $('#activeValueId').text(id);
    $('#activeControllerName').text(controllerName);
}

var active = function () {
    $('#divLoading').show();
    var id = $('#activeValueId').text();
    var controllerName = $('#activeControllerName').text();
    $.ajax({
        type: "POST",
        url: 'https://localhost:44395/Active/Active/',
        data:{controllerName: controllerName,
              id: id,
              state: true
        },
        success: function () {
            $("#active-conformation").modal('hide');
            document.location.reload();
        },
        error: function () {
            $("#active-conformation").modal('hide');
        }
    });
}

var deactivateConfirm = function (controllerName,id) {
    $('#deactivate-conformation').modal('show');
    $('#deactivateValueId').text(id);
    $('#deactivateControllerName').text(controllerName);
}

var deactivate = function () {
    $('#divLoading').show();
    var id = $('#deactivateValueId').text();
    var controllerName = $('#deactivateControllerName').text();
    $.ajax({
        type: "POST",
        url: 'https://localhost:44395/Active/Active/',
        data:{controllerName: controllerName,
            id: id,
            state: false
        },
        success: function () {
            $("#deactivate-conformation").modal('hide');
            document.location.reload();
        },
        error: function () {
            $("#deactivate-conformation").modal('hide');
        }
    });
}