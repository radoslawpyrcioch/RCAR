$(document).ready(function () {  
    $("select[name=exportService]").change(function () {
        $("#myFormExcel").submit();
    })
});