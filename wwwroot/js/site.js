// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", function () {
    var validationSummary = document.querySelector(".alert-float");
    // Verifica si hay mensajes de error reales dentro del div
    // Verifica si hay contenido que no sean espacios vacíos
    if (validationSummary && validationSummary.textContent.trim().length > 0) {
        validationSummary.style.display = "block";

        // Ocultar automáticamente después de 5 segundos
        setTimeout(function () {
            validationSummary.style.display = "none";
        }, 5000);
    } else {
        validationSummary.style.display = "none"; // Mantenerlo oculto si está vacío
    }

});
