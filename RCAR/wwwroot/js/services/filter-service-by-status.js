$(document).ready(function () {
    $("select[name=filterService]").change(function () {
        $("#myForm").submit();
    })
});