function RemoveFromTable(paymentRecordId) {
    var result = confirm("Czy na pewno chcesz usuńąć ten rekord?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "GET",
            url: "/PaymentRecord/Delete",
            data: { id: paymentRecordId },
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function () {
                location.reload(true);
            },
            error: function (response) {
                alert(response);
            }
        })
    }
}