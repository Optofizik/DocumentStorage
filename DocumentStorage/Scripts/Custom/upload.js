var upload = upload || {};

(function (obj) {
    obj.methods = {
        sendFile: function () {

            obj.props.uploadStatus.hide();
            var res = false;

            var formData = new FormData();
            var fileInput = obj.props.fileControl;
            var files = fileInput[0].files;

            for (var i = 0; i < files.length; i++) {
                formData.append("file_" + i, files[i]);
            }

            $.ajax({
                url: obj.urls.sendFileUrl,
                type: "POST",
                data: formData,
                cache: false,
                async: false,
                contentType: false,
                processData: false,
                success: (result) => {
                    res = true;
                },
                error: () => {
                    res = false;
                }
            });

            return res;
        },

        onFileChange: () => {
            var val = !obj.props.fileControl.val();
            obj.props.submitButton.prop("disabled", val);
        },

        clearForm: () => {
            obj.props.controls.val("");
        },

        showError: () => {
            obj.props.fileError.show();
            obj.props.fileControl.val("");
            obj.methods.onFileChange();
        },

        formSuccess: () => {
            obj.props.formSuccess.show();
            obj.methods.clearForm();
            obj.methods.onFileChange();
        }
    };

    obj.props = {
        get fileControl() {
            return $("#file");
        },

        get submitButton() {
            return $("#submit-btn");
        },

        get uploadForm() {
            return $("#upload-form");
        },

        get controls() {
            return $(".upload-control");
        },

        get fileError() {
            return $("#file-error");
        },

        get formSuccess() {
            return $("#form-success");
        },

        get uploadStatus() {
            return $(".upload-status");
        }
    };

    obj.urls = {
        get sendFileUrl() {
            return $("#hfUploadFileUrl").val();
        }
    }
})(upload);

$(document).ready(() => {
    upload.methods.onFileChange();
    upload.props.fileControl.on("change", upload.methods.onFileChange);
    upload.props.uploadForm.on("submit", () => {

        if (!upload.props.uploadForm.valid())
            return false;

        var res = upload.methods.sendFile();
        if (!res) {
            upload.methods.showError();
            return res;
        }
    });
});