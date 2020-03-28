var valid = false;
var file = null;

var embedding_values = null;

$(document).ready(() => {
    $("#faceFile").change(function (e) {
        handleFileSelect(e);
    });

    $("#faceForm").submit((e) => {
        e.preventDefault();

        let formData = new FormData();
        formData.append("embedding", embedding_values[0]);
        console.log(embedding_values[0]);
        formData.append("name", $("#fullName").val());
        console.log($("#fullName").val());
        formData.append("city", $("#city").val());
        console.log($("#city").val());
        formData.append("ucn", $("#ucn").val());
        console.log($("#ucn").val());
        formData.append("image", file);
        console.log(file);
        formData.append("quarantine", $("#quarantine").val());
        console.log($("#quarantine").val());


        console.log("Sending");
        $.ajax({
            method: "post",
            url: '/Person/Upload',
            data: formData,
            processData: false,
            success: function() {
                console.log("SUCCESS");
            },
            error: function(req, status, err) {
                console.log("something went wrong");
                console.log(status);
                console.log(err);
                console.log(req);
            }
        });
    });
});

function isValid() {
    //Only pics
    if (!file.type.match('image')) {
        console.log("invalid type");

        $("#toggleResult").trigger("click");

        $("#posts").empty();
        $(".modal-body").children().hide();

        $("#exampleModalCenterTitle").text("ERROR");

        $("#error").show();

        return false;
    }
    return true;
}

function fileHandler(e) {
    valid = isValid(file);
    if (!valid) {
        return;
    }

    //Check File API support
    if ($(".thumbnail")) {
        $(".thumbnail").remove();
    }

    if (file.name.length > 25) {
        $("#faceFileLabel").text(file.name.slice(0, 10) + '...' + file.name.split('.')[1]);
    } else {
        $("#faceFileLabel").text(file.name);
    }

    var picReader = new FileReader();
    picReader.addEventListener("load",
        function (event) {
            var picFile = event.target;
            var div = $("#form-header-content");
            var image = $("<img class='thumbnail' alt='pic' src='" +
                picFile.result +
                "'" +
                "title='" +
                file.name +
                "'/>");

            image.ready(function (parameters) {
                div.append(image);

                var box = $("#form-header");

                box.animate({ height: "+=" + image.height + "px" }, 1000);
            });
        });

    //Read the image
    picReader.readAsDataURL(file);

    checkPicture(e);
}

function handleFileSelect(e) {
    if (window.File && window.FileList && window.FileReader) {

        var files = event.target.files; //FileList object

        file = files[0];

        fileHandler(e);
    } else {
        console.log("Your browser does not support File API");
    }
}

function dropHandler(ev) {
    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();

    if (ev.dataTransfer.items) {
        // Use DataTransferItemList interface to access the file(s)
        for (let i = 0; i < ev.dataTransfer.items.length; i++) {
            // If dropped items aren't files, reject them
            if (ev.dataTransfer.items[i].kind === 'file') {
                file = ev.dataTransfer.items[i].getAsFile();
                console.log('... items: file[' + i + '].name = ' + file.name);
            }
        }
    }
    fileHandler(ev);
}

function checkPicture(e) {
    e.preventDefault();

    const facesApiUrl = "http://94.156.180.190:80/getembeddings";
    let formData = new FormData();
    formData.append('face', file);
    console.log("FACE: ", file);

    if (!valid) {
        return;
    }

    $.ajax({
        url: facesApiUrl,
        method: "post",
        data: formData,
        contentType: false,
        processData: false,
        crossDomain: true,
        cache: false,
        success: function (embeddings) {
            if (embeddings.length === 0) {
                $("#uploadInfo").text("No face found");

                console.log('No face found!');
            } else if (embeddings.length !== 1) {
                $("#uploadInfo").text("More than one face found");

                console.log('More than one face found!');
            } else {
                $("#uploadInfo").text("Everything's perfect");
                embedding_values = embeddings;
            }

            console.log(embeddings);
        },
        error: function (req, status, err) {
            $("#uploadInfo").text("Something went wrong");
            console.log("something went wrong");
            console.log(status);
            console.log(err);
            console.log(req);
        }
    });
}

function dragOverHandler(ev) {
    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
}