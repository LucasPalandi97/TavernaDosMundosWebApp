

//GET LIST<CONTINENTES> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateContinentesByMundo() {
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
                $("#SelectedMundo").data("selected", selectedmundoId);
            }
        });
    } else {
        // Clear contient if no world is selected
        $("#SelectedContinentes").empty().append('<option value="" class="text-danger" selected>No Continent</option>'); // add empty default option
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

//GET LIST<REGIOES> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateRegioesByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminRegioes/ListRegioesByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedRegioes").empty();
                $("#SelectedRegioes").append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedRegioes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear character if no world is selected        
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

//GET LIST<REGIOES> BY LIST<CONTINENTES> IDs (ALLOW MULTIPLE ITEMS)
function updateByContinenteRegioesByMundo() {
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
                }
                $("#SelectedContinentes").data("selected", selectedContinenteIds);
            }
        });
    } else {
        // Reset the region dropdown
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

//GET LIST<REGIOES> WITHOUT CONTINENT BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateRegioesNoContinentByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminRegioes/ListRegioesSemContinenteByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedRegioes").empty();
                $("#SelectedRegioes").append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedRegioes").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear character if no world is selected        
        $("#SelectedRegioes").empty().append('<option value="" class="text-danger" selected>No Region</option>'); // add empty default option
    }
}

//GET LIST<PERSONAGENS> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updatePersonagensByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminPersonagens/ListPersonagensByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedPersonagens").empty();
                $("#SelectedPersonagens").append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedPersonagens").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear character if no world is selected        
        $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
    }
}

//GET LIST<PERSONAGENS> WITH NO REGIAO BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updatePersonagensSemRegiaoByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminPersonagens/ListPersonagensSemRegiaoByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedPersonagens").empty();
                $("#SelectedPersonagens").append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedPersonagens").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear character if no world is selected        
        $("#SelectedPersonagens").empty().append('<option value="" class="text-danger" selected>No Character</option>'); // add empty default option
    }
}

//GET LIST<CRIATURAS> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateCriaturasByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminCriaturas/ListCriaturasByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedCriaturas").empty();
                $("#SelectedCriaturas").append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedCriaturas").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear character if no world is selected        
        $("#SelectedCriaturas").empty().append('<option value="" class="text-danger" selected>No Creature</option>'); // add empty default option
    }
}

//GET LIST<POVOS> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updatePovosByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminPovos/ListPovosByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedPovos").empty();
                $("#SelectedPovos").append('<option value="" class="text-danger" selected>No People</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedPovos").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear people if no world is selected        
        $("#SelectedPovos").empty().append('<option value="" class="text-danger" selected>No People</option>'); // add empty default option
    }
}

//GET LIST<CONTOS> BY MUNDOID (ALLOW MULTIPLE ITEMS)
function updateContosByMundo() {
    const selectedmundoId = $("#SelectedMundo").val();
    if (selectedmundoId != null && selectedmundoId.length > 0) {
        $.ajax({
            url: "/AdminContos/ListContosByMundo",
            type: "POST",
            dataType: "json",
            data: { id: selectedmundoId },
            success: function (data) {
                $("#SelectedContos").empty();
                $("#SelectedContos").append('<option value="" class="text-danger" selected>No Story</option>'); // add empty default option
                $.each(data, function (index, value) {
                    $("#SelectedContos").append('<option value="' + value.value + '">' + value.text + '</option>');
                });
            }
        });
    } else {
        // Clear story if no world is selected        
        $("#SelectedContos").empty().append('<option value="" class="text-danger" selected>No Story</option>'); // add empty default option
    }
}