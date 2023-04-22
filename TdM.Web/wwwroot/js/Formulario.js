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
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminContinentes/ListContinentesbyMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedContinentes").empty();
                $("#SelectedContinentes").append('<option value="" class="text-danger" selected>No Continent</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedContinentes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
                // Clear selected region, character and creature if a different mundo is selected
                if ($("#SelectedMundo").data("selected") != selectedmundoId) {
                    $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>');
                    $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
                    $("#SelectedCriaturas").empty().append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
                }
                $("#SelectedMundo").data("selected", selectedmundoId);
            }
        });
    } else {
        // Clear contient, region, character and creature if no world is selected
        $("#SelectedContinentes").empty().append('<option value="" class="text-danger" selected>No Continent</option>'); // add empty default option
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
        $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
        $("#SelectedCriaturas").empty().append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
    }
}

//GET LIST<REGIOES> BY LIST<CONTINENTES> IDs (ALLOW MULTIPLE ITEMS)
function updateRegioes() {
    const selectedContinenteIds = $("#SelectedContinentes").val();
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
                // Clear selected character and creature if a diferent continente is selected
                if ($("#SelectedContinentes").data("selected") != selectedContinenteIds) {
                    $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
                    $("#SelectedCriaturas").empty().append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
                }
                $("#SelectedContinentes").data("selected", selectedContinenteIds);
            }
        });
    } else {
        // Reset the region dropdown
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

//GET LIST<PERSONAGENS> BY LIST<REGIOES> IDs (ALLOW MULTIPLE ITEMS)
function updatePersonagens() {
    const selectedRegiaoIds = $("#SelectedRegioes").val();
    if (selectedRegiaoIds != null && selectedRegiaoIds.length > 0) {
        $.ajax({
            url: "/AdminPersonagens/ListPersonagensByRegiao",
            type: "POST",
            dataType: "json",
            data: { selectedRegiaoIds: selectedRegiaoIds },
            success: function (data) {
                $("#SelectedPersonagens").empty();
                $("#SelectedPersonagens").append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedPersonagens").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Reset the character dropdown
        $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
    }
}

//GET LIST<CRIATURAS> BY LIST<REGIOES> IDs (ALLOW MULTIPLE ITEMS)
function updateCriaturas() {
    const selectedRegiaoIds = $("#SelectedRegioes").val();
    if (selectedRegiaoIds != null && selectedRegiaoIds.length > 0) {
        $.ajax({
            url: "/AdminCriaturas/ListCriaturasByRegiao",
            type: "POST",
            dataType: "json",
            data: { selectedRegiaoIds: selectedRegiaoIds },
            success: function (data) {
                $("#SelectedCriaturas").empty();
                $("#SelectedCriaturas").append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedCriaturas").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Reset the creature dropdown
        $("#SelectedCriaturas").empty().append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
    }
}

//GET LIST<POVOS> BY LIST<REGIOES> IDs (ALLOW MULTIPLE ITEMS)
function updatePovos() {
    const selectedRegiaoIds = $("#SelectedRegioes").val();
    if (selectedRegiaoIds != null && selectedRegiaoIds.length > 0 ) {
        $.ajax({
            url: "/AdminPovos/ListPovosByRegiao",
            type: "POST",
            dataType: "json",
            data: { selectedRegiaoIds: selectedRegiaoIds },
            success: function (data) {
                $("#SelectedPovos").empty();
                $("#SelectedPovos").append('<option value="" class="text-danger" selected>No People</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedPovos").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Reset the creature dropdown
        $("#SelectedPovos").empty().append('<option value="" class="text-danger" selected>No People</option>'); // add empty default option
    }
}