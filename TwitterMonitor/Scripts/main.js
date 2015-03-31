function adjustStyle(width) {
    width = parseInt(width);
    if (width < 800) {
        $("#size-stylesheet").attr("href", "~/Content/smallscreen.css");
    } else {
        $("#size-stylesheet").attr("href", "~/Content/bigscreen.css");
    }
};

$(function () {
    adjustStyle($(this).width());
    $(window).resize(function () {
        adjustStyle($(this).width());
    });
});