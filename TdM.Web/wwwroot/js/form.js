

//Set default date to today
Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});
document.getElementById('publishedDate').value = new Date().toDateInputValue();


//froala editor HTML
var editor = new FroalaEditor('#descricao', {
    imageUploadURL: '/api/images'
});

var text = $("#descricao").val();
$("#DetailDesc").val(text);
$('#save').on('click', function () {
    var text = $("#DetailDesc").val();
    $("#descricao").val(text);
});


//Symbol image for regions 
const SymbolUploadElement = document.getElementById('SymbolUpload');
const SymbolUrlElement = document.getElementById('SymbolUrl');
const SymbolDisplayElement = document.getElementById('SymbolUploadDisplay');
async function uploadSymbol(e) {
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
            SymbolUrlElement.value = result.link;
            SymbolDisplayElement.src = result.link;
            SymbolDisplayElement.style.display = 'block';
        });
}

SymbolUploadElement.addEventListener('change', uploadSymbol);


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

ImageCardUploadElement.addEventListener('change', uploadCardImage);


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

ImageBoxUploadElement.addEventListener('change', uploadBoxImage);


