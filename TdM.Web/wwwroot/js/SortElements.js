﻿//FILTER CARDS
//FILTER CARDS
var searchBox = document.getElementById('search-box');
var cards = document.querySelectorAll('#charactercard');

searchBox.addEventListener('input', function () {
    var searchText = searchBox.value.trim().toLowerCase();

    cards.forEach(function (card) {
        var title = card.querySelector('.card-title').textContent.trim().toLowerCase();

        if (title.includes(searchText)) {
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
            card.style.display = 'block';
        } else {
            card.style.opacity = '0';
            card.style.transform = 'translateY(10%)';
            card.addEventListener('transitionend', function () {
                if (card.style.opacity === '0') {
                    card.style.display = 'none';
                }
            }, { once: true });
        }
    });
});

cards.forEach(function (card) {
    card.addEventListener('mouseover', function () {
        if (card.style.display === 'block') {
  
        }
    });

    card.addEventListener('mouseout', function () {
        if (card.style.display === 'block') {
     
        }
    });
});


//FILTER TABLE FOR ADMINS
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


  