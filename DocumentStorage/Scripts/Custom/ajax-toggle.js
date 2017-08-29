$(document).ready(function () {
    $(document)
        .ajaxStart(function () {
        $(".body-content").find(".form-control, :submit").prop("disabled", true);
        })
        .ajaxStop(function () {
            $(".body-content").find(".form-control, :submit").prop("disabled", false);
        });
});