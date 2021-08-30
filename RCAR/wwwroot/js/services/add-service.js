$(document).ready(function () {
    $("#btnOpen").click(function () {
        $("#myModal").modal('show');
    })

    $("#addService").click(function () {
        var data = $("#myform").serialize();
        $.ajax({
            type: "POST",
            url: "/Service/Create",
            data: data,
            success: function () {
                $('#myModal').modal('hide');
                location.reload();
            },
            error: function (response) {
                alert(response);
            }

        });
    })
})



