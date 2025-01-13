// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Visar Bekräfta och avbryt knapparna när man vill ta bort meddelande döljer dem sen när man klickar avbryt
function showConfirmation(button) {
    document.querySelector('.delete-button').style.display = 'none';
    document.querySelector('.confirmation-buttons').style.display = 'block';
}

function hideConfirmation(button) {
    document.querySelector('.confirmation-buttons').style.display = 'none';
    document.querySelector('.delete-button').style.display = 'block';
}