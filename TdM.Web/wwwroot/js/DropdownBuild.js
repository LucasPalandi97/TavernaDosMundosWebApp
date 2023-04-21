
//MULTI DROPDOWN BUTTONS FOR WORLDBUILD
// Get the dropdown buttons
const dropdownBtns = document.querySelectorAll("#worldbuild .dropdown-toggle");
// Toggle the dropdown menu when the button is clicked
document.addEventListener("click", function (event) {
    if (event.target.matches("#worldbuild .dropdown-toggle")) {
        event.preventDefault();
        const dropdownMenu = event.target.nextElementSibling;
        dropdownMenu.classList.toggle("show");
    } else {
        // Close the dropdown menu when the user clicks outside of it
        const dropdownMenus = document.querySelectorAll("#worldbuild .dropdown-menu.show");
        dropdownMenus.forEach(function (menu) {
            menu.classList.remove("show");
        });
    }
});

