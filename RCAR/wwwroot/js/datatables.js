$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#myTable tfoot th').each(function () {
        var title = $(this).text();
        if (title.trim() === 'Opcje')
            $(this).html('');
        else
            $(this).html('<input type="text" style="width: 100%;" class = "form-control" placeholder=' + title + ' />');
    });


    $('#myTable').DataTable( {
        "order": [[ 6, "desc" ]]
    });


});