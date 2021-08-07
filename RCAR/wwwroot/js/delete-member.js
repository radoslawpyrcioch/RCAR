function RemoveFromTable(memberId) {
    var result = confirm("Czy na pewno chcesz usuńąć ten rekord?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "GET",
            url: "/Member/Delete",
            data: { id: memberId },
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