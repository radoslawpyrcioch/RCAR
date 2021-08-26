$(document).ready(function () {
    $(".sidebar-btn").click(function () {
        $(".wrapper").toggleClass("collapse");
    });
});

function menuToggle() {
    const toggleMenu = document.querySelector('.menu');
    toggleMenu.classList.toggle('active');
}