$(document).ready(() => {
    if ($(".carousel-item").length === 0) {
        clearNodes();
    }
});

function clearNodes() {
    $(".bd-carousel").children().remove();
    $("#controls").remove();
    $(".bd-carousel").append($("<h1 class='text-center'>There are no reports for this person!</h1>"));
}

function deleteReport(id) {
    console.log(id);
    console.log("Deleting...");

    $.ajax({
        method: "post",
        url: '/Report/Delete?id=' + id,
        processData: false,
        contentType: false,
        cache: false,
        success: function () {
            console.log("SUCCESS");
            if ($(".carousel-item").length > 0) {
                let deleteIndicator = $(`#carousel-item-${id}`).find($("#indicatorId"))[0].innerText;
                $(`carousel-indicator-${deleteIndicator}`).remove();
                $(`#carousel-item-${id}`).remove();
                $($(".carousel-item")[0]).addClass("active");
            } else {
                clearNodes();
            }

            if ($(".carousel-item").length === 1) {
                $(".carousel-control-prev").remove();
                $(".carousel-control-next").remove();
            }
        },
        error: function (req, status, err) {
            console.log(status);
            console.log(err);
            console.log(req);
            console.log(req.responseText);
        }
    });
}

function archiveReport(id) {
    console.log("Archiving...");
    $.ajax({
        method: "post",
        url: '/Report/Archive?id=' + id,
        processData: false,
        contentType: false,
        cache: false,
        success: function () {
            console.log("SUCCESS");
            if ($(".carousel-item").length > 0) {
                $(`#carousel-item-${id}`).remove();
                $($(".carousel-item")[0]).addClass("active");
            } else {
                clearNodes();
            }
        },
        error: function (req, status, err) {
            console.log(status);
            console.log(err);
            console.log(req);
            console.log(req.responseText);
        }
    });
}