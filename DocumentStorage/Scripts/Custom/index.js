var index = index || {};

(function (obj) {

    obj.methods = {
        deleteFile: function () {

            var btn = $(this);

            btn.prop("disabled", true);

            var id = btn.attr("data-id");

            $.ajax({
                url: obj.urls.deleteFileUrl,
                type: "POST",
                data: { id: id },
                success: function () {
                    obj.methods.removeHTML(btn);
                }
            })
        },
        removeHTML: function (elem) {
            var li = $(elem).parents("li")[0];

            if ($(li).siblings().length === 0) {
                var liId = $(li).parents(".tab-pane").attr("id");

                var link = obj.props.navs.find("a").filter(function () {
                    return $(this).attr("href") === ("#" + liId);
                })

                link.parents("li").remove();
                $(li).parents(".tab-pane").remove();
                $(obj.props.navs.get(0)).addClass("active in");
                $(obj.props.tabs.get(0)).addClass("active in");
            }

            li.remove();            
        }
    };

    obj.props = {

        get mainContainer() {
            return $(".body-content");
        },

        get navs() {
            return $(".nav.nav-tabs");
        },

        get tabs() {
            return $(".tab-pane.fade");
        }
    };

    obj.urls = {
        get deleteFileUrl() {
            return $("#hfDelFile").val();
        }
    };
})(index);

$(document).ready(function () {
    index.props.mainContainer.on("click", ".btn-remove", index.methods.deleteFile);
});