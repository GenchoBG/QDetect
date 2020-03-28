$(document).ready(() => {
    $('body').on('click', '[data-editable]', function () {

        var el = $(this);
        let id = $(this)[0].id;
        var type = "";
        var date = null;
        if (id === "tableQuarantine") {
            type = "date";
            date = $(this)[0].innerText;
        } else {
            type = "text";
        }

        var input = $(`<input type=${type} id="${id}"/>`).val(el.text());
        el.replaceWith(input);

        var save = function () {
            var p = $(`<p data-editable id="${id}"/>`).text(date ? input.val() ? input.val() : date : input.val());
            input.replaceWith(p);
        };

        /**
          We're defining the callback with `one`, because we know that
          the element will be gone just after that, and we don't want
          any callbacks leftovers take memory.
          Next time `p` turns into `input` this single callback
          will be applied again.
        */
        input.one('blur', save).focus();

    });
});

function openModal(person) {
    $("#modalCenterTitle").text(person.name);
    $("#modalPersonId").text(person.id);
    person.hasReports ? $("#modalReported").text("!") : $("#modalReported").css("display", "none");
    $("#modalImage").attr("src", person.image);
    $("#tableName").text(person.name);
    $("#tableUCN").text(person.ucn);
    $("#tableCity").text(person.city);
    $("#tableQuarantine").text(person.quarantineEndDate);
    $("#tableReported").text(person.hasReports ? "Yes" : "No");
}

function saveInfo() {
    let formData = new FormData();
    let id = $("#modalPersonId").text();

    formData.append("name", $("#tableName").text());
    formData.append("city", $("#tableCity").text());
    formData.append("ucn", $("#tableUCN").text());
    formData.append("quarantineEndDate", $("#tableQuarantine").text());
    formData.append("id", id);

    console.log("Sending");
    $.ajax({
        method: "post",
        url: '/Person/Edit',
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        success: function () {
            console.log("SUCCESS");
            $("#modal").modal('toggle');

            $.ajax({
                method: "get",
                url: `/Person/Info?Id=${id}`,
                processData: false,
                contentType: false,
                cache: false,
                success: function (person) {
                    $(`#table-tr-${id}`).find("#personName").text(person.name);
                    $(`#table-tr-${id}`).find("#personUCN").text(person.ucn);
                    $(`#table-tr-${id}`).find("#personCity").text(person.city);
                    $(`#table-tr-${id}`).find("#personQuarantine").text(person.quarantineEndDate);
                },
                error: function (req, status, err) {
                    console.log(status);
                    console.log(err);
                    console.log(req);
                    console.log(req.responseText);
                }
            });
        },
        error: function (req, status, err) {
            console.log(status);
            console.log(err);
            console.log(req);
            console.log(req.responseText);
        }
    });
}