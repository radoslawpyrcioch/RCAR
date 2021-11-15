$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#myTable tfoot th').each(function () {
        var title = $(this).text();
        if (title.trim() === 'Opcje')
            $(this).html('');
        else
            $(this).html('<input type="text" style="width: 100%;" class = "form-control" placeholder=' + title + ' />');
    });


    $('#myTable').DataTable({
        "order": [[8, "asc"]],
        "language": {
            "lengthMenu": "Wyświetl _MENU_ pozycji na stronie",
            "zeroRecords": "Nic nie znaleziono - przykro nam",
            "info": "Strona _PAGE_ z _PAGES_",
            "infoEmpty": "Brak serwisów",
            "infoFiltered": "(wyfiltrowano z _MAX_ wszystkich pozycji)",
            "loadingRecords": "Wczytywanie...",
            "search": "Szukaj:",
            "paginate": {
                "first": "Pierwsza",
                "previous": "Poprzednia",
                "next": "Następna",
                "last": "Ostatnia"
            },
        }
    });


});