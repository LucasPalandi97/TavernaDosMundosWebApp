﻿
//Toasts
const toastElList = document.querySelectorAll('.toast')
const toastList = [...toastElList].map(toastEl => new bootstrap.Toast(toastEl, option))

//Tooltips
const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

//Popovers
$(document).ready(function () {
    $('[data-bs-toggle="popover"]').popover();
});

const themeSwitch = document.getElementById('themeSwitch');

//Eliminate padding left when mobile
if (/Mobi/.test(navigator.userAgent)) {
    document.querySelector('main[role="main"]').style.paddingLeft = 0;
}

//Disable the scrolbar when the collapse menu is active
$(document).ready(function () {
    $('.navbar-toggler').click(function () {
        $('body').toggleClass('collapse-active');
    });
});

//Color theme
// Get stored theme or null
function getTheme() {
    return localStorage.getItem('theme');
}

// Set theme and store in local storage
function setTheme(theme) {
    if (theme === 'dark') {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
        localStorage.setItem('theme', 'dark');
    } else if (theme === 'light') {
        document.documentElement.setAttribute('data-bs-theme', 'light');
        localStorage.setItem('theme', 'light');
    }
}

// On page load, set theme if stored
const storedTheme = getTheme();
if (storedTheme !== null) {
    setTheme(storedTheme);
}

// Add event listener to each theme button
const lightBtn = document.getElementById('lightBtn');
const darkBtn = document.getElementById('darkBtn');

lightBtn.addEventListener('click', function () {
    setTheme('light');
});

darkBtn.addEventListener('click', function () {
    setTheme('dark');
});
