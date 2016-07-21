function sortList() {
    var orderBy = $("#orderBy").val();
    $.ajax({
        url: '/Community/OrderBy/',
        type: "POST",
        dataType: "JSON",
        data: { orderBy: orderBy },
        success: function (ingredients) {
            alert("ça marche..");
            window.location.reload();
        }
    });
}