function RemoveFromTable(carId) {
    var result = confirm("Czy na pewno chcesz usuńąć ten rekord?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "GET",
            url: "/Car/Delete",
            data: { id: carId },
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function () {
                $("#row_" + carId).remove();
            },
            error: function (response) {
                alert(response);
            }
        })
    }
}