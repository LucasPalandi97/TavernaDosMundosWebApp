//TODAY DEFAULT DATETIME
const publishedDateInput = document.getElementById('publishedDate');
if (publishedDateInput) {
    const now = moment().tz('America/Sao_Paulo');
    const datetimeLocalString = now.format('YYYY-MM-DDTHH:mm');
    const datetimeFormatted = now.format('DD/MM/YYYY HH:mm');
    publishedDateInput.value = datetimeLocalString;
    publishedDateInput.setAttribute('title', datetimeFormatted);
} else {
    console.error('Could not find element with ID "publishedDate".');
}




//FROLADA EDITOR HTML
var editor = new FroalaEditor('#descricao', {
    imageUploadURL: '/api/images'
});

var editor2 = new FroalaEditor('#biografia', {
    imageUploadURL: '/api/images'
});


//IMAGE UPLOAD

// Content image 
const ImageBoxUploadElement = document.getElementById('ImageBoxUpload');
const ImgBoxUrlElement = document.getElementById('ImgBoxUrl');
const ImageBoxDisplayElement = document.getElementById('ImageBoxUploadDisplay');

// Card image
const ImageCardUploadElement = document.getElementById('ImageCardUpload');
const ImgCardUrlElement = document.getElementById('ImgCardUrl');
const ImageCardDisplayElement = document.getElementById('ImageCardUploadDisplay');

// Symbol image for regions
const SymbolUploadElement = document.getElementById('SymbolUpload');
const SymbolUrlElement = document.getElementById('SymbolUrl');
const SymbolDisplayElement = document.getElementById('SymbolUploadDisplay');
// Function for uploading an image

async function uploadImage(uploadElement, urlElement, displayElement) {
    console.log(uploadElement.files[0]);

    let data = new FormData();
    data.append('file', uploadElement.files[0]);

    await fetch('/api/images', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
        },
        body: data
    }).then(response => response.json())
        .then(result => {
            urlElement.value = result.link;
            displayElement.src = result.link;
            displayElement.style.display = 'block';
        });
}
// Add event listeners to each upload element
if (ImageBoxUploadElement != null) {
    ImageBoxUploadElement.addEventListener('change', () => {
        uploadImage(ImageBoxUploadElement, ImgBoxUrlElement, ImageBoxDisplayElement);
    });
}

if (ImageCardUploadElement != null) {
    ImageCardUploadElement.addEventListener('change', () => {
        uploadImage(ImageCardUploadElement, ImgCardUrlElement, ImageCardDisplayElement);
    });
}

if (SymbolUploadElement != null) {
    SymbolUploadElement.addEventListener('change', () => {
        uploadImage(SymbolUploadElement, SymbolUrlElement, SymbolDisplayElement);
    });
}

//SELECT MENUS

//GET LIST<CONTINENTES> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateContinentes() {
    var mundoId = $("#SelectedMundo").val();
    if (mundoId != "") {
        $.ajax({
            url: "/AdminContinentes/ListContinentesbyMundo",
            type: "POST",
            dataType: "json",
            data: { id: mundoId },
            success: function (data) {
                $("#SelectedContinentes").empty();
                $("#SelectedContinentes").append('<option value="" class="text-danger" selected>No Continent</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedContinentes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
                // Clear selected region if a different mundo is selected
                if ($("#SelectedMundo").data("selected") != mundoId) {
                    $("#SelectedRegioes").empty();
                    $("#SelectedRegioes").append('<option value="" class="text-danger" selected>No Region</option>');
                }

                $("#SelectedMundo").data("selected", mundoId);
            }
        });
    } else {
        // Clear both continent and region if no mundo is selected
        $("#SelectedContinentes").empty();
        $("#SelectedContinentes").append('<option value="" class="text-danger" selected>No Continent</option>'); // add empty default option
        $("#SelectedRegioes").empty();
        $("#SelectedRegioes").append('<option value="" class="text-danger" selected>No Region</option>');
    }
}

//GET LIST<REGIOES> BY LIST<CONTINENTES> IDs (ALLOW MULTIPLE ITEMS)
function updateRegioes() {
    var selectedContinenteIds = $("#SelectedContinentes").val();
    if (selectedContinenteIds != null && selectedContinenteIds.length > 0) {
        $.ajax({
            url: "/AdminRegioes/ListRegioesByContinente",
            type: "POST",
            dataType: "json",
            data: { selectedContinenteIds: selectedContinenteIds },
            success: function (data) {
                $("#SelectedRegioes").empty();
                $("#SelectedRegioes").append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedRegioes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Reset the region dropdown
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

