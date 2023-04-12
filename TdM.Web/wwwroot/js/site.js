// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



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

//froala editor HTML
var editor2 = new FroalaEditor('#biografia', {
    imageUploadURL: '/api/images'
});


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
if (uploadSymbol != null) {
    SymbolUploadElement.addEventListener('change', uploadSymbol);
}
//Sort table elements by column
function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("table");
    switching = true;
    //Set the sorting direction to ascending:
    dir = "asc";
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            /*check if the two rows should switch place,
            based on the direction, asc or desc:*/
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            //Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /*If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again.*/
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

