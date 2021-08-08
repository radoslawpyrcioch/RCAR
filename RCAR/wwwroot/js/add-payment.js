function openModal () {
    $("#exampleModalCenter").modal('show');
}

function createService() {

    var data = $("#myform").serialize();

    $ajax({
        type: "post",
        url: "/service/create",
        data: data,
        success: function (response) {
            $("#loaderdiv").hide();
            alert("you are done");
        }
    })
}