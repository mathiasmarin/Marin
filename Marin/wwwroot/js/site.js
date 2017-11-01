const apiUrl = window.location.origin + "/api/";
bootbox.setDefaults({
    locale: "sv",
    backdrop: "static", // Disables close modal on click-outside. 
});
function CreateConfirmModal(message) {
    /// <summary>Create a bootbox green check modal with a input message.</summary>
    var msg =
        '<div class="row text-center"><i class="fa fa-check fa-5x primary-green"></i></div><div class="row text-center">' +
            message +
            '</div>';
    bootbox.alert({
        title: "Resultat",
        message: msg,
        buttons: {
            ok: {
                label: "OK",
                className: "btn btn-primary"
            }
        }
    });

}
