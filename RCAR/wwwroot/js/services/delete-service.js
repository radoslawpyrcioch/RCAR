function RemoveFromTable(serviceId) {
    var result = confirm("Czy na pewno chcesz zakończyć ten serwis?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "GET",
            url: "/Service/ChangeStatus",
            data: { id: serviceId },
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function () {

                $("#row_" + serviceId).remove();

                location.reload(true);
            },
            error: function (response) {
                alert(response);
            }
        })
    }
}