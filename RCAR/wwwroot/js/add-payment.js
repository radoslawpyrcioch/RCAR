function AddPaymentToTable(paymentRecordId) {
    var result = confirm("Czy na pewno chcesz dodać ten rekord?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "POST",
            url: "/PaymentRecord/Create",
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