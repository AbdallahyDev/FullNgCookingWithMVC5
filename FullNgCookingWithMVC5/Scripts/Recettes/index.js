function getFilteredRecette() {
    var minCalorieValue = $("#minCalorieValue").val();
    var maxCalorieValue = $("#maxCalorieValue").val();
    var recetteName = $("#recetteName").val();
    var recetteIngsName = $("#ingredients").val();
    var orderBy = $("#orderBy").val();
    $.ajax({
        url: '/Recette/getFilteredRecettes/',
        type: "POST",
        dataType: "JSON",
        data: { subName: recetteName, ingsName: recetteIngsName, minCalorieValue: minCalorieValue, maxCalorieValue: maxCalorieValue, orderBy: orderBy },
        success: function (ingredients) {
            alert("ça marche..");
            window.location.reload();
        }
    });
}