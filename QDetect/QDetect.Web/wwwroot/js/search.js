function openModal(person) {
    console.log(person);
    $("#modalCenterTitle").text(person.name);
    person.hasReports ? $("#modalReported").text("!") : $("#modalReported").css("display", "none");
    $("#modalImage").attr("src", person.image);
    $("#tableName").text(person.name);
    $("#tableCity").text(person.city);
    $("#tableQuarantine").text(person.quarantineEndDate);
    $("#tableReported").text(person.hasReports ? "Yes" : "No");
}

$(document).ready(() => {
    $('body').on('click', '[data-editable]', function () {

        var el = $(this);
        console.log($(this));
        var id = $(this)[0].id;
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