$(document).ready(function () {
    $('#myTable').DataTable({
        "order": [[6, "desc"]],
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