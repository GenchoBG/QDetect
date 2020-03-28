var valid = false;
var file = null;

$(document).ready(() => {
    $("#faceFile").change(function () {
        handleFileSelect();
    });

    $("#faceModal").on("hide.bs.modal", function () {
        $("#exampleModalCenterTitle").text("Results");
    });

    $("#faceForm").submit(function (e) {
        e.preventDefault();

        var banner = $("#banner");

        $("#posts").empty();
        $(".modal-body").children().hide();
        banner.show();

        $("#toggleResult").trigger("click");

        const facesApiUrl = "http://94.156.180.190:80/getembeddings";
        var formData = new FormData();
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
                    $("#exampleModalCenterTitle").text("ERROR");

                    console.log('No face found!');
                    banner.hide();
                    $("#nothingFound").show();
                } else if (embeddings.length !== 1) {
                    $("#exampleModalCenterTitle").text("ERROR");

                    console.log('More than one face found!');
                    banner.hide();
                    $("#moreThanOne").show();
                } else {
                    $.ajax({
                        method: "post",
                        url: '/Home/SearchFace',
                        data: {
                            embedding: embeddings[0]
                        },
                        success: function (posts) {
                            banner.hide();
                            if (posts.length === 0) {
                                $("#nothingFoundDb").show();
                            } else {
                                $("#successResult").show();
                                var postsTable = $("#posts");
                                var i = 1;
                                for (let post of posts) {
                                    postsTable.append($(`<tr>
                                                        <th scope="row">${i}</th>
                                                        <td><a href="${post.link}">${post.link}</a></td>
                                                    </tr>`));
                                    console.log(post.link);
                                    i++;
                                }
                            }

                        },
                        error: function (req, status, err) {
                            console.log("something went wrong");
                            console.log(status);
                            console.log(err);
                            console.log(req);
                        }
                    });

                    console.log('All is good! One face found');
                }

                console.log(embeddings);
            },
            error: function (req, status, err) {
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

function fileHandler() {
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
}

function handleFileSelect() {
    if (window.File && window.FileList && window.FileReader) {

        var files = event.target.files; //FileList object

        file = files[0];

        fileHandler();
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
    fileHandler();
}

function dragOverHandler(ev) {
    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
}