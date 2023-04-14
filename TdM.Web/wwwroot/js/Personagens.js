//Content image 

const ImageBoxUploadElement = document.getElementById('ImageBoxUpload');
const ImgBoxUrlElement = document.getElementById('ImgBoxUrl');
const ImageBoxDisplayElement = document.getElementById('ImageBoxUploadDisplay');
async function uploadBoxImage(e) {
    console.log(e.target.files[0]);

    let data = new FormData();
    data.append('file', e.target.files[0]);

    await fetch('/api/images', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
        },
        body: data
    }).then(response => response.json())
        .then(result => {
            ImgBoxUrlElement.value = result.link;
            ImageBoxDisplayElement.src = result.link;
            ImageBoxDisplayElement.style.display = 'block';
        });
}
if (uploadBoxImage != null) {
    ImageBoxUploadElement.addEventListener('change', uploadBoxImage);
}

//Card image

const ImageCardUploadElement = document.getElementById('ImageCardUpload');
const ImgCardUrlElement = document.getElementById('ImgCardUrl');
const ImageCardDisplayElement = document.getElementById('ImageCardUploadDisplay');
async function uploadCardImage(e) {
    console.log(e.target.files[0]);

    let data = new FormData();
    data.append('file', e.target.files[0]);

    await fetch('/api/images', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
        },
        body: data
    }).then(response => response.json())
        .then(result => {
            ImgCardUrlElement.value = result.link;
            ImageCardDisplayElement.src = result.link;
            ImageCardDisplayElement.style.display = 'block';
        });
}
if (uploadCardImage != null) {
    ImageCardUploadElement.addEventListener('change', uploadCardImage);
}


//GET LIST<CONTINENTES> BY MUNDO ID (NOT ALLOW MULTIPLE ITENS)
function updateContinentes() {
    var mundoId = $("#mundos").val();
    if (mundoId != "") {
        $.ajax({
            url: "/AdminContinentes/List",
            type: "POST",
            dataType: "json",
            data: { id: mundoId },
            success: function (data) {
                $("#SelectedContinentes").empty();
                $("#SelectedContinentes").append('<option value="" class="text-danger">No Continent</option>');
                $.each(data, function (index, value) {
                    $("#SelectedContinentes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });

                // Disable multiple selections
                $("#SelectedContinentes").on("change", function () {
                    var selectedOptions = $(this).find(":selected");
                    if (selectedOptions.length > 1) {
                        $(this).val(selectedOptions.eq(0).val());
                    }
                });
            }
        k
    }
}