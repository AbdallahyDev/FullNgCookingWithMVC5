function getFilteredIngredients() {
    var minCalorieValue = $("#minCalorieValue").val();
    var maxCalorieValue = $("#maxCalorieValue").val();
    var subIngName = $("#ingName").val();
    var categorie = $("#Categories").val();
    var i = 10;
    $.ajax({
        url: '/Ingredient/getFilteredIngredients/',
        type: "POST",
        dataType: "JSON",
        data: { subIngName: subIngName, categorieId: categorie, minCalorieValue: minCalorieValue, maxCalorieValue: maxCalorieValue },
        success: function (data) {
            alert("ça marche..");
            window.location.reload();
            $("#ingName").val(data.subIngName);
        }
    });

}