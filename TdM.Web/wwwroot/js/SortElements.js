function sortTable(columnIndex) {
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("table");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[columnIndex];
            y = rows[i + 1].getElementsByTagName("TD")[columnIndex];
            if (columnIndex === 3) { // check if sorting by PublishedDate column
                x = Date.parse(rows[i].querySelector('.published-date').innerText.replace('T', ' ')); // convert datetime to Unix timestamp
                y = Date.parse(rows[i + 1].querySelector('.published-date').innerText.replace('T', ' ')); // convert datetime to Unix timestamp
            }
            else {
                x = x.innerText.toLowerCase();
                y = y.innerText.toLowerCase();
            }
            if (x > y) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

// this overrides `contains` to make it case insenstive
jQuery.expr[':'].contains = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

$('#search').keyup(function () {
    $('.card').removeClass('d-none');
    var filter = $(this).val(); // get the value of the input, which we filter on
    $('.card-deck').find('.card .card-body h3:not(:contains("' + filter + '"))').parent().parent().addClass('d-none');
})

$('#btnSort').click(function () {
    $('.card-deck .card').sort(function (a, b) {
        return $(a).find(".card-title").text() > $(b).find(".card-title").text() ? 1 : -1;
    }).appendTo(".card-deck");
})