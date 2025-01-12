// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Visar Bekräfta och avbryt knapparna när man vill ta bort meddelande
function showConfirmation(button) {
    button.style.display = 'none'; 
    button.nextElementSibling.style.display = 'block'; 
}

function hideConfirmation(button) {
    var confirmationButtons = button.parentElement; // Föräldern är 'confirmation-buttons'
    var deleteButton = confirmationButtons.previousElementSibling; // Föregående syskon är 'delete-button'
    confirmationButtons.style.display = 'none'; // Dölj bekräftelseknapparna
    deleteButton.style.display = 'block'; // Visa 'Ta bort'-knappen
}