$(document).ready(() => {
    if ($(".carousel-item").length === 0) {
        clearNodes();
    }

    $("button").on("click", () => {
        if ($(".carousel-item").length === 0) {
            clearNodes();
        }
    });
});

function clearNodes() {
    $(".bd-carousel").empty();
    $("#controls").remove();
    $(".bd-carousel").append($("<h1 class='text-center'>There are no reports for this person!</h1>"));
}

function deleteReport(id) {
    $.ajax({
        method: "post",
        url: '/Report/Delete?id=' + id,
        processData: false,
        contentType: false,
        cache: false,
        success: function () {
            if ($(".carousel-inner").children().length > 1) {
                let deleteIndicator = $(".carousel-inner").children().index($(`#carousel-item-${id}`));
                console.log(deleteIndicator);
                $(".carousel-indicators").children()[deleteIndicator].remove();

                console.log($(`#carousel-item-${id}`));
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
    $.ajax({
        method: "post",
        url: '/Report/Archive?id=' + id,
        processData: false,
        contentType: false,
        cache: false,
        success: function () {
            if ($(".carousel-item").length > 0) {
                let deleteIndicator = $(".carousel-inner").children().index($(`#carousel-item-${id}`));
                console.log(deleteIndicator);
                $(".carousel-indicators").children()[deleteIndicator].remove();

                console.log($(`#carousel-item-${id}`));
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