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
        }, 10000);
    } else {
        validationSummary.style.display = "none"; // Mantenerlo oculto si está vacío
    }

});

document.getElementById('descripcionTextarea').addEventListener('keydown', function (e) {
    if (e.key === 'Tab') {
        e.preventDefault(); // Evita que el tab cambie el foco
        const textarea = e.target;
        const start = textarea.selectionStart;
        const end = textarea.selectionEnd;

        // Inserta 4 espacios en la posición del cursor
        textarea.value = textarea.value.substring(0, start) + '    ' + textarea.value.substring(end);

        // Mueve el cursor después de los espacios insertados
        textarea.selectionStart = textarea.selectionEnd = start + 4;
    }
});

$(document).ready(function () {
    if ($("#message").length) {
        setTimeout(function () {
            $("#message").fadeOut("slow");
        }, 10000); // 10 segundos
    }
});