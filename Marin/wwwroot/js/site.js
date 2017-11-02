const apiUrl = window.location.origin + "/api/";
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
function CreateErrorModal(errorResponseText) {
    /// <summary>Create a bootbox red triangle warning modal when api call resulted in error</summary>
    var msg =
        '<div class="row text-center"><i class="fa fa-exclamation-triangle fa-5x text-danger"></i></div><div class="row text-center"> Något gick fel: ' +
            message +
            '</div>';
    bootbox.alert({
        title: "Varning",
        message: msg,
        buttons: {
            ok: {
                label: "OK",
                className: "btn btn-primary"
            }
        }
    });

}
